/**
 * Form Types
 * Types specific to the multi-step credit application form
 */

import {
  ApplicantType,
  LoanType,
  EmploymentType,
  IncomeType,
  AddressType,
  DocumentType,
  Gender,
  MaritalStatus,
  RelationType,
  PaymentFrequency
} from './entities';

// ============================================================================
// FORM STATE
// ============================================================================

export interface CreditApplicationFormData {
  // Step 1: Personal Information
  step1: PersonalInfoFormData;
  
  // Step 2: Address Information
  step2: AddressInfoFormData;
  
  // Step 3: Income & Employment
  step3: IncomeEmploymentFormData;
  
  // Step 4: Credit Details
  step4: CreditDetailsFormData;
  
  // Step 5: Documents
  step5: DocumentsFormData;
  
  // Step 6: Guarantors & References
  step6: GuarantorsFormData;
  
  // Step 7: Company Information (for corporate)
  step7?: CompanyInfoFormData;
  
  // Step 8: Review & Submit
  step8: ReviewFormData;
}

// ============================================================================
// STEP 1: PERSONAL INFORMATION
// ============================================================================

export interface PersonalInfoFormData {
  // Application Type
  applicantType: ApplicantType;
  
  // Personal Details
  titleTh: string;
  firstNameTh: string;
  lastNameTh: string;
  firstNameEn?: string;
  lastNameEn?: string;
  
  // Identification
  idCardNumber: string;
  passportNumber?: string;
  
  // Personal Information
  dateOfBirth: string; // ISO date string
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
}

// ============================================================================
// STEP 2: ADDRESS INFORMATION
// ============================================================================

export interface AddressInfoFormData {
  currentAddress: AddressFormInput;
  permanentAddress: AddressFormInput;
  sameAsCurrentAddress: boolean;
  workAddress?: AddressFormInput;
}

export interface AddressFormInput {
  type: AddressType;
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
  yearsAtAddress?: number;
  monthsAtAddress?: number;
  ownershipStatus?: 'OWNED' | 'RENTED' | 'FAMILY_OWNED' | 'COMPANY_PROVIDED';
  monthlyRent?: number;
}

// ============================================================================
// STEP 3: INCOME & EMPLOYMENT
// ============================================================================

export interface IncomeEmploymentFormData {
  // Employment
  employmentType: EmploymentType;
  employerName?: string;
  position?: string;
  yearsEmployed?: number;
  monthsEmployed?: number;
  
  // Primary Income
  primaryIncomeType: IncomeType;
  monthlyIncome: number;
  
  // Additional Income
  hasAdditionalIncome: boolean;
  additionalIncomes?: AdditionalIncomeInput[];
  
  // Business Details (if self-employed)
  businessName?: string;
  businessType?: string;
  businessRegistrationNumber?: string;
  
  // Expenses
  monthlyExpenses: number;
  monthlyRent?: number;
  otherMonthlyExpenses?: number;
  
  // Existing Debts
  hasExistingLoans: boolean;
  existingLoans?: ExistingLoanInput[];
}

export interface AdditionalIncomeInput {
  id: string;
  type: IncomeType;
  amount: number;
  source?: string;
}

export interface ExistingLoanInput {
  id: string;
  lender: string;
  loanType: string;
  outstandingBalance: number;
  monthlyPayment: number;
  remainingTenure: number;
  interestRate: number;
}

// ============================================================================
// STEP 4: CREDIT DETAILS
// ============================================================================

export interface CreditDetailsFormData {
  // Loan Request
  loanType: LoanType;
  requestedAmount: number;
  requestedTenure: number;
  purpose: string;
  purposeDetails?: string;
  
  // Payment Preference
  preferredPaymentFrequency: PaymentFrequency;
  preferredPaymentDate?: number; // Day of month
  
  // Collateral
  hasCollateral: boolean;
  collateralType?: string;
  collateralDescription?: string;
  estimatedCollateralValue?: number;
  
  // Co-borrower
  hasCoApplicant: boolean;
  coApplicant?: CoApplicantInput;
}

export interface CoApplicantInput {
  titleTh: string;
  firstNameTh: string;
  lastNameTh: string;
  idCardNumber: string;
  relationship: string;
  mobilePhone: string;
  email?: string;
  monthlyIncome?: number;
}

// ============================================================================
// STEP 5: DOCUMENTS
// ============================================================================

export interface DocumentsFormData {
  documents: DocumentUploadInput[];
}

export interface DocumentUploadInput {
  id: string;
  type: DocumentType;
  name: string;
  file?: File;
  fileUrl?: string;
  status: 'pending' | 'uploading' | 'completed' | 'error';
  isMandatory: boolean;
  uploadProgress?: number;
  error?: string;
}

// ============================================================================
// STEP 6: GUARANTORS & REFERENCES
// ============================================================================

export interface GuarantorsFormData {
  hasGuarantor: boolean;
  guarantors?: GuarantorInput[];
  references: ReferenceInput[];
}

export interface GuarantorInput {
  id: string;
  relationType: RelationType;
  
  // Personal
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
  
  // Financial
  occupation?: string;
  monthlyIncome?: number;
  employer?: string;
  
  // Address
  address?: AddressFormInput;
}

export interface ReferenceInput {
  id: string;
  name: string;
  relationship: string;
  phoneNumber: string;
  email?: string;
}

// ============================================================================
// STEP 7: COMPANY INFORMATION (Corporate Only)
// ============================================================================

export interface CompanyInfoFormData {
  // Company Details
  companyNameTh: string;
  companyNameEn?: string;
  registrationNumber: string;
  taxId: string;
  
  // Business
  businessType: string;
  industryCode: string;
  establishedDate: string; // ISO date
  
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
  
  // Address
  companyAddress: AddressFormInput;
  
  // Directors
  directors: DirectorInput[];
  
  // Shareholders
  shareholders: ShareholderInput[];
}

export interface DirectorInput {
  id: string;
  titleTh: string;
  firstNameTh: string;
  lastNameTh: string;
  idCardNumber: string;
  position: string;
  isAuthorizedSignatory: boolean;
  sharePercentage?: number;
}

export interface ShareholderInput {
  id: string;
  name: string;
  idCardNumber?: string;
  taxId?: string;
  sharePercentage: number;
  numberOfShares: number;
}

// ============================================================================
// STEP 8: REVIEW & SUBMIT
// ============================================================================

export interface ReviewFormData {
  // Declarations
  agreeToTerms: boolean;
  agreeToPrivacyPolicy: boolean;
  agreeToDataSharing: boolean;
  confirmInformationAccuracy: boolean;
  
  // Signature
  signature?: string; // Base64 signature
  signedAt?: string;
  
  // Comments
  additionalComments?: string;
}

// ============================================================================
// FORM NAVIGATION
// ============================================================================

export interface FormStep {
  step: number;
  title: string;
  description: string;
  isCompleted: boolean;
  isValid: boolean;
  isCurrent: boolean;
  isAccessible: boolean;
}

export interface FormProgress {
  currentStep: number;
  totalSteps: number;
  completedSteps: number;
  percentComplete: number;
  stepsData: FormStep[];
}

// ============================================================================
// FORM HELPERS
// ============================================================================

export interface FormError {
  field: string;
  message: string;
  type: 'validation' | 'server' | 'network';
}

export interface FormState {
  isSubmitting: boolean;
  isSaving: boolean;
  isDirty: boolean;
  errors: FormError[];
  lastSaved?: Date;
  draftId?: string;
}

// ============================================================================
// CALCULATED FIELDS
// ============================================================================

export interface CalculatedFinancials {
  totalMonthlyIncome: number;
  totalMonthlyExpenses: number;
  totalMonthlyDebtPayment: number;
  netMonthlyIncome: number;
  debtToIncomeRatio: number;
  maxLoanAmount: number;
  estimatedMonthlyPayment: number;
  isEligible: boolean;
  eligibilityIssues: string[];
}

// ============================================================================
// TYPE UTILITIES
// ============================================================================

export type PartialFormData = Partial<CreditApplicationFormData>;

export type StepFormData = 
  | PersonalInfoFormData
  | AddressInfoFormData
  | IncomeEmploymentFormData
  | CreditDetailsFormData
  | DocumentsFormData
  | GuarantorsFormData
  | CompanyInfoFormData
  | ReviewFormData;

// ============================================================================
// CONSTANTS
// ============================================================================

export const TOTAL_STEPS = 8;
export const CORPORATE_TOTAL_STEPS = 8; // Step 7 is required for corporate

export const STEP_TITLES: Record<number, string> = {
  1: 'Personal Information',
  2: 'Address Information',
  3: 'Income & Employment',
  4: 'Credit Details',
  5: 'Documents',
  6: 'Guarantors & References',
  7: 'Company Information',
  8: 'Review & Submit'
};

export const STEP_DESCRIPTIONS: Record<number, string> = {
  1: 'Basic personal details and identification',
  2: 'Current and permanent address information',
  3: 'Employment and income details',
  4: 'Loan requirements and preferences',
  5: 'Upload required documents',
  6: 'Guarantor and reference information',
  7: 'Company and business information',
  8: 'Review application and submit'
};
