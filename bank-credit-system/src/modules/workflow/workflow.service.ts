import { Injectable, Logger } from '@nestjs/common';
import { Prisma } from '@prisma/client';
import { ApplicationStatus, RoleCode, AuditAction } from '../../common/enums';
import {
  InvalidStatusTransitionException,
  WorkflowPermissionException,
} from '../../common/exceptions';
import { AuditLogService, AuditContext } from '../audit-log/audit-log.service';
import { PrismaService } from '../prisma/prisma.service';

// ============================================================
// WORKFLOW STATE MACHINE DEFINITION
// ============================================================

interface TransitionRule {
  allowedRoles: RoleCode[];
  requiredChecks?: string[];
  description: string;
}

type TransitionKey = `${ApplicationStatus}->${ApplicationStatus}`;

// Defines every valid transition with required roles
const TRANSITION_RULES: Partial<Record<TransitionKey, TransitionRule>> = {
  [`${ApplicationStatus.DRAFT}->${ApplicationStatus.SUBMITTED}`]: {
    allowedRoles: [RoleCode.CUSTOMER],
    description: 'Customer submits application',
  },
  [`${ApplicationStatus.DRAFT}->${ApplicationStatus.CANCELLED}`]: {
    allowedRoles: [RoleCode.CUSTOMER, RoleCode.ADMIN],
    description: 'Customer cancels draft',
  },
  [`${ApplicationStatus.SUBMITTED}->${ApplicationStatus.DOCUMENT_CHECK}`]: {
    allowedRoles: [RoleCode.BANK_OFFICER, RoleCode.ADMIN],
    description: 'Officer begins document verification',
  },
  [`${ApplicationStatus.SUBMITTED}->${ApplicationStatus.CANCELLED}`]: {
    allowedRoles: [RoleCode.BANK_OFFICER, RoleCode.ADMIN],
    description: 'Officer cancels submitted application',
  },
  [`${ApplicationStatus.DOCUMENT_CHECK}->${ApplicationStatus.CREDIT_ANALYSIS}`]: {
    allowedRoles: [RoleCode.BANK_OFFICER, RoleCode.ADMIN],
    requiredChecks: ['documentsVerified'],
    description: 'Officer completes document check, moves to credit analysis',
  },
  [`${ApplicationStatus.DOCUMENT_CHECK}->${ApplicationStatus.NEED_MORE_INFO}`]: {
    allowedRoles: [RoleCode.BANK_OFFICER, RoleCode.ADMIN],
    description: 'Officer requests more documents',
  },
  [`${ApplicationStatus.CREDIT_ANALYSIS}->${ApplicationStatus.APPROVAL}`]: {
    allowedRoles: [RoleCode.BANK_OFFICER, RoleCode.ADMIN],
    requiredChecks: ['creditAnalysisCompleted'],
    description: 'Officer completes analysis, sends for approval',
  },
  [`${ApplicationStatus.CREDIT_ANALYSIS}->${ApplicationStatus.REJECTED}`]: {
    allowedRoles: [RoleCode.BANK_OFFICER, RoleCode.ADMIN],
    description: 'Officer rejects at analysis stage (not eligible)',
  },
  [`${ApplicationStatus.APPROVAL}->${ApplicationStatus.APPROVED}`]: {
    allowedRoles: [RoleCode.APPROVER, RoleCode.ADMIN],
    description: 'Approver approves application',
  },
  [`${ApplicationStatus.APPROVAL}->${ApplicationStatus.REJECTED}`]: {
    allowedRoles: [RoleCode.APPROVER, RoleCode.ADMIN],
    description: 'Approver rejects application',
  },
  [`${ApplicationStatus.APPROVAL}->${ApplicationStatus.NEED_MORE_INFO}`]: {
    allowedRoles: [RoleCode.APPROVER, RoleCode.ADMIN],
    description: 'Approver requests additional information',
  },
  [`${ApplicationStatus.NEED_MORE_INFO}->${ApplicationStatus.DOCUMENT_CHECK}`]: {
    allowedRoles: [RoleCode.CUSTOMER, RoleCode.BANK_OFFICER, RoleCode.ADMIN],
    description: 'Customer provides requested info, re-enters document check',
  },
  [`${ApplicationStatus.APPROVED}->${ApplicationStatus.CONTRACT_SIGNED}`]: {
    allowedRoles: [RoleCode.BANK_OFFICER, RoleCode.ADMIN],
    description: 'Contract signed by customer',
  },
  [`${ApplicationStatus.CONTRACT_SIGNED}->${ApplicationStatus.DISBURSED}`]: {
    allowedRoles: [RoleCode.BANK_OFFICER, RoleCode.ADMIN],
    description: 'Loan disbursed to customer',
  },
};

// ============================================================
// WORKFLOW SERVICE
// ============================================================
@Injectable()
export class WorkflowService {
  private readonly logger = new Logger(WorkflowService.name);

  constructor(
    private readonly prisma: PrismaService,
    private readonly auditLog: AuditLogService,
  ) {}

  /**
   * Validate that a transition is allowed for a given user role.
   * Throws InvalidStatusTransitionException if not valid.
   */
  validateTransition(
    fromStatus: ApplicationStatus,
    toStatus: ApplicationStatus,
    userRoles: RoleCode[],
  ): void {
    const key = `${fromStatus}->${toStatus}` as TransitionKey;
    const rule = TRANSITION_RULES[key];

    if (!rule) {
      throw new InvalidStatusTransitionException(
        fromStatus,
        toStatus,
        'This transition is not defined in the workflow',
      );
    }

    const hasPermission = rule.allowedRoles.some((role) =>
      userRoles.includes(role),
    );

    if (!hasPermission) {
      throw new WorkflowPermissionException(
        rule.allowedRoles.join(' or '),
        `transition ${fromStatus} → ${toStatus}`,
      );
    }
  }

  /**
   * Transition an application to a new status.
   * Validates transition, persists new status, creates history record, and audit log.
   * All within a single transaction.
   */
  async transition(params: {
    tx: Prisma.TransactionClient;
    applicationId: string;
    fromStatus: ApplicationStatus;
    toStatus: ApplicationStatus;
    userId: string;
    userFullName: string;
    userRoles: RoleCode[];
    remark?: string;
    context?: AuditContext;
  }): Promise<void> {
    const {
      tx,
      applicationId,
      fromStatus,
      toStatus,
      userId,
      userFullName,
      userRoles,
      remark,
      context,
    } = params;

    // Validate transition rules
    this.validateTransition(fromStatus, toStatus, userRoles);

    // Build update payload based on target status
    const statusTimestamps = this.getStatusTimestamp(toStatus);

    // Update application status
    await tx.creditApplication.update({
      where: { id: applicationId },
      data: {
        status: toStatus,
        ...statusTimestamps,
      },
    });

    // Record status history (mandatory for every transition)
    await tx.applicationStatusHistory.create({
      data: {
        applicationId,
        fromStatus,
        toStatus,
        changedBy: userId,
        changedByName: userFullName,
        remark,
      },
    });

    // Audit log within transaction
    await this.auditLog.logStatusChangeInTx(tx, {
      userId,
      applicationId,
      fromStatus,
      toStatus,
      remark,
      context,
    });

    this.logger.log(
      `Application ${applicationId}: ${fromStatus} → ${toStatus} by user ${userId}`,
    );
  }

  /**
   * Get list of valid next statuses from a given status
   */
  getAvailableTransitions(
    currentStatus: ApplicationStatus,
    userRoles: RoleCode[],
  ): Array<{ toStatus: ApplicationStatus; description: string }> {
    const transitions: Array<{ toStatus: ApplicationStatus; description: string }> = [];

    for (const [key, rule] of Object.entries(TRANSITION_RULES)) {
      const [from, to] = key.split('->') as [ApplicationStatus, ApplicationStatus];

      if (from !== currentStatus) continue;

      const hasPermission = rule.allowedRoles.some((role) =>
        userRoles.includes(role),
      );

      if (hasPermission) {
        transitions.push({ toStatus: to, description: rule.description });
      }
    }

    return transitions;
  }

  /**
   * Check if a specific transition is allowed (without throwing)
   */
  canTransition(
    fromStatus: ApplicationStatus,
    toStatus: ApplicationStatus,
    userRoles: RoleCode[],
  ): boolean {
    try {
      this.validateTransition(fromStatus, toStatus, userRoles);
      return true;
    } catch {
      return false;
    }
  }

  // ============================================================
  // PRIVATE HELPERS
  // ============================================================

  private getStatusTimestamp(
    status: ApplicationStatus,
  ): Partial<Prisma.CreditApplicationUpdateInput> {
    const now = new Date();
    const timestampMap: Record<string, Partial<Prisma.CreditApplicationUpdateInput>> = {
      [ApplicationStatus.SUBMITTED]: { submittedAt: now },
      [ApplicationStatus.DOCUMENT_CHECK]: { documentCheckStartAt: now },
      [ApplicationStatus.CREDIT_ANALYSIS]: { creditAnalysisStartAt: now },
      [ApplicationStatus.APPROVAL]: { approvalStartAt: now },
      [ApplicationStatus.APPROVED]: { decisionAt: now },
      [ApplicationStatus.REJECTED]: { decisionAt: now },
      [ApplicationStatus.CONTRACT_SIGNED]: { contractSignedAt: now },
      [ApplicationStatus.DISBURSED]: { disbursedAt: now },
    };

    return timestampMap[status] || {};
  }
}
