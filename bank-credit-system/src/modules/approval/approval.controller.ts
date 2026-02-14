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
} from '@nestjs/common';
import {
  ApiTags,
  ApiOperation,
  ApiResponse,
  ApiBearerAuth,
  ApiParam,
} from '@nestjs/swagger';
import { Request } from 'express';
import { ApprovalService, CreateApprovalDto } from './approval.service';
import { JwtAuthGuard } from '../../common/guards/jwt-auth.guard';
import { RolesGuard } from '../../common/guards/roles.guard';
import { Roles, CurrentUser } from '../../common/decorators';
import { ICurrentUser } from '../../common/decorators';
import { RoleCode } from '../../common/enums';

@ApiTags('Approvals')
@ApiBearerAuth()
@UseGuards(JwtAuthGuard, RolesGuard)
@Controller('credit-applications/:applicationId/approvals')
export class ApprovalController {
  constructor(private readonly approvalService: ApprovalService) {}

  @Post()
  @Roles(RoleCode.APPROVER, RoleCode.ADMIN ,RoleCode.CUSTOMER)
  @HttpCode(HttpStatus.CREATED)
  @ApiOperation({
    summary: 'Make approval decision',
    description:
      'Approver makes a decision: APPROVED, REJECTED, NEED_MORE_INFO, or ESCALATED. ' +
      'APPROVED requires approvedAmount and interestRate. ' +
      'NEED_MORE_INFO requires requestedInfo.',
  })
  @ApiParam({ name: 'applicationId', type: String, format: 'uuid' })
  @ApiResponse({
    status: 201,
    description: 'Decision recorded and application status updated',
  })
  @ApiResponse({
    status: 403,
    description: 'Application is not in APPROVAL status',
  })
  makeDecision(
    @Param('applicationId', ParseUUIDPipe) applicationId: string,
    @Body() dto: CreateApprovalDto,
    @CurrentUser() user: ICurrentUser,
    @Req() req: Request,
  ) {
    return this.approvalService.createDecision(applicationId, dto, user, {
      ipAddress: req.ip,
      userAgent: req.headers['user-agent'],
    });
  }

  @Get()
  @Roles(RoleCode.CUSTOMER, RoleCode.BANK_OFFICER, RoleCode.APPROVER, RoleCode.ADMIN)
  @ApiOperation({ summary: 'Get approval history for an application' })
  @ApiParam({ name: 'applicationId', type: String, format: 'uuid' })
  @ApiResponse({ status: 200, description: 'List of approval decisions' })
  getApprovalHistory(
    @Param('applicationId', ParseUUIDPipe) applicationId: string,
    @CurrentUser() user: ICurrentUser,
  ) {
    return this.approvalService.getApprovalHistory(applicationId, user);
  }
}
