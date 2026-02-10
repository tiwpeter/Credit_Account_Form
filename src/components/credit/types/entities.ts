/**
 * Credit Application System - Entity Types
 * Production-grade TypeScript definitions for all entities
 * Based on normalized database schema
 */

// ============================================================================
// ENUMS
// ============================================================================

export enum ApplicationStatus {
  DRAFT = 'DRAFT',
  SUBMITTED = 'SUBMITTED',
  DOCUMENT_CHECK = 'DOCUMENT_CHECK',
  CREDIT_ANALYSIS = 'CREDIT_ANALYSIS',
  APPROVAL = 'APPROVAL',
  APPROVED = 'APPROVED',
  REJECTED = 'REJECTED',
  NEED_MORE_INFO = 'NEED_MORE_INFO',
  CONTRACT_SIGNED = 'CONTRACT_SIGNED',
  DISBURSED = 'DISBURSED'
}

export enum ApplicantType {
  PERSONAL = 'PERSONAL',
  CORPORATE = 'CORPORATE'
}

export enum LoanType {
  PERSONAL = 'PERSONAL',
  HOME = 'HOME',
  AUTO = 'AUTO',
  SME = 'SME',
  CORPORATE = 'CORPORATE'
}

export enum EmploymentType {
  PERMANENT = 'PERMANENT',
  CONTRACT = 'CONTRACT',
  SELF_EMPLOYED = 'SELF_EMPLOYED',
  BUSINESS_OWNER = 'BUSINESS_OWNER',
  FREELANCE = 'FREELANCE',
  RETIRED = 'RETIRED',
  UNEMPLOYED = 'UNEMPLOYED'
}

export enum IncomeType {
  SALARY = 'SALARY',
  BUSINESS = 'BUSINESS',
  COMMISSION = 'COMMISSION',
  RENTAL = 'RENTAL',
  INVESTMENT = 'INVESTMENT',
  PENSION = 'PENSION',
  OTHER = 'OTHER'
}

export enum AddressType {
  CURRENT = 'CURRENT',
  PERMANENT = 'PERMANENT',
  WORK = 'WORK',
  COMPANY = 'COMPANY'
}

export enum DocumentType {
  ID_CARD = 'ID_CARD',
  HOUSE_REGISTRATION = 'HOUSE_REGISTRATION',
  INCOME_PROOF = 'INCOME_PROOF',
  BANK_STATEMENT = 'BANK_STATEMENT',
  TAX_RETURN = 'TAX_RETURN',
  COMPANY_REGISTRATION = 'COMPANY_REGISTRATION',
  FINANCIAL_STATEMENT = 'FINANCIAL_STATEMENT',
  COLLATERAL_DOCUMENT = 'COLLATERAL_DOCUMENT',
  OTHER = 'OTHER'
}

export enum DocumentStatus {
  PENDING = 'PENDING',
  VERIFIED = 'VERIFIED',
  REJECTED = 'REJECTED'
}

export enum RelationType {
  GUARANTOR = 'GUARANTOR',
  REFERENCE = 'REFERENCE',
  CO_BORROWER = 'CO_BORROWER'
}

export enum RiskGrade {
  AAA = 'AAA',
  AA = 'AA',
  A = 'A',
  BBB = 'BBB',
  BB = 'BB',
  B = 'B',
  CCC = 'CCC',
  CC = 'CC',
  C = 'C',
  D = 'D'
}

export enum UserRole {
  CUSTOMER = 'CUSTOMER',
  BANK_OFFICER = 'BANK_OFFICER',
  APPROVER = 'APPROVER',
  CREDIT_COMMITTEE = 'CREDIT_COMMITTEE',
  ADMIN = 'ADMIN'
}

export enum Gender {
  MALE = 'MALE',
  FEMALE = 'FEMALE',
  OTHER = 'OTHER'
}

export enum MaritalStatus {
  SINGLE = 'SINGLE',
  MARRIED = 'MARRIED',
  DIVORCED = 'DIVORCED',
  WIDOWED = 'WIDOWED'
}

export enum PaymentFrequency {
  MONTHLY = 'MONTHLY',
  QUARTERLY = 'QUARTERLY',
  SEMI_ANNUALLY = 'SEMI_ANNUALLY',
  ANNUALLY = 'ANNUALLY'
}

// ============================================================================
// BASE INTERFACES
// ============================================================================

export interface BaseEntity {
  id: string;
  createdAt: Date;
  updatedAt: Date;
  createdBy?: string;
  updatedBy?: string;
}

export interface AuditableEntity extends BaseEntity {
  version: number;
  isDeleted: boolean;
  deletedAt?: Date;
  deletedBy?: string;
}

// ============================================================================
// APPLICANT ENTITIES
// ============================================================================

export interface Applicant extends AuditableEntity {
  applicationId: string;
  type: ApplicantType;
  
  // Personal Information
  titleTh?: string;
  titleEn?: string;
  firstNameTh: string;
  lastNameTh: string;
  firstNameEn?: string;
  lastNameEn?: string;
  
  // Identification
  idCardNumber: string;
  passportNumber?: string;
  taxId?: string;
  
  // Personal Details
  dateOfBirth: Date;
  age: number;
  gender: Gender;
  nationality: string;
  maritalStatus: MaritalStatus;
  
  // Contact
  mobilePhone: string;
  homePhone?: string;
  email: string;
  lineId?: string;
  
  // Additional
  educationLevel?: string;
  numberOfDependents: number;
  
  // Relations
  addresses?: Address[];
  incomes?: Income[];
}

export interface Company extends AuditableEntity {
  applicationId: string;
  
  // Company Information
  companyNameTh: string;
  companyNameEn?: string;
  registrationNumber: string;
  taxId: string;
  
  // Business Details
  businessType: string;
  industryCode: string;
  establishedDate: Date;
  yearsInBusiness: number;
  
  // Size
  numberOfEmployees: number;
  paidUpCapital: number;
  registeredCapital: number;
  
  // Contact
  phoneNumber: string;
  email: string;
  website?: string;
  
  // Financial
  annualRevenue: number;
  annualProfit?: number;
  totalAssets?: number;
  totalLiabilities?: number;
  
  // Relations
  addresses?: Address[];
  directors?: Director[];
  shareholders?: Shareholder[];
}

export interface Director extends BaseEntity {
  companyId: string;
  
  // Personal
  titleTh: string;
  firstNameTh: string;
  lastNameTh: string;
  idCardNumber: string;
  
  // Position
  position: string;
  isAuthorizedSignatory: boolean;
  sharePercentage?: number;
}

export interface Shareholder extends BaseEntity {
  companyId: string;
  
  // Identity
  name: string;
  idCardNumber?: string;
  taxId?: string;
  
  // Ownership
  sharePercentage: number;
  numberOfShares: number;
  shareValue: number;
}

// ============================================================================
// ADDRESS
// ============================================================================

export interface Address extends AuditableEntity {
  applicantId?: string;
  companyId?: string;
  type: AddressType;
  
  // Address Components
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
  
  // Metadata
  isPrimary: boolean;
  yearsAtAddress?: number;
  monthsAtAddress?: number;
  
  // Ownership
  ownershipStatus?: 'OWNED' | 'RENTED' | 'FAMILY_OWNED' | 'COMPANY_PROVIDED';
  monthlyRent?: number;
}

// ============================================================================
// INCOME & EMPLOYMENT
// ============================================================================

export interface Income extends AuditableEntity {
  applicantId: string;
  
  // Employment
  employmentType: EmploymentType;
  employerName?: string;
  position?: string;
  yearsEmployed?: number;
  monthsEmployed?: number;
  
  // Income Details
  incomeType: IncomeType;
  monthlyIncome: number;
  otherIncome?: number;
  totalMonthlyIncome: number;
  
  // Business (if self-employed)
  businessName?: string;
  businessType?: string;
  businessRegistrationNumber?: string;
  
  // Verification
  isVerified: boolean;
  verifiedBy?: string;
  verifiedAt?: Date;
}

// ============================================================================
// CREDIT APPLICATION
// ============================================================================

export interface CreditApplication extends AuditableEntity {
  // Application Info
  applicationNumber: string;
  applicantType: ApplicantType;
  status: ApplicationStatus;
  
  // Loan Details
  loanType: LoanType;
  requestedAmount: number;
  requestedTenure: number;
  purpose: string;
  
  // Applicant References
  primaryApplicantId: string;
  coApplicantId?: string;
  companyId?: string;
  
  // Status Metadata
  submittedAt?: Date;
  submittedBy?: string;
  currentOfficerId?: string;
  currentApproverId?: string;
  
  // Relations
  applicant?: Applicant;
  coApplicant?: Applicant;
  company?: Company;
  creditDetail?: CreditDetail;
  documents?: Document[];
  guarantors?: Guarantor[];
  references?: Reference[];
  approvals?: Approval[];
  statusHistory?: StatusHistory[];
  auditLogs?: AuditLog[];
}

// ============================================================================
// CREDIT DETAIL
// ============================================================================

export interface CreditDetail extends AuditableEntity {
  applicationId: string;
  
  // Financial Assessment
  totalMonthlyIncome: number;
  totalMonthlyExpenses: number;
  totalMonthlyDebtPayment: number;
  netMonthlyIncome: number;
  
  // Ratios
  debtToIncomeRatio: number;
  debtServiceCoverageRatio?: number;
  loanToValueRatio?: number;
  
  // Existing Debts
  existingLoans: ExistingLoan[];
  totalExistingDebt: number;
  
  // Credit Score & Assessment
  creditScore?: number;
  creditScoreProvider?: string;
  riskGrade?: RiskGrade;
  
  // Approval Details (after approval)
  approvedAmount?: number;
  approvedTenure?: number;
  interestRate?: number;
  monthlyPayment?: number;
  
  // Terms
  paymentFrequency?: PaymentFrequency;
  firstPaymentDate?: Date;
  maturityDate?: Date;
  
  // Conditions
  specialConditions?: string[];
  collateralRequired?: boolean;
  collateralDescription?: string;
  collateralValue?: number;
  
  // Analysis
  analysisNotes?: string;
  analysisCompletedAt?: Date;
  analysisCompletedBy?: string;
}

export interface ExistingLoan {
  id: string;
  lender: string;
  loanType: string;
  outstandingBalance: number;
  monthlyPayment: number;
  remainingTenure: number;
  interestRate: number;
}

// ============================================================================
// DOCUMENTS
// ============================================================================

export interface Document extends AuditableEntity {
  applicationId: string;
  
  // Document Info
  type: DocumentType;
  name: string;
  description?: string;
  
  // File Details
  fileName: string;
  fileSize: number;
  mimeType: string;
  fileUrl: string;
  thumbnailUrl?: string;
  
  // Status
  status: DocumentStatus;
  isMandatory: boolean;
  
  // Verification
  verifiedBy?: string;
  verifiedAt?: Date;
  rejectionReason?: string;
  
  // OCR Data
  ocrData?: Record<string, any>;
  ocrProcessedAt?: Date;
  ocrConfidence?: number;
  
  // Metadata
  uploadedBy: string;
  category?: string;
  expiryDate?: Date;
  pageCount?: number;
}

// ============================================================================
// GUARANTOR & REFERENCE
// ============================================================================

export interface Guarantor extends AuditableEntity {
  applicationId: string;
  relationType: RelationType;
  
  // Personal Information
  titleTh: string;
  firstNameTh: string;
  lastNameTh: string;
  idCardNumber: string;
  
  // Contact
  mobilePhone: string;
  email?: string;
  
  // Relationship
  relationshipToApplicant: string;
  yearsKnown: number;
  
  // Financial (for guarantor)
  occupation?: string;
  monthlyIncome?: number;
  employer?: string;
  
  // Address
  address?: Address;
  
  // Verification
  isVerified: boolean;
  verifiedBy?: string;
  verifiedAt?: Date;
  verificationNotes?: string;
}

export interface Reference extends BaseEntity {
  applicationId: string;
  
  // Contact Information
  name: string;
  relationship: string;
  phoneNumber: string;
  email?: string;
  
  // Verification
  contactedAt?: Date;
  contactedBy?: string;
  notes?: string;
}

// ============================================================================
// APPROVAL & WORKFLOW
// ============================================================================

export interface Approval extends AuditableEntity {
  applicationId: string;
  
  // Approval Level
  level: number;
  approverRole: UserRole;
  approverId: string;
  approverName: string;
  
  // Decision
  decision: 'APPROVED' | 'REJECTED' | 'NEED_MORE_INFO' | 'PENDING';
  approvedAmount?: number;
  approvedTenure?: number;
  interestRate?: number;
  
  // Feedback
  comments?: string;
  conditions?: string[];
  requestedDocuments?: string[];
  
  // Timing
  reviewedAt?: Date;
  dueDate?: Date;
  
  // Delegation
  delegatedTo?: string;
  delegatedAt?: Date;
}

export interface StatusHistory extends BaseEntity {
  applicationId: string;
  
  // Status Change
  fromStatus: ApplicationStatus;
  toStatus: ApplicationStatus;
  
  // Actor
  changedBy: string;
  changedByRole: UserRole;
  
  // Context
  reason?: string;
  remarks?: string;
  
  // Timing
  timestamp: Date;
}

// ============================================================================
// AUDIT LOG
// ============================================================================

export interface AuditLog extends BaseEntity {
  // Entity Reference
  entityType: string;
  entityId: string;
  applicationId?: string;
  
  // Action
  action: 'CREATE' | 'UPDATE' | 'DELETE' | 'VIEW' | 'SUBMIT' | 'APPROVE' | 'REJECT' | 'DOWNLOAD' | 'PRINT';
  description: string;
  
  // Actor
  userId: string;
  userName: string;
  userRole: UserRole;
  
  // Context
  ipAddress?: string;
  userAgent?: string;
  sessionId?: string;
  
  // Data
  oldValue?: Record<string, any>;
  newValue?: Record<string, any>;
  metadata?: Record<string, any>;
  
  // Timing
  timestamp: Date;
}

// ============================================================================
// USER & PERMISSIONS
// ============================================================================

export interface User extends AuditableEntity {
  // Identity
  username: string;
  email: string;
  
  // Personal
  titleTh?: string;
  firstNameTh: string;
  lastNameTh: string;
  firstNameEn?: string;
  lastNameEn?: string;
  
  // Employment
  employeeId?: string;
  department?: string;
  position?: string;
  
  // Access
  role: UserRole;
  permissions: string[];
  isActive: boolean;
  
  // Security
  lastLoginAt?: Date;
  passwordChangedAt?: Date;
  failedLoginAttempts: number;
  lockedUntil?: Date;
}

// ============================================================================
// TYPE GUARDS
// ============================================================================

export function isPersonalApplicant(applicant: Applicant): boolean {
  return applicant.type === ApplicantType.PERSONAL;
}

export function isCorporateApplication(application: CreditApplication): boolean {
  return application.applicantType === ApplicantType.CORPORATE;
}

export function isApprovalRequired(status: ApplicationStatus): boolean {
  return [
    ApplicationStatus.APPROVAL,
    ApplicationStatus.NEED_MORE_INFO
  ].includes(status);
}

export function canEditApplication(status: ApplicationStatus): boolean {
  return [
    ApplicationStatus.DRAFT,
    ApplicationStatus.NEED_MORE_INFO
  ].includes(status);
}

export function isFinalStatus(status: ApplicationStatus): boolean {
  return [
    ApplicationStatus.APPROVED,
    ApplicationStatus.REJECTED,
    ApplicationStatus.DISBURSED
  ].includes(status);
}
