/**
 * API Types
 * Request and response types for API communication
 */

import {
  CreditApplication,
  ApplicationStatus,
  ApplicantType,
  LoanType,
  UserRole
} from './entities';
import { CreditApplicationFormData } from './form';

// ============================================================================
// API REQUEST TYPES
// ============================================================================

export interface CreateApplicationRequest {
  applicantType: ApplicantType;
  applicant: {
    titleTh: string;
    firstNameTh: string;
    lastNameTh: string;
    firstNameEn?: string;
    lastNameEn?: string;
    idCardNumber: string;
    passportNumber?: string;
    dateOfBirth: string;
    gender: string;
    nationality: string;
    maritalStatus: string;
    mobilePhone: string;
    homePhone?: string;
    email: string;
    lineId?: string;
    educationLevel?: string;
    numberOfDependents: number;
  };
  addresses: Array<{
    type: string;
    houseNumber: string;
    moo?: string;
    village?: string;
    soi?: string;
    street?: string;
    subDistrict: string;
    district: string;
    province: string;
    postalCode: string;
    country: string;
    isPrimary: boolean;
    yearsAtAddress?: number;
    monthsAtAddress?: number;
    ownershipStatus?: string;
    monthlyRent?: number;
  }>;
  income: {
    employmentType: string;
    employerName?: string;
    position?: string;
    yearsEmployed?: number;
    monthsEmployed?: number;
    incomeType: string;
    monthlyIncome: number;
    otherIncome?: number;
    totalMonthlyIncome: number;
    businessName?: string;
    businessType?: string;
    businessRegistrationNumber?: string;
  };
  creditDetail: {
    loanType: string;
    requestedAmount: number;
    requestedTenure: number;
    purpose: string;
    totalMonthlyIncome: number;
    totalMonthlyExpenses: number;
    totalMonthlyDebtPayment: number;
    netMonthlyIncome: number;
    debtToIncomeRatio: number;
    existingLoans: Array<{
      lender: string;
      loanType: string;
      outstandingBalance: number;
      monthlyPayment: number;
      remainingTenure: number;
      interestRate: number;
    }>;
    totalExistingDebt: number;
    paymentFrequency?: string;
    collateralRequired?: boolean;
    collateralDescription?: string;
    collateralValue?: number;
  };
  coApplicant?: {
    titleTh: string;
    firstNameTh: string;
    lastNameTh: string;
    idCardNumber: string;
    relationship: string;
    mobilePhone: string;
    email?: string;
    monthlyIncome?: number;
  };
  guarantors?: Array<{
    relationType: string;
    titleTh: string;
    firstNameTh: string;
    lastNameTh: string;
    idCardNumber: string;
    mobilePhone: string;
    email?: string;
    relationshipToApplicant: string;
    yearsKnown: number;
    occupation?: string;
    monthlyIncome?: number;
    employer?: string;
    address?: any;
  }>;
  references?: Array<{
    name: string;
    relationship: string;
    phoneNumber: string;
    email?: string;
  }>;
  company?: {
    companyNameTh: string;
    companyNameEn?: string;
    registrationNumber: string;
    taxId: string;
    businessType: string;
    industryCode: string;
    establishedDate: string;
    numberOfEmployees: number;
    paidUpCapital: number;
    registeredCapital: number;
    phoneNumber: string;
    email: string;
    website?: string;
    annualRevenue: number;
    annualProfit?: number;
    totalAssets?: number;
    totalLiabilities?: number;
    addresses: any[];
    directors: Array<{
      titleTh: string;
      firstNameTh: string;
      lastNameTh: string;
      idCardNumber: string;
      position: string;
      isAuthorizedSignatory: boolean;
      sharePercentage?: number;
    }>;
    shareholders: Array<{
      name: string;
      idCardNumber?: string;
      taxId?: string;
      sharePercentage: number;
      numberOfShares: number;
    }>;
  };
  documents?: Array<{
    type: string;
    name: string;
    fileName: string;
    fileSize: number;
    mimeType: string;
    fileUrl: string;
    isMandatory: boolean;
  }>;
  reviewData: {
    agreeToTerms: boolean;
    agreeToPrivacyPolicy: boolean;
    agreeToDataSharing: boolean;
    confirmInformationAccuracy: boolean;
    signature?: string;
    additionalComments?: string;
  };
}

export interface UpdateApplicationRequest extends Partial<CreateApplicationRequest> {
  applicationId: string;
}

export interface SubmitApplicationRequest {
  applicationId: string;
  signature: string;
  submittedBy: string;
}

export interface UpdateStatusRequest {
  applicationId: string;
  newStatus: ApplicationStatus;
  reason?: string;
  remarks?: string;
  updatedBy: string;
  userRole: UserRole;
}

export interface UploadDocumentRequest {
  applicationId: string;
  documentType: string;
  file: File;
  isMandatory: boolean;
}

export interface VerifyDocumentRequest {
  documentId: string;
  status: 'VERIFIED' | 'REJECTED';
  verifiedBy: string;
  rejectionReason?: string;
}

export interface CreateApprovalRequest {
  applicationId: string;
  level: number;
  approverId: string;
  decision: 'APPROVED' | 'REJECTED' | 'NEED_MORE_INFO';
  approvedAmount?: number;
  approvedTenure?: number;
  interestRate?: number;
  comments?: string;
  conditions?: string[];
  requestedDocuments?: string[];
}

// ============================================================================
// API RESPONSE TYPES
// ============================================================================

export interface ApiResponse<T = any> {
  success: boolean;
  data?: T;
  error?: ApiError;
  message?: string;
  timestamp: string;
}

export interface ApiError {
  code: string;
  message: string;
  details?: any;
  field?: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
  hasNext: boolean;
  hasPrevious: boolean;
}

export interface ApplicationListResponse extends PaginatedResponse<ApplicationSummary> {}

export interface ApplicationSummary {
  id: string;
  applicationNumber: string;
  applicantType: ApplicantType;
  applicantName: string;
  loanType: LoanType;
  requestedAmount: number;
  status: ApplicationStatus;
  submittedAt?: string;
  createdAt: string;
  updatedAt: string;
  currentOfficer?: {
    id: string;
    name: string;
  };
}

export interface ApplicationDetailResponse {
  application: CreditApplication;
  permissions: ApplicationPermissions;
  nextActions: WorkflowAction[];
  timeline: TimelineEvent[];
}

export interface ApplicationPermissions {
  canEdit: boolean;
  canSubmit: boolean;
  canVerifyDocuments: boolean;
  canAnalyze: boolean;
  canApprove: boolean;
  canReject: boolean;
  canRequestMoreInfo: boolean;
  canViewSensitiveData: boolean;
  canDownloadDocuments: boolean;
  canPrint: boolean;
}

export interface WorkflowAction {
  action: string;
  label: string;
  requiresReason: boolean;
  requiresAmount?: boolean;
  targetStatus: ApplicationStatus;
}

export interface TimelineEvent {
  id: string;
  timestamp: string;
  status: ApplicationStatus;
  actor: {
    id: string;
    name: string;
    role: UserRole;
  };
  action: string;
  remarks?: string;
}

export interface DocumentUploadResponse {
  documentId: string;
  fileName: string;
  fileUrl: string;
  thumbnailUrl?: string;
  uploadedAt: string;
}

export interface CreditAnalysisResponse {
  applicationId: string;
  financialSummary: {
    totalMonthlyIncome: number;
    totalMonthlyExpenses: number;
    totalMonthlyDebtPayment: number;
    netMonthlyIncome: number;
    debtToIncomeRatio: number;
    debtServiceCoverageRatio?: number;
  };
  creditScore?: {
    score: number;
    provider: string;
    scoredAt: string;
  };
  riskAssessment: {
    riskGrade: string;
    riskScore: number;
    riskFactors: string[];
    mitigatingFactors: string[];
  };
  recommendation: {
    isApproved: boolean;
    recommendedAmount: number;
    recommendedTenure: number;
    recommendedInterestRate: number;
    monthlyPayment: number;
    conditions: string[];
    reasoning: string;
  };
  complianceChecks: {
    amlCheck: 'PASS' | 'FAIL' | 'PENDING';
    sanctionScreening: 'PASS' | 'FAIL' | 'PENDING';
    fraudCheck: 'PASS' | 'FAIL' | 'PENDING';
    creditBureauCheck: 'PASS' | 'FAIL' | 'PENDING';
  };
}

// ============================================================================
// QUERY PARAMETERS
// ============================================================================

export interface ApplicationListQuery {
  page?: number;
  pageSize?: number;
  status?: ApplicationStatus[];
  applicantType?: ApplicantType;
  loanType?: LoanType;
  search?: string;
  fromDate?: string;
  toDate?: string;
  sortBy?: 'createdAt' | 'updatedAt' | 'submittedAt' | 'requestedAmount';
  sortOrder?: 'asc' | 'desc';
  officerId?: string;
}

export interface DocumentListQuery {
  applicationId: string;
  type?: string[];
  status?: string[];
}

export interface AuditLogQuery {
  applicationId?: string;
  entityType?: string;
  entityId?: string;
  action?: string;
  userId?: string;
  fromDate?: string;
  toDate?: string;
  page?: number;
  pageSize?: number;
}

// ============================================================================
// MASTER DATA RESPONSES
// ============================================================================

export interface MasterDataResponse {
  loanTypes: LoanTypeConfig[];
  provinces: Province[];
  industries: Industry[];
  occupations: string[];
  educationLevels: string[];
  relationships: string[];
  documentTypes: DocumentTypeConfig[];
}

export interface LoanTypeConfig {
  code: LoanType;
  nameTh: string;
  nameEn: string;
  minAmount: number;
  maxAmount: number;
  minTenure: number;
  maxTenure: number;
  baseInterestRate: number;
  requiresCollateral: boolean;
  allowsCoApplicant: boolean;
  allowsGuarantor: boolean;
  requiredDocuments: string[];
}

export interface Province {
  code: string;
  nameTh: string;
  nameEn: string;
  region: string;
  districts: District[];
}

export interface District {
  code: string;
  nameTh: string;
  nameEn: string;
  subDistricts: SubDistrict[];
}

export interface SubDistrict {
  code: string;
  nameTh: string;
  nameEn: string;
  postalCode: string;
}

export interface Industry {
  code: string;
  nameTh: string;
  nameEn: string;
  category: string;
}

export interface DocumentTypeConfig {
  type: string;
  nameTh: string;
  nameEn: string;
  description: string;
  isMandatory: boolean;
  acceptedFormats: string[];
  maxFileSize: number;
  requiresOCR: boolean;
  applicantTypes: ApplicantType[];
  loanTypes: LoanType[];
}

// ============================================================================
// WEBSOCKET TYPES
// ============================================================================

export interface WebSocketMessage {
  type: 'status_update' | 'document_verified' | 'approval_decision' | 'comment_added';
  applicationId: string;
  data: any;
  timestamp: string;
}

// ============================================================================
// BATCH OPERATIONS
// ============================================================================

export interface BatchAssignRequest {
  applicationIds: string[];
  officerId: string;
  assignedBy: string;
}

export interface BatchStatusUpdateRequest {
  applicationIds: string[];
  newStatus: ApplicationStatus;
  reason?: string;
  updatedBy: string;
}

// ============================================================================
// REPORTS
// ============================================================================

export interface ApplicationReportQuery {
  fromDate: string;
  toDate: string;
  groupBy: 'status' | 'loanType' | 'officer' | 'date';
  format: 'json' | 'csv' | 'pdf';
}

export interface ApplicationReport {
  summary: {
    totalApplications: number;
    totalApproved: number;
    totalRejected: number;
    totalInProgress: number;
    totalAmount: number;
    averageAmount: number;
    approvalRate: number;
  };
  byStatus: Record<ApplicationStatus, number>;
  byLoanType: Record<LoanType, number>;
  byOfficer: Array<{
    officerId: string;
    officerName: string;
    applicationCount: number;
    approvalCount: number;
    approvalRate: number;
  }>;
  trend: Array<{
    date: string;
    count: number;
    amount: number;
  }>;
}
