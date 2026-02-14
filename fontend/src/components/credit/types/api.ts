// API request/response types

import type { CreditApplication, PersonalInfo, ValidationError } from './entities';

// API Response wrapper
export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  error?: ApiError;
  timestamp: string;
}

export interface ApiError {
  code: string;
  message: string;
  details?: Record<string, string>;
}

// Pagination
export interface PaginationParams {
  page: number;
  pageSize: number;
  sortBy?: string;
  sortOrder?: 'asc' | 'desc';
}

export interface PaginatedResponse<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

// Create Application Request
export interface CreateApplicationRequest {
  customerId: string;
  loanType: string;
  loanAmount: number;
  step1Data: {
    title: string;
    firstName: string;
    lastName: string;
    gender: string;
    dateOfBirth: string;
    nationality: string;
    idCardNumber: string;
    maritalStatus: string;
    dependents: number;
    mobilePhone: string;
    email: string;
  };
}

// Submit Application Request
export interface SubmitApplicationRequest {
  applicationId: string;
  loanType: string;
  personalInfo: PersonalInfo;
}

// Get Applications Response
export interface GetApplicationsResponse extends PaginatedResponse<CreditApplication> {}

// Get Application Detail Response
export interface GetApplicationDetailResponse {
  application: CreditApplication;
}

// Update Step Response
export interface UpdateStepResponse {
  success: boolean;
  stepNumber: number;
  validation: {
    isValid: boolean;
    errors?: ValidationError[];
  };
}

// Master Data Responses
export interface ProvinceData {
  code: string;
  name: string;
  thai: string;
}

export interface DistrictData {
  code: string;
  name: string;
  provinceCode: string;
  thai: string;
}

export interface LoanPurposeOption {
  code: string;
  label: string;
  thai: string;
  loanTypes: string[];
}

export interface MasterDataResponse {
  provinces: ProvinceData[];
  districts: Record<string, DistrictData[]>;
  loanPurposes: LoanPurposeOption[];
  industries: { code: string; label: string; thai: string }[];
  occupations: { code: string; label: string; thai: string }[];
}

// Eligibility Check Response
export interface EligibilityCheckResponse {
  isEligible: boolean;
  dtiRatio: number;
  riskGrade: string;
  reasons: string[];
  estimatedMonthlyPayment: number;
  estimatedTotalInterest: number;
}

// Document Upload Response
export interface DocumentUploadResponse {
  documentId: string;
  fileName: string;
  fileUrl: string;
  uploadedAt: string;
}

// WebSocket Message Types (for real-time updates)
export interface WebSocketMessage {
  type: 'STATUS_UPDATE' | 'APPROVAL_UPDATE' | 'DOCUMENT_VERIFIED' | 'ERROR';
  applicationId: string;
  data: Record<string, unknown>;
  timestamp: string;
}
