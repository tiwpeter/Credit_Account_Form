import {
  Controller,
  Post,
  Get,
  Body,
  Param,
  UseGuards,
  HttpCode,
  HttpStatus,
  ParseUUIDPipe,
  Req,
  ForbiddenException,
} from '@nestjs/common';
import {
  ApiTags,
  ApiOperation,
  ApiResponse,
  ApiBearerAuth,
  ApiParam,
  ApiBody,
} from '@nestjs/swagger';
import {
  IsNumber,
  IsOptional,
  IsString,
  IsPositive,
  Min,
  MaxLength,
} from 'class-validator';
import { ApiPropertyOptional, ApiProperty } from '@nestjs/swagger';
import { Request } from 'express';
import { CreditAnalysisService } from './credit-analysis.service';
import { PrismaService } from '../prisma/prisma.service';
import { AuditLogService } from '../audit-log/audit-log.service';
import { WorkflowService } from '../workflow/workflow.service';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { RolesGuard } from '../../common/guards/roles.guard';
import { Roles, CurrentUser } from '../../common/decorators';
import { ICurrentUser } from '../../common/decorators';
import {
  ApplicationStatus,
  ApplicationType,
  AuditAction,
  EmploymentType,
  RoleCode,
} from '../../common/enums';
import { ApplicationNotFoundException } from '../../common/exceptions';

// ============================================================
// CREDIT ANALYSIS DTO
// ============================================================
export class PerformCreditAnalysisDto {
  @ApiProperty({
    example: 50000,
    description: 'Verified monthly income (may differ from self-reported)',
  })
  @IsNumber()
  @IsPositive()
  verifiedMonthlyIncome: number;

  @ApiPropertyOptional({ description: 'Override existing monthly debt (if verification found different amount)' })
  @IsOptional()
  @IsNumber()
  @Min(0)
  verifiedExistingDebt?: number;

  @ApiPropertyOptional({ description: 'Analyst notes' })
  @IsOptional()
  @IsString()
  @MaxLength(2000)
  analystNote?: string;
}

// ============================================================
// CREDIT ANALYSIS CONTROLLER
// ============================================================
@ApiTags('Credit Analysis')
@ApiBearerAuth()
@UseGuards(JwtAuthGuard, RolesGuard)
@Controller('credit-applications/:applicationId/credit-analysis')
export class CreditAnalysisController {
  constructor(
    private readonly creditAnalysisService: CreditAnalysisService,
    private readonly prisma: PrismaService,
    private readonly auditLog: AuditLogService,
    private readonly workflowService: WorkflowService,
  ) {}

  @Post()
  @Roles(RoleCode.BANK_OFFICER, RoleCode.ADMIN)
  @HttpCode(HttpStatus.OK)
  @ApiOperation({
    summary: 'Perform credit analysis',
    description:
      'Runs DTI calculation, eligibility check, and risk grading. Stores results. Application must be in CREDIT_ANALYSIS status.',
  })
  @ApiParam({ name: 'applicationId', type: String, format: 'uuid' })
  @ApiBody({ type: PerformCreditAnalysisDto })
  @ApiResponse({ status: 200, description: 'Analysis complete. Returns full analysis results.' })
  @ApiResponse({ status: 403, description: 'Application is not in CREDIT_ANALYSIS status' })
  async performAnalysis(
    @Param('applicationId', ParseUUIDPipe) applicationId: string,
    @Body() dto: PerformCreditAnalysisDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    // Load application with applicant data
    const application = await this.prisma.creditApplication.findFirst({
      where: { id: applicationId, deletedAt: null },
      include: {
        personalApplicant: true,
        corporateApplicant: { include: { company: true } },
      },
    });

    if (!application) throw new ApplicationNotFoundException(applicationId);

    if (application.status !== ApplicationStatus.CREDIT_ANALYSIS) {
      throw new ForbiddenException(
        `Credit analysis can only be performed when status is CREDIT_ANALYSIS. Current: ${application.status}`,
      );
    }

    // Extract financial data based on loan type
    let monthlyIncome = dto.verifiedMonthlyIncome;
    let existingDebt = dto.verifiedExistingDebt ?? 0;
    let employmentType: EmploymentType = EmploymentType.EMPLOYED;
    let yearsEmployed: number | undefined;

    if (application.type === ApplicationType.PERSONAL_LOAN) {
      const pa = application.personalApplicant;
      existingDebt = dto.verifiedExistingDebt ?? Number(pa?.existingDebtPayment ?? 0);
      employmentType = (pa?.employmentType as EmploymentType) || EmploymentType.EMPLOYED;
      yearsEmployed = pa?.yearsEmployed ? Number(pa.yearsEmployed) : undefined;
    } else if (application.type === ApplicationType.CORPORATE_LOAN) {
      const company = application.corporateApplicant?.company;
      existingDebt = dto.verifiedExistingDebt ?? Number(company?.existingDebtPayment ?? 0);
    }

    // Calculate estimated monthly payment using suggested rate (10% default)
    const estimatedRate = 0.10;
    const estimatedMonthlyPayment = this.creditAnalysisService.calculateMonthlyPayment(
      Number(application.requestedAmount),
      estimatedRate,
      application.requestedTenureMonths,
    );

    // DTI Calculation
    const dtiResult = this.creditAnalysisService.calculateDti({
      monthlyIncome,
      existingMonthlyDebtPayment: existingDebt,
      proposedMonthlyPayment: estimatedMonthlyPayment,
    });

    // Eligibility Check
    const eligibilityResult = this.creditAnalysisService.checkEligibility({
      monthlyIncome,
      requestedAmount: Number(application.requestedAmount),
      requestedTenureMonths: application.requestedTenureMonths,
      existingMonthlyDebt: existingDebt,
      loanType: application.type as ApplicationType,
    });

    // Risk Assessment
    const riskResult = this.creditAnalysisService.assessRisk({
      monthlyIncome,
      requestedAmount: Number(application.requestedAmount),
      tenureMonths: application.requestedTenureMonths,
      existingDebt,
      employmentType,
      yearsEmployed,
      dtiRatio: dtiResult.dtiRatio,
      loanType: application.type as ApplicationType,
    });

    // Persist analysis results
    const savedAnalysis = await this.creditAnalysisService.saveAnalysis({
      applicationId,
      analyzedById: user.id,
      verifiedMonthlyIncome: monthlyIncome,
      dtiResult,
      eligibilityResult,
      riskResult,
      analystNote: dto.analystNote,
    });

    // Audit log
    await this.auditLog.log({
      userId: user.id,
      action: AuditAction.CREDIT_ANALYSIS,
      entityType: 'CreditAnalysis',
      entityId: savedAnalysis.id,
      applicationId,
      afterState: {
        creditScore: riskResult.creditScore,
        riskGrade: riskResult.riskGrade,
        dtiRatio: dtiResult.dtiRatio,
        dtiPassed: dtiResult.dtiPassed,
        isEligible: eligibilityResult.isEligible,
      },
      changedFields: ['all'],
      remark: riskResult.recommendation,
      ipAddress: req.ip,
    });

    return {
      analysis: savedAnalysis,
      dtiResult,
      eligibilityResult,
      riskResult,
      summary: {
        overallEligible: eligibilityResult.isEligible && dtiResult.dtiPassed,
        creditScore: riskResult.creditScore,
        riskGrade: riskResult.riskGrade,
        suggestedRate: `${(riskResult.suggestedInterestRate * 100).toFixed(2)}%`,
        recommendation: riskResult.recommendation,
      },
    };
  }

  @Get()
  @Roles(RoleCode.BANK_OFFICER, RoleCode.APPROVER, RoleCode.ADMIN)
  @ApiOperation({ summary: 'Get credit analysis results for an application' })
  @ApiParam({ name: 'applicationId', type: String, format: 'uuid' })
  async getAnalysis(
    @Param('applicationId', ParseUUIDPipe) applicationId: string,
    @CurrentUser() user: ICurrentUser,
  ) {
    const analysis = await this.prisma.creditAnalysis.findUnique({
      where: { applicationId },
    });

    if (!analysis) {
      throw new ForbiddenException('No credit analysis found for this application');
    }

    return analysis;
  }
}
