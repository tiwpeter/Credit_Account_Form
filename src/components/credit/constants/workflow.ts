/**
 * Workflow Constants
 * Application workflow states and transitions
 */

import { ApplicationStatus, UserRole } from '../types/entities';

// ============================================================================
// WORKFLOW STATES
// ============================================================================

export const WORKFLOW_STATES = {
  DRAFT: {
    value: ApplicationStatus.DRAFT,
    label: 'Draft',
    description: 'Application is being prepared',
    color: 'gray',
    icon: 'FileEdit',
    allowedRoles: [UserRole.CUSTOMER],
    nextStates: [ApplicationStatus.SUBMITTED]
  },
  SUBMITTED: {
    value: ApplicationStatus.SUBMITTED,
    label: 'Submitted',
    description: 'Application has been submitted for review',
    color: 'blue',
    icon: 'Send',
    allowedRoles: [UserRole.CUSTOMER, UserRole.BANK_OFFICER],
    nextStates: [ApplicationStatus.DOCUMENT_CHECK, ApplicationStatus.NEED_MORE_INFO]
  },
  DOCUMENT_CHECK: {
    value: ApplicationStatus.DOCUMENT_CHECK,
    label: 'Document Check',
    description: 'Documents are being verified',
    color: 'yellow',
    icon: 'FileCheck',
    allowedRoles: [UserRole.BANK_OFFICER],
    nextStates: [ApplicationStatus.CREDIT_ANALYSIS, ApplicationStatus.NEED_MORE_INFO]
  },
  CREDIT_ANALYSIS: {
    value: ApplicationStatus.CREDIT_ANALYSIS,
    label: 'Credit Analysis',
    description: 'Credit analysis is in progress',
    color: 'orange',
    icon: 'Calculator',
    allowedRoles: [UserRole.BANK_OFFICER],
    nextStates: [ApplicationStatus.APPROVAL, ApplicationStatus.NEED_MORE_INFO, ApplicationStatus.REJECTED]
  },
  APPROVAL: {
    value: ApplicationStatus.APPROVAL,
    label: 'Pending Approval',
    description: 'Waiting for approval decision',
    color: 'purple',
    icon: 'Clock',
    allowedRoles: [UserRole.APPROVER, UserRole.CREDIT_COMMITTEE],
    nextStates: [ApplicationStatus.APPROVED, ApplicationStatus.REJECTED, ApplicationStatus.NEED_MORE_INFO]
  },
  APPROVED: {
    value: ApplicationStatus.APPROVED,
    label: 'Approved',
    description: 'Application has been approved',
    color: 'green',
    icon: 'CheckCircle',
    allowedRoles: [UserRole.APPROVER, UserRole.CREDIT_COMMITTEE, UserRole.BANK_OFFICER],
    nextStates: [ApplicationStatus.CONTRACT_SIGNED]
  },
  REJECTED: {
    value: ApplicationStatus.REJECTED,
    label: 'Rejected',
    description: 'Application has been rejected',
    color: 'red',
    icon: 'XCircle',
    allowedRoles: [UserRole.APPROVER, UserRole.CREDIT_COMMITTEE],
    nextStates: []
  },
  NEED_MORE_INFO: {
    value: ApplicationStatus.NEED_MORE_INFO,
    label: 'Need More Information',
    description: 'Additional information is required',
    color: 'amber',
    icon: 'AlertCircle',
    allowedRoles: [UserRole.BANK_OFFICER, UserRole.APPROVER],
    nextStates: [ApplicationStatus.SUBMITTED]
  },
  CONTRACT_SIGNED: {
    value: ApplicationStatus.CONTRACT_SIGNED,
    label: 'Contract Signed',
    description: 'Loan contract has been signed',
    color: 'indigo',
    icon: 'FileSignature',
    allowedRoles: [UserRole.BANK_OFFICER],
    nextStates: [ApplicationStatus.DISBURSED]
  },
  DISBURSED: {
    value: ApplicationStatus.DISBURSED,
    label: 'Disbursed',
    description: 'Loan has been disbursed',
    color: 'emerald',
    icon: 'Coins',
    allowedRoles: [UserRole.BANK_OFFICER],
    nextStates: []
  }
} as const;

// ============================================================================
// WORKFLOW TRANSITIONS
// ============================================================================

export interface WorkflowTransition {
  from: ApplicationStatus;
  to: ApplicationStatus;
  action: string;
  label: string;
  requiresReason: boolean;
  requiresApproval: boolean;
  requiredRole: UserRole[];
  validationRules?: string[];
}

export const WORKFLOW_TRANSITIONS: WorkflowTransition[] = [
  // From DRAFT
  {
    from: ApplicationStatus.DRAFT,
    to: ApplicationStatus.SUBMITTED,
    action: 'SUBMIT',
    label: 'Submit Application',
    requiresReason: false,
    requiresApproval: false,
    requiredRole: [UserRole.CUSTOMER],
    validationRules: ['ALL_MANDATORY_FIELDS', 'ALL_MANDATORY_DOCUMENTS']
  },
  
  // From SUBMITTED
  {
    from: ApplicationStatus.SUBMITTED,
    to: ApplicationStatus.DOCUMENT_CHECK,
    action: 'START_DOCUMENT_CHECK',
    label: 'Start Document Check',
    requiresReason: false,
    requiresApproval: false,
    requiredRole: [UserRole.BANK_OFFICER]
  },
  {
    from: ApplicationStatus.SUBMITTED,
    to: ApplicationStatus.NEED_MORE_INFO,
    action: 'REQUEST_MORE_INFO',
    label: 'Request More Information',
    requiresReason: true,
    requiresApproval: false,
    requiredRole: [UserRole.BANK_OFFICER]
  },
  
  // From DOCUMENT_CHECK
  {
    from: ApplicationStatus.DOCUMENT_CHECK,
    to: ApplicationStatus.CREDIT_ANALYSIS,
    action: 'START_CREDIT_ANALYSIS',
    label: 'Start Credit Analysis',
    requiresReason: false,
    requiresApproval: false,
    requiredRole: [UserRole.BANK_OFFICER],
    validationRules: ['ALL_DOCUMENTS_VERIFIED']
  },
  {
    from: ApplicationStatus.DOCUMENT_CHECK,
    to: ApplicationStatus.NEED_MORE_INFO,
    action: 'REQUEST_MORE_DOCUMENTS',
    label: 'Request More Documents',
    requiresReason: true,
    requiresApproval: false,
    requiredRole: [UserRole.BANK_OFFICER]
  },
  
  // From CREDIT_ANALYSIS
  {
    from: ApplicationStatus.CREDIT_ANALYSIS,
    to: ApplicationStatus.APPROVAL,
    action: 'SUBMIT_FOR_APPROVAL',
    label: 'Submit for Approval',
    requiresReason: false,
    requiresApproval: false,
    requiredRole: [UserRole.BANK_OFFICER],
    validationRules: ['CREDIT_ANALYSIS_COMPLETE', 'DTI_RATIO_VALID']
  },
  {
    from: ApplicationStatus.CREDIT_ANALYSIS,
    to: ApplicationStatus.REJECTED,
    action: 'REJECT_OFFICER',
    label: 'Reject Application',
    requiresReason: true,
    requiresApproval: false,
    requiredRole: [UserRole.BANK_OFFICER]
  },
  {
    from: ApplicationStatus.CREDIT_ANALYSIS,
    to: ApplicationStatus.NEED_MORE_INFO,
    action: 'REQUEST_MORE_INFO_ANALYSIS',
    label: 'Request More Information',
    requiresReason: true,
    requiresApproval: false,
    requiredRole: [UserRole.BANK_OFFICER]
  },
  
  // From APPROVAL
  {
    from: ApplicationStatus.APPROVAL,
    to: ApplicationStatus.APPROVED,
    action: 'APPROVE',
    label: 'Approve Application',
    requiresReason: false,
    requiresApproval: true,
    requiredRole: [UserRole.APPROVER, UserRole.CREDIT_COMMITTEE]
  },
  {
    from: ApplicationStatus.APPROVAL,
    to: ApplicationStatus.REJECTED,
    action: 'REJECT_APPROVER',
    label: 'Reject Application',
    requiresReason: true,
    requiresApproval: true,
    requiredRole: [UserRole.APPROVER, UserRole.CREDIT_COMMITTEE]
  },
  {
    from: ApplicationStatus.APPROVAL,
    to: ApplicationStatus.NEED_MORE_INFO,
    action: 'REQUEST_MORE_INFO_APPROVAL',
    label: 'Request More Information',
    requiresReason: true,
    requiresApproval: false,
    requiredRole: [UserRole.APPROVER, UserRole.CREDIT_COMMITTEE]
  },
  
  // From APPROVED
  {
    from: ApplicationStatus.APPROVED,
    to: ApplicationStatus.CONTRACT_SIGNED,
    action: 'SIGN_CONTRACT',
    label: 'Contract Signed',
    requiresReason: false,
    requiresApproval: false,
    requiredRole: [UserRole.BANK_OFFICER]
  },
  
  // From CONTRACT_SIGNED
  {
    from: ApplicationStatus.CONTRACT_SIGNED,
    to: ApplicationStatus.DISBURSED,
    action: 'DISBURSE',
    label: 'Disburse Loan',
    requiresReason: false,
    requiresApproval: false,
    requiredRole: [UserRole.BANK_OFFICER],
    validationRules: ['CONTRACT_SIGNED', 'ACCOUNT_VERIFIED']
  },
  
  // From NEED_MORE_INFO
  {
    from: ApplicationStatus.NEED_MORE_INFO,
    to: ApplicationStatus.SUBMITTED,
    action: 'RESUBMIT',
    label: 'Resubmit Application',
    requiresReason: false,
    requiresApproval: false,
    requiredRole: [UserRole.CUSTOMER]
  }
];

// ============================================================================
// WORKFLOW HELPERS
// ============================================================================

export function getNextStates(currentStatus: ApplicationStatus): ApplicationStatus[] {
  return WORKFLOW_STATES[currentStatus]?.nextStates || [];
}

export function canTransition(
  from: ApplicationStatus,
  to: ApplicationStatus,
  userRole: UserRole
): boolean {
  const transition = WORKFLOW_TRANSITIONS.find(
    t => t.from === from && t.to === to
  );
  
  if (!transition) return false;
  
  return transition.requiredRole.includes(userRole);
}

export function getAvailableActions(
  currentStatus: ApplicationStatus,
  userRole: UserRole
): WorkflowTransition[] {
  return WORKFLOW_TRANSITIONS.filter(
    t => t.from === currentStatus && t.requiredRole.includes(userRole)
  );
}

export function isTerminalStatus(status: ApplicationStatus): boolean {
  return [
    ApplicationStatus.APPROVED,
    ApplicationStatus.REJECTED,
    ApplicationStatus.DISBURSED
  ].includes(status);
}

export function canEditApplication(status: ApplicationStatus): boolean {
  return [
    ApplicationStatus.DRAFT,
    ApplicationStatus.NEED_MORE_INFO
  ].includes(status);
}

export function requiresOfficerAction(status: ApplicationStatus): boolean {
  return [
    ApplicationStatus.SUBMITTED,
    ApplicationStatus.DOCUMENT_CHECK,
    ApplicationStatus.CREDIT_ANALYSIS
  ].includes(status);
}

export function requiresApproverAction(status: ApplicationStatus): boolean {
  return status === ApplicationStatus.APPROVAL;
}

// ============================================================================
// SLA DEFINITIONS
// ============================================================================

export interface SLA {
  status: ApplicationStatus;
  targetHours: number;
  warningHours: number;
  escalationHours: number;
}

export const SLA_DEFINITIONS: SLA[] = [
  {
    status: ApplicationStatus.SUBMITTED,
    targetHours: 24,
    warningHours: 20,
    escalationHours: 30
  },
  {
    status: ApplicationStatus.DOCUMENT_CHECK,
    targetHours: 48,
    warningHours: 40,
    escalationHours: 60
  },
  {
    status: ApplicationStatus.CREDIT_ANALYSIS,
    targetHours: 72,
    warningHours: 60,
    escalationHours: 96
  },
  {
    status: ApplicationStatus.APPROVAL,
    targetHours: 48,
    warningHours: 40,
    escalationHours: 72
  }
];

export function getSLA(status: ApplicationStatus): SLA | undefined {
  return SLA_DEFINITIONS.find(sla => sla.status === status);
}

export function calculateSLAStatus(
  status: ApplicationStatus,
  statusChangedAt: Date
): 'ON_TIME' | 'WARNING' | 'OVERDUE' | 'ESCALATED' {
  const sla = getSLA(status);
  if (!sla) return 'ON_TIME';
  
  const hoursElapsed = (Date.now() - statusChangedAt.getTime()) / (1000 * 60 * 60);
  
  if (hoursElapsed >= sla.escalationHours) return 'ESCALATED';
  if (hoursElapsed >= sla.targetHours) return 'OVERDUE';
  if (hoursElapsed >= sla.warningHours) return 'WARNING';
  
  return 'ON_TIME';
}
