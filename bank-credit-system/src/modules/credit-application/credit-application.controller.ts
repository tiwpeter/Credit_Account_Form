import {
  Controller,
  Get,
  Post,
  Put,
  Patch,
  Delete,
  Body,
  Param,
  Query,
  UseGuards,
  HttpCode,
  HttpStatus,
  ParseUUIDPipe,
  Req,
} from '@nestjs/common';
import {
  ApiTags,
  ApiOperation,
  ApiResponse,
  ApiBearerAuth,
  ApiParam,
  ApiQuery,
} from '@nestjs/swagger';
import { Request } from 'express';
import { CreditApplicationService } from './credit-application.service';
import {
  CreateCreditApplicationDto,
  UpdateCreditApplicationDto,
  ApplicationQueryDto,
  TransitionStatusDto,
  AssignOfficerDto,
  SubmitApplicationDto,
} from './dto/credit-application.dto';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { RolesGuard } from '../../common/guards/roles.guard';
import { Roles, CurrentUser } from '../../common/decorators';
import { ICurrentUser } from '../../common/decorators';
import { ApplicationStatus, RoleCode } from '../../common/enums';

@ApiTags('Credit Applications')
@ApiBearerAuth()
@UseGuards(JwtAuthGuard, RolesGuard)
@Controller('credit-applications')
export class CreditApplicationController {
  constructor(private readonly service: CreditApplicationService) {}

  // ============================================================
  // LIST APPLICATIONS
  // ============================================================
  @Get()
  @Roles(RoleCode.CUSTOMER, RoleCode.BANK_OFFICER, RoleCode.APPROVER, RoleCode.ADMIN)
  @ApiOperation({
    summary: 'List credit applications',
    description:
      'Customers see only their own applications. Officers/Approvers/Admins see all.',
  })
  @ApiResponse({ status: 200, description: 'Paginated list of applications' })
  findAll(
    @Query() query: ApplicationQueryDto,
    @CurrentUser() user: ICurrentUser,
  ) {
    return this.service.findAll(query, user);
  }

  // ============================================================
  // GET SINGLE APPLICATION
  // ============================================================
  @Get(':id')
  @Roles(RoleCode.CUSTOMER, RoleCode.BANK_OFFICER, RoleCode.APPROVER, RoleCode.ADMIN)
  @ApiOperation({ summary: 'Get application by ID' })
  @ApiParam({ name: 'id', type: String, format: 'uuid' })
  @ApiResponse({ status: 200, description: 'Application details with full workflow history' })
  @ApiResponse({ status: 403, description: 'Access denied' })
  @ApiResponse({ status: 404, description: 'Application not found' })
  findOne(
    @Param('id', ParseUUIDPipe) id: string,
    @CurrentUser() user: ICurrentUser,
  ) {
    return this.service.findById(id, user);
  }

  // ============================================================
  // CREATE DRAFT
  // ============================================================
  @Post()
  @Roles(RoleCode.CUSTOMER)
  @HttpCode(HttpStatus.CREATED)
  @ApiOperation({
    summary: 'Create new credit application (draft)',
    description:
      'Creates application in DRAFT status. Can save partial data for multi-step forms.',
  })
  @ApiResponse({ status: 201, description: 'Application draft created' })
  @ApiResponse({ status: 400, description: 'Validation error' })
  create(
    @Body() dto: CreateCreditApplicationDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.create(dto, user, {
      ipAddress: req.ip,
      userAgent: req.headers['user-agent'],
    });
  }

  // ============================================================
  // UPDATE DRAFT
  // ============================================================
  @Put(':id')
  @Roles(RoleCode.CUSTOMER)
  @ApiOperation({
    summary: 'Update draft application',
    description:
      'Update fields in a DRAFT application. Supports multi-step form saving via currentStep.',
  })
  @ApiParam({ name: 'id', type: String, format: 'uuid' })
  @ApiResponse({ status: 200, description: 'Application updated' })
  @ApiResponse({ status: 409, description: 'Optimistic lock conflict' })
  update(
    @Param('id', ParseUUIDPipe) id: string,
    @Body() dto: UpdateCreditApplicationDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.update(id, dto, user, {
      ipAddress: req.ip,
      userAgent: req.headers['user-agent'],
    });
  }

  // ============================================================
  // SUBMIT APPLICATION
  // ============================================================
  @Post(':id/submit')
  @Roles(RoleCode.CUSTOMER)
  @HttpCode(HttpStatus.OK)
  @ApiOperation({
    summary: 'Submit application for review',
    description:
      'Transitions application from DRAFT → SUBMITTED. Validates completeness. Requires customer confirmation.',
  })
  @ApiParam({ name: 'id', type: String, format: 'uuid' })
  @ApiResponse({ status: 200, description: 'Application submitted successfully' })
  @ApiResponse({ status: 422, description: 'Application is incomplete' })
  submit(
    @Param('id', ParseUUIDPipe) id: string,
    @Body() dto: SubmitApplicationDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.submit(id, dto, user, {
      ipAddress: req.ip,
      userAgent: req.headers['user-agent'],
    });
  }

  // ============================================================
  // WORKFLOW TRANSITIONS (Officer)
  // ============================================================
  @Post(':id/start-document-check')
  @Roles(RoleCode.BANK_OFFICER, RoleCode.ADMIN)
  @HttpCode(HttpStatus.OK)
  @ApiOperation({ summary: 'Begin document verification' })
  startDocumentCheck(
    @Param('id', ParseUUIDPipe) id: string,
    @Body() dto: TransitionStatusDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.transitionStatus(
      id,
      ApplicationStatus.DOCUMENT_CHECK,
      dto,
      user,
      { ipAddress: req.ip, userAgent: req.headers['user-agent'] },
    );
  }

  @Post(':id/start-credit-analysis')
  @Roles(RoleCode.BANK_OFFICER, RoleCode.ADMIN)
  @HttpCode(HttpStatus.OK)
  @ApiOperation({
    summary: 'Move to credit analysis',
    description: 'Document check complete. Moves to credit analysis stage.',
  })
  startCreditAnalysis(
    @Param('id', ParseUUIDPipe) id: string,
    @Body() dto: TransitionStatusDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.transitionStatus(
      id,
      ApplicationStatus.CREDIT_ANALYSIS,
      dto,
      user,
      { ipAddress: req.ip, userAgent: req.headers['user-agent'] },
    );
  }

  @Post(':id/send-for-approval')
  @Roles(RoleCode.BANK_OFFICER, RoleCode.ADMIN)
  @HttpCode(HttpStatus.OK)
  @ApiOperation({
    summary: 'Submit for approval',
    description: 'Credit analysis completed. Forwards to approver.',
  })
  sendForApproval(
    @Param('id', ParseUUIDPipe) id: string,
    @Body() dto: TransitionStatusDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.transitionStatus(
      id,
      ApplicationStatus.APPROVAL,
      dto,
      user,
      { ipAddress: req.ip, userAgent: req.headers['user-agent'] },
    );
  }

  @Post(':id/request-more-info')
  @Roles(RoleCode.BANK_OFFICER, RoleCode.APPROVER, RoleCode.ADMIN)
  @HttpCode(HttpStatus.OK)
  @ApiOperation({ summary: 'Request additional information from customer' })
  requestMoreInfo(
    @Param('id', ParseUUIDPipe) id: string,
    @Body() dto: TransitionStatusDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.transitionStatus(
      id,
      ApplicationStatus.NEED_MORE_INFO,
      dto,
      user,
      { ipAddress: req.ip, userAgent: req.headers['user-agent'] },
    );
  }

  @Post(':id/reject-at-analysis')
  @Roles(RoleCode.BANK_OFFICER, RoleCode.ADMIN)
  @HttpCode(HttpStatus.OK)
  @ApiOperation({ summary: 'Reject application at credit analysis stage' })
  rejectAtAnalysis(
    @Param('id', ParseUUIDPipe) id: string,
    @Body() dto: TransitionStatusDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.transitionStatus(
      id,
      ApplicationStatus.REJECTED,
      dto,
      user,
      { ipAddress: req.ip, userAgent: req.headers['user-agent'] },
    );
  }

  @Post(':id/sign-contract')
  @Roles(RoleCode.BANK_OFFICER, RoleCode.ADMIN)
  @HttpCode(HttpStatus.OK)
  @ApiOperation({ summary: 'Mark contract as signed' })
  signContract(
    @Param('id', ParseUUIDPipe) id: string,
    @Body() dto: TransitionStatusDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.transitionStatus(
      id,
      ApplicationStatus.CONTRACT_SIGNED,
      dto,
      user,
      { ipAddress: req.ip, userAgent: req.headers['user-agent'] },
    );
  }

  @Post(':id/disburse')
  @Roles(RoleCode.BANK_OFFICER, RoleCode.ADMIN)
  @HttpCode(HttpStatus.OK)
  @ApiOperation({ summary: 'Mark loan as disbursed' })
  disburse(
    @Param('id', ParseUUIDPipe) id: string,
    @Body() dto: TransitionStatusDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.transitionStatus(
      id,
      ApplicationStatus.DISBURSED,
      dto,
      user,
      { ipAddress: req.ip, userAgent: req.headers['user-agent'] },
    );
  }

  // ============================================================
  // ASSIGN OFFICER
  // ============================================================
  @Patch(':id/assign-officer')
  @Roles(RoleCode.BANK_OFFICER, RoleCode.ADMIN)
  @ApiOperation({ summary: 'Assign bank officer to application' })
  @ApiParam({ name: 'id', type: String, format: 'uuid' })
  assignOfficer(
    @Param('id', ParseUUIDPipe) id: string,
    @Body() dto: AssignOfficerDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.assignOfficer(id, dto, user, { ipAddress: req.ip });
  }

  // ============================================================
  // AVAILABLE TRANSITIONS
  // ============================================================
  @Get(':id/available-transitions')
  @Roles(RoleCode.CUSTOMER, RoleCode.BANK_OFFICER, RoleCode.APPROVER, RoleCode.ADMIN)
  @ApiOperation({ summary: 'Get available workflow transitions for current user' })
  @ApiParam({ name: 'id', type: String, format: 'uuid' })
  getAvailableTransitions(
    @Param('id', ParseUUIDPipe) id: string,
    @CurrentUser() user: ICurrentUser,
  ) {
    return this.service.getAvailableTransitions(id, user);
  }

  // ============================================================
  // DELETE DRAFT
  // ============================================================
  @Delete(':id')
  @Roles(RoleCode.CUSTOMER)
  @HttpCode(HttpStatus.NO_CONTENT)
  @ApiOperation({ summary: 'Delete draft application (soft delete)' })
  @ApiParam({ name: 'id', type: String, format: 'uuid' })
  @ApiResponse({ status: 204, description: 'Application deleted' })
  @ApiResponse({ status: 403, description: 'Only DRAFT applications can be deleted' })
  delete(
    @Param('id', ParseUUIDPipe) id: string,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.service.softDelete(id, user, { ipAddress: req.ip });
  }
}
