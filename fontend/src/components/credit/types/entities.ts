// Types for domain entities in the credit application system

// Enums
export enum LoanType {
  PERSONAL = 'PERSONAL',
  HOME = 'HOME',
  AUTO = 'AUTO',
  SME = 'SME',
  CORPORATE = 'CORPORATE'
}

export enum ApplicationStatus {
  DRAFT = 'DRAFT',
  SUBMITTED = 'SUBMITTED',
  DOCUMENT_CHECK = 'DOCUMENT_CHECK',
  CREDIT_ANALYSIS = 'CREDIT_ANALYSIS',
  APPROVAL = 'APPROVAL',
  APPROVED = 'APPROVED',
  REJECTED = 'REJECTED',
  CONTRACT_SIGNED = 'CONTRACT_SIGNED',
  DISBURSED = 'DISBURSED'
}

export enum RiskGrade {
  EXCELLENT = 'EXCELLENT',    // <= 25%
  GOOD = 'GOOD',              // 26-40%
  ACCEPTABLE = 'ACCEPTABLE',  // 41-50%
  FAIR = 'FAIR',              // 51-60%
  POOR = 'POOR'               // > 60%
}

export enum EmploymentType {
  PERMANENT = 'PERMANENT',
  CONTRACT = 'CONTRACT',
  SELF_EMPLOYED = 'SELF_EMPLOYED',
  RETIRED = 'RETIRED'
}

export enum DocumentType {
  ID_CARD = 'ID_CARD',
  PASSPORT = 'PASSPORT',
  PROOF_OF_ADDRESS = 'PROOF_OF_ADDRESS',
  INCOME_STATEMENT = 'INCOME_STATEMENT',
  TAX_RETURN = 'TAX_RETURN',
  BANK_STATEMENT = 'BANK_STATEMENT',
  EMPLOYMENT_LETTER = 'EMPLOYMENT_LETTER',
  CONTRACT = 'CONTRACT',
  LICENSE = 'LICENSE',
  FINANCIAL_STATEMENT = 'FINANCIAL_STATEMENT'
}

export enum Gender {
  MALE = 'MALE',
  FEMALE = 'FEMALE',
  OTHER = 'OTHER'
}

// Main Entity Interfaces
export interface PersonalInfo {
  id: string;
  title: string;
  firstName: string;
  lastName: string;
  gender: Gender;
  dateOfBirth: string;
  nationality: string;
  idCardNumber: string;
  idCardExpire?: string;
  passportNumber?: string;
  passportExpire?: string;
  mobilePhone: string;
  email: string;
  maritalStatus: 'SINGLE' | 'MARRIED' | 'DIVORCED' | 'WIDOWED';
  dependents: number;
  createdAt: string;
  updatedAt: string;
}

export interface AddressInfo {
  id: string;
  applicationId: string;
  currentAddress: {
    building?: string;
    street: string;
    province: string;
    district: string;
    subdistrict: string;
    zipCode: string;
    country: string;
  };
  permanentAddress: {
    building?: string;
    street: string;
    province: string;
    district: string;
    subdistrict: string;
    zipCode: string;
    country: string;
  };
  isSameAsCurrentAddress: boolean;
  yearsAtCurrentAddress: number;
  housingType: 'OWN' | 'RENT' | 'WITH_FAMILY' | 'OTHER';
  monthlyRent?: number;
  createdAt: string;
  updatedAt: string;
}

export interface IncomeInfo {
  id: string;
  applicationId: string;
  employmentType: EmploymentType;
  company: string;
  position: string;
  industry: string;
  monthlyIncome: number;
  annualIncome: number;
  otherIncome: number;
  otherIncomeSource?: string;
  employmentStartDate: string;
  workingYears: number;
  createdAt: string;
  updatedAt: string;
}

export interface CreditInfo {
  id: string;
  applicationId: string;
  loanType: LoanType;
  loanAmount: number;
  loanPurpose: string;
  tenorMonths: number;
  existingLoans: ExistingLoan[];
  totalExistingDebt: number;
  monthlyDebtObligation: number;
  dtiRatio: number;
  riskGrade: RiskGrade;
  isEligible: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface ExistingLoan {
  id: string;
  type: 'BANK_LOAN' | 'CREDIT_CARD' | 'INSTALLMENT';
  lender: string;
  amount: number;
  monthlyPayment: number;
  remainingBalance: number;
  term: number;
}

export interface DocumentUpload {
  id: string;
  applicationId: string;
  documentType: DocumentType;
  fileName: string;
  fileUrl: string;
  fileSize: number;
  uploadedAt: string;
  verifiedAt?: string;
  status: 'PENDING' | 'VERIFIED' | 'REJECTED';
  remarks?: string;
}

export interface CreditApplication {
  id: string;
  applicationNumber: string;
  customerId: string;
  status: ApplicationStatus;
  loanType: LoanType;
  loanAmount: number;
  personalInfo: PersonalInfo;
  addressInfo?: AddressInfo;
  incomeInfo?: IncomeInfo;
  creditInfo?: CreditInfo;
  documents: DocumentUpload[];
  guarantors: Guarantor[];
  approvalHistory: ApprovalHistory[];
  submittedAt?: string;
  approvedAt?: string;
  rejectedAt?: string;
  rejectionReason?: string;
  disbursedAt?: string;
  createdAt: string;
  updatedAt: string;
}

export interface Guarantor {
  id: string;
  applicationId: string;
  relationship: string;
  firstName: string;
  lastName: string;
  mobilePhone: string;
  email: string;
  idCardNumber: string;
  netWorth?: number;
}

export interface ApprovalHistory {
  id: string;
  applicationId: string;
  step: ApplicationStatus;
  approver: string;
  action: 'APPROVED' | 'REJECTED' | 'PENDING' | 'RETURNED';
  remarks?: string;
  timestamp: string;
}

export interface UserRole {
  id: string;
  name: 'CUSTOMER' | 'BANK_OFFICER' | 'APPROVER' | 'CREDIT_COMMITTEE' | 'ADMIN';
  permissions: string[];
}

export interface User {
  id: string;
  email: string;
  name: string;
  role: UserRole;
  department?: string;
  createdAt: string;
}

// Utility Interfaces
export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  error?: {
    code: string;
    message: string;
    details?: Record<string, string>;
  };
  timestamp: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

export interface ValidationError {
  field: string;
  message: string;
}

export interface CalculationResult {
  dti: number;
  monthlyPayment: number;
  totalInterest: number;
  riskGrade: RiskGrade;
  isEligible: boolean;
  eligibilityReasons: string[];
}
