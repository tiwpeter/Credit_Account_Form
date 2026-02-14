import { Injectable, Logger } from '@nestjs/common';
import { PrismaService } from '../prisma/prisma.service';
import { AuditAction } from '../../common/enums';
import { Prisma } from '@prisma/client';

export interface CreateAuditLogDto {
  userId?: string;
  action: AuditAction;
  entityType: string;
  entityId: string;
  applicationId?: string;
  beforeState?: Record<string, any>;
  afterState?: Record<string, any>;
  changedFields?: string[];
  remark?: string;
  ipAddress?: string;
  userAgent?: string;
  requestId?: string;
}

export interface AuditContext {
  userId?: string;
  ipAddress?: string;
  userAgent?: string;
  requestId?: string;
}

@Injectable()
export class AuditLogService {
  private readonly logger = new Logger(AuditLogService.name);

  constructor(private readonly prisma: PrismaService) {}

  /**
   * Create a single audit log entry
   */
  async log(dto: CreateAuditLogDto): Promise<void> {
    try {
      await this.prisma.auditLog.create({
        data: {
          userId: dto.userId,
          action: dto.action,
          entityType: dto.entityType,
          entityId: dto.entityId,
          applicationId: dto.applicationId,
          // Store JSON fields as string in SQLite dev schema
          beforeState: dto.beforeState ? JSON.stringify(dto.beforeState) : null,
          afterState: dto.afterState ? JSON.stringify(dto.afterState) : null,
          changedFields: JSON.stringify(dto.changedFields || []),
          remark: dto.remark,
          ipAddress: dto.ipAddress,
          userAgent: dto.userAgent,
          requestId: dto.requestId,
        },
      });
    } catch (error) {
      // Audit log failures should NOT crash the main operation
      // Log error but don't rethrow
      this.logger.error(
        `Failed to create audit log: ${error.message}`,
        error.stack,
      );
    }
  }

  /**
   * Create audit log within an existing transaction
   * Use this when you need the audit log to be part of the same transaction
   */
  async logInTransaction(
    tx: Prisma.TransactionClient,
    dto: CreateAuditLogDto,
  ): Promise<void> {
    await tx.auditLog.create({
      data: {
        userId: dto.userId,
        action: dto.action,
        entityType: dto.entityType,
        entityId: dto.entityId,
        applicationId: dto.applicationId,
        beforeState: dto.beforeState ? JSON.stringify(dto.beforeState) : null,
        afterState: dto.afterState ? JSON.stringify(dto.afterState) : null,
        changedFields: JSON.stringify(dto.changedFields || []),
        remark: dto.remark,
        ipAddress: dto.ipAddress,
        userAgent: dto.userAgent,
        requestId: dto.requestId,
      },
    });
  }

  /**
   * Log status change with before/after states
   */
  async logStatusChange(params: {
    userId: string;
    applicationId: string;
    fromStatus: string;
    toStatus: string;
    remark?: string;
    context?: AuditContext;
  }): Promise<void> {
    await this.log({
      userId: params.userId,
      action: AuditAction.STATUS_CHANGE,
      entityType: 'CreditApplication',
      entityId: params.applicationId,
      applicationId: params.applicationId,
      beforeState: { status: params.fromStatus },
      afterState: { status: params.toStatus },
      changedFields: ['status'],
      remark: params.remark,
      ...params.context,
    });
  }

  /**
   * Log in transaction with status change
   */
  async logStatusChangeInTx(
    tx: Prisma.TransactionClient,
    params: {
      userId: string;
      applicationId: string;
      fromStatus: string;
      toStatus: string;
      remark?: string;
      context?: AuditContext;
    },
  ): Promise<void> {
    await this.logInTransaction(tx, {
      userId: params.userId,
      action: AuditAction.STATUS_CHANGE,
      entityType: 'CreditApplication',
      entityId: params.applicationId,
      applicationId: params.applicationId,
      beforeState: { status: params.fromStatus },
      afterState: { status: params.toStatus },
      changedFields: ['status'],
      remark: params.remark,
      ...params.context,
    });
  }

  /**
   * Retrieve audit logs for an application
   */
  async findByApplication(applicationId: string, limit = 50) {
    return this.prisma.auditLog.findMany({
      where: { applicationId },
      include: {
        user: {
          select: {
            id: true,
            firstName: true,
            lastName: true,
            email: true,
          },
        },
      },
      orderBy: { createdAt: 'desc' },
      take: limit,
    });
  }

  /**
   * Retrieve audit logs for a user
   */
  async findByUser(userId: string, limit = 50) {
    return this.prisma.auditLog.findMany({
      where: { userId },
      orderBy: { createdAt: 'desc' },
      take: limit,
    });
  }
}
