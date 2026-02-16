import {
  Injectable,
  Logger,
  ForbiddenException,
  NotFoundException,
} from '@nestjs/common';
import {
  IsEnum,
  IsString,
  IsOptional,
  IsNumber,
  IsNotEmpty,
  Min,
  Max,
  IsPositive,
  MaxLength,
} from 'class-validator';
import { ApiProperty, ApiPropertyOptional } from '@nestjs/swagger';
import { PrismaService } from '../prisma/prisma.service';
import { AuditLogService } from '../audit-log/audit-log.service';
import { WorkflowService } from '../workflow/workflow.service';
import { ICurrentUser } from '../../common/decorators';
import {
  ApplicationStatus,
  ApprovalDecision,
  AuditAction,
  RoleCode,
} from '../../common/enums';
import { ApplicationNotFoundException } from '../../common/exceptions';
import { prismaToDomainStatus } from '@modules/credit-application/credit-application.service';

// ============================================================
// APPROVAL DTOs
// ============================================================
export class CreateApprovalDto {
  @ApiProperty({ enum: ApprovalDecision })
  @IsEnum(ApprovalDecision)
  decision: ApprovalDecision;

  @ApiPropertyOptional({ example: 450000, description: 'Approved loan amount (required if APPROVED)' })
  @IsOptional()
  @IsNumber()
  @IsPositive()
  approvedAmount?: number;

  @ApiPropertyOptional({ example: 36 })
  @IsOptional()
  @IsNumber()
  @Min(1)
  @Max(120)
  approvedTenure?: number;

  @ApiPropertyOptional({ example: 0.085, description: 'Annual interest rate as decimal (e.g. 0.085 = 8.5%)' })
  @IsOptional()
  @IsNumber()
  @Min(0.01)
  @Max(0.36)
  interestRate?: number;

  @ApiPropertyOptional({ example: 'Requires monthly income proof every 6 months' })
  @IsOptional()
  @IsString()
  @MaxLength(2000)
  conditions?: string;

  @ApiProperty({ example: 'Good credit profile. Approved with standard conditions.' })
  @IsString()
  @IsNotEmpty()
  @MaxLength(2000)
  remarks: string;

  @ApiPropertyOptional({
    example: 'Please provide last 3 months bank statements',
    description: 'Required for NEED_MORE_INFO decision',
  })
  @IsOptional()
  @IsString()
  @MaxLength(1000)
  requestedInfo?: string;
}

// ============================================================
// APPROVAL SERVICE
// ============================================================
@Injectable()
export class ApprovalService {
  private readonly logger = new Logger(ApprovalService.name);

  constructor(
    private readonly prisma: PrismaService,
    private readonly auditLog: AuditLogService,
    private readonly workflowService: WorkflowService,
  ) {}

  async createDecision(
    applicationId: string,
    dto: CreateApprovalDto,
    user: ICurrentUser,
    context: { ipAddress?: string; userAgent?: string },
  ) {
    // Fetch application
    const application = await this.prisma.creditApplication.findFirst({
      where: { id: applicationId, deletedAt: null },
    });

    if (!application) throw new ApplicationNotFoundException(applicationId);

    if (application.status !== ApplicationStatus.APPROVAL) {
      throw new ForbiddenException(
        `Application must be in APPROVAL status to make a decision. Current status: ${application.status}`,
      );
    }

    // Validate decision-specific requirements
    if (dto.decision === ApprovalDecision.APPROVED) {
      if (!dto.approvedAmount || !dto.interestRate) {
        throw new ForbiddenException(
          'Approved amount and interest rate are required for APPROVED decision',
        );
      }
    }

    if (
      dto.decision === ApprovalDecision.NEED_MORE_INFO &&
      !dto.requestedInfo
    ) {
      throw new ForbiddenException(
        'Please specify what information is needed',
      );
    }

    // Determine new application status
    const statusMap: Record<ApprovalDecision, ApplicationStatus> = {
      [ApprovalDecision.APPROVED]: ApplicationStatus.APPROVED,
      [ApprovalDecision.REJECTED]: ApplicationStatus.REJECTED,
      [ApprovalDecision.NEED_MORE_INFO]: ApplicationStatus.NEED_MORE_INFO,
      [ApprovalDecision.ESCALATED]: ApplicationStatus.APPROVAL, // Stays in APPROVAL
    };

    const toStatus = statusMap[dto.decision];

    return this.prisma.executeInTransaction(async (tx) => {
      // Create approval record
      const approval = await tx.approval.create({
        data: {
          applicationId,
          approverId: user.id,
          decision: dto.decision,
          approvedAmount: dto.approvedAmount,
          approvedTenure: dto.approvedTenure,
          interestRate: dto.interestRate,
          conditions: dto.conditions,
          remarks: dto.remarks,
          requestedInfo: dto.requestedInfo,
        },
      });

      // Update application with approval details
      const updateData: any = {};
      if (dto.decision === ApprovalDecision.APPROVED) {
        updateData.approvedAmount = dto.approvedAmount;
        updateData.approvedTenureMonths = dto.approvedTenure;
        updateData.approvedInterestRate = dto.interestRate;
        updateData.approvalConditions = dto.conditions;
      }
      if (dto.decision === ApprovalDecision.REJECTED) {
        updateData.rejectionReason = dto.remarks;
      }

      if (Object.keys(updateData).length > 0) {
        await tx.creditApplication.update({
          where: { id: applicationId },
          data: updateData,
        });
      }

      // Transition status (only if not ESCALATED)
      if (dto.decision !== ApprovalDecision.ESCALATED) {
        await this.workflowService.transition({
          tx,
          applicationId,
          fromStatus: prismaToDomainStatus(application.status),
          toStatus,
          userId: user.id,
          userFullName: `${user.firstName} ${user.lastName}`,
          userRoles: user.roles,
          remark: `${dto.decision}: ${dto.remarks.substring(0, 100)}`,
          context,
        });
      }

      // Audit log
      await this.auditLog.logInTransaction(tx, {
        userId: user.id,
        action: AuditAction.APPROVAL_DECISION,
        entityType: 'Approval',
        entityId: approval.id,
        applicationId,
        afterState: {
          decision: dto.decision,
          approvedAmount: dto.approvedAmount,
          interestRate: dto.interestRate,
        },
        changedFields: ['decision', 'status'],
        remark: `Approval decision: ${dto.decision}`,
        ipAddress: context.ipAddress,
        userAgent: context.userAgent,
      });

      this.logger.log(
        `Approval decision ${dto.decision} for application ${applicationId} by user ${user.id}`,
      );

      return {
        approval,
        decision: dto.decision,
        newStatus: toStatus,
      };
    });
  }

  async getApprovalHistory(applicationId: string, user: ICurrentUser) {
    const application = await this.prisma.creditApplication.findFirst({
      where: { id: applicationId, deletedAt: null },
    });

    if (!application) throw new ApplicationNotFoundException(applicationId);

    if (
      user.roles.includes(RoleCode.CUSTOMER) &&
      application.customerId !== user.id
    ) {
      throw new ForbiddenException('Access denied');
    }

    return this.prisma.approval.findMany({
      where: { applicationId },
      include: {
        approver: {
          select: { id: true, firstName: true, lastName: true, email: true },
        },
      },
      orderBy: { decidedAt: 'asc' },
    });
  }
}
