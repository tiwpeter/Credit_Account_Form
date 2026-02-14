// Form types for multi-step credit application

import type { PersonalInfo, AddressInfo, IncomeInfo, CreditInfo, Guarantor, DocumentUpload } from './entities';

// Step-specific form types
export interface Step1PersonalInfoForm {
  title: string;
  firstName: string;
  lastName: string;
  gender: 'MALE' | 'FEMALE' | 'OTHER';
  dateOfBirth: string;
  nationality: string;
  idCardNumber: string;
  idCardExpire?: string;
  maritalStatus: 'SINGLE' | 'MARRIED' | 'DIVORCED' | 'WIDOWED';
  dependents: number;
  mobilePhone: string;
  email: string;
}

export interface Step2AddressInfoForm {
  currentAddress: {
    building?: string;
    street: string;
    province: string;
    district: string;
    subdistrict: string;
    zipCode: string;
  };
  permanentAddress?: {
    building?: string;
    street: string;
    province: string;
    district: string;
    subdistrict: string;
    zipCode: string;
  };
  isSameAsCurrentAddress: boolean;
  yearsAtCurrentAddress: number;
  housingType: 'OWN' | 'RENT' | 'WITH_FAMILY' | 'OTHER';
  monthlyRent?: number;
}

export interface Step3IncomeEmploymentForm {
  employmentType: 'PERMANENT' | 'CONTRACT' | 'SELF_EMPLOYED' | 'RETIRED';
  company: string;
  position: string;
  industry: string;
  monthlyIncome: number;
  annualIncome: number;
  otherIncome: number;
  otherIncomeSource?: string;
  employmentStartDate: string;
  workingYears: number;
}

export interface Step4CreditDetailsForm {
  loanType: string;
  loanAmount: number;
  loanPurpose: string;
  tenorMonths: number;
  existingLoans: {
    type: string;
    lender: string;
    monthlyPayment: number;
    remainingBalance: number;
  }[];
}

export interface Step5DocumentsForm {
  documents: {
    documentType: string;
    fileName: string;
    fileUrl: string;
    fileSize: number;
  }[];
}

export interface Step6GuarantorsForm {
  guarantors: {
    relationship: string;
    firstName: string;
    lastName: string;
    mobilePhone: string;
    email: string;
    idCardNumber: string;
  }[];
}

export interface Step7CompanyInfoForm {
  companyName: string;
  companyRegistration: string;
  businessType: string;
  yearsInBusiness: number;
  annualRevenue: number;
  annualProfit: number;
  numberOfEmployees: number;
}

export interface Step8ReviewForm {
  acceptTerms: boolean;
  acceptPrivacy: boolean;
  authorizedSigner: boolean;
}

// Main form state
export interface CreditApplicationForm {
  step1?: Step1PersonalInfoForm;
  step2?: Step2AddressInfoForm;
  step3?: Step3IncomeEmploymentForm;
  step4?: Step4CreditDetailsForm;
  step5?: Step5DocumentsForm;
  step6?: Step6GuarantorsForm;
  step7?: Step7CompanyInfoForm;
  step8?: Step8ReviewForm;
}

// Form navigation
export interface FormProgress {
  currentStep: number;
  completedSteps: number[];
  stepStatus: Record<number, 'INCOMPLETE' | 'COMPLETE' | 'ERROR'>;
}

export interface FormError {
  step: number;
  field: string;
  message: string;
}

// Calculated fields
export interface CalculatedFormFields {
  dtiRatio: number;
  monthlyPayment: number;
  totalInterest: number;
  riskGrade: string;
  isEligible: boolean;
  eligibilityReasons: string[];
}
