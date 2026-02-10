/**
 * Validation Schemas
 * Zod schemas for all form steps
 */

import { z } from 'zod';
import {
  ApplicantType,
  Gender,
  MaritalStatus,
  EmploymentType,
  IncomeType,
  AddressType,
  LoanType,
  DocumentType,
  RelationType,
  PaymentFrequency
} from '../types/entities';

// ============================================================================
// CUSTOM VALIDATORS
// ============================================================================

const thaiIdCardRegex = /^[1-8]\d{12}$/;
const thaiPhoneRegex = /^0[0-9]{9}$/;
const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

// ============================================================================
// STEP 1: PERSONAL INFORMATION
// ============================================================================

export const step1Schema = z.object({
  applicantType: z.nativeEnum(ApplicantType, {
    errorMap: () => ({ message: 'Please select applicant type' })
  }),
  
  titleTh: z.string().min(1, 'Please select title'),
  
  firstNameTh: z.string()
    .min(1, 'First name is required')
    .max(100, 'First name must not exceed 100 characters'),
  
  lastNameTh: z.string()
    .min(1, 'Last name is required')
    .max(100, 'Last name must not exceed 100 characters'),
  
  firstNameEn: z.string()
    .max(100, 'First name must not exceed 100 characters')
    .optional(),
  
  lastNameEn: z.string()
    .max(100, 'Last name must not exceed 100 characters')
    .optional(),
  
  idCardNumber: z.string()
    .regex(thaiIdCardRegex, 'Invalid ID card number (13 digits)')
    .refine(
      (val) => {
        // Luhn algorithm for Thai ID validation
        const digits = val.split('').map(Number);
        let sum = 0;
        for (let i = 0; i < 12; i++) {
          sum += digits[i] * (13 - i);
        }
        const checkDigit = (11 - (sum % 11)) % 10;
        return checkDigit === digits[12];
      },
      { message: 'Invalid ID card number checksum' }
    ),
  
  passportNumber: z.string()
    .max(20, 'Passport number must not exceed 20 characters')
    .optional(),
  
  dateOfBirth: z.string()
    .min(1, 'Date of birth is required')
    .refine(
      (val) => {
        const date = new Date(val);
        const age = (Date.now() - date.getTime()) / (1000 * 60 * 60 * 24 * 365);
        return age >= 20 && age <= 65;
      },
      { message: 'Age must be between 20 and 65 years' }
    ),
  
  gender: z.nativeEnum(Gender, {
    errorMap: () => ({ message: 'Please select gender' })
  }),
  
  nationality: z.string().min(1, 'Please select nationality'),
  
  maritalStatus: z.nativeEnum(MaritalStatus, {
    errorMap: () => ({ message: 'Please select marital status' })
  }),
  
  mobilePhone: z.string()
    .regex(thaiPhoneRegex, 'Invalid mobile phone number (10 digits starting with 0)'),
  
  homePhone: z.string()
    .regex(thaiPhoneRegex, 'Invalid home phone number')
    .optional()
    .or(z.literal('')),
  
  email: z.string()
    .regex(emailRegex, 'Invalid email address'),
  
  lineId: z.string()
    .max(50, 'LINE ID must not exceed 50 characters')
    .optional(),
  
  educationLevel: z.string().optional(),
  
  numberOfDependents: z.number()
    .int('Number of dependents must be a whole number')
    .min(0, 'Number of dependents cannot be negative')
    .max(20, 'Number of dependents seems unreasonably high')
});

export type Step1FormData = z.infer<typeof step1Schema>;

// ============================================================================
// STEP 2: ADDRESS INFORMATION
// ============================================================================

const addressSchema = z.object({
  type: z.nativeEnum(AddressType),
  
  houseNumber: z.string()
    .min(1, 'House number is required')
    .max(20, 'House number must not exceed 20 characters'),
  
  moo: z.string()
    .max(10)
    .optional(),
  
  village: z.string()
    .max(100)
    .optional(),
  
  soi: z.string()
    .max(100)
    .optional(),
  
  street: z.string()
    .max(100)
    .optional(),
  
  subDistrict: z.string()
    .min(1, 'Sub-district is required')
    .max(100),
  
  district: z.string()
    .min(1, 'District is required')
    .max(100),
  
  province: z.string()
    .min(1, 'Province is required')
    .max(100),
  
  postalCode: z.string()
    .regex(/^\d{5}$/, 'Postal code must be 5 digits'),
  
  country: z.string()
    .min(1, 'Country is required')
    .default('Thailand'),
  
  yearsAtAddress: z.number()
    .int()
    .min(0)
    .max(100)
    .optional(),
  
  monthsAtAddress: z.number()
    .int()
    .min(0)
    .max(11)
    .optional(),
  
  ownershipStatus: z.enum(['OWNED', 'RENTED', 'FAMILY_OWNED', 'COMPANY_PROVIDED'])
    .optional(),
  
  monthlyRent: z.number()
    .min(0)
    .optional()
});

export const step2Schema = z.object({
  currentAddress: addressSchema,
  
  sameAsCurrentAddress: z.boolean().default(false),
  
  permanentAddress: addressSchema,
  
  workAddress: addressSchema.optional()
}).refine(
  (data) => {
    if (data.currentAddress.ownershipStatus === 'RENTED') {
      return data.currentAddress.monthlyRent !== undefined && data.currentAddress.monthlyRent > 0;
    }
    return true;
  },
  {
    message: 'Monthly rent is required for rented property',
    path: ['currentAddress', 'monthlyRent']
  }
);

export type Step2FormData = z.infer<typeof step2Schema>;

// ============================================================================
// STEP 3: INCOME & EMPLOYMENT
// ============================================================================

const additionalIncomeSchema = z.object({
  id: z.string(),
  type: z.nativeEnum(IncomeType),
  amount: z.number()
    .min(0, 'Amount must be positive')
    .max(10000000, 'Amount seems unreasonably high'),
  source: z.string()
    .max(200)
    .optional()
});

const existingLoanSchema = z.object({
  id: z.string(),
  lender: z.string()
    .min(1, 'Lender name is required')
    .max(200),
  loanType: z.string()
    .min(1, 'Loan type is required'),
  outstandingBalance: z.number()
    .min(0, 'Outstanding balance must be positive'),
  monthlyPayment: z.number()
    .min(0, 'Monthly payment must be positive'),
  remainingTenure: z.number()
    .int()
    .min(0, 'Remaining tenure must be positive'),
  interestRate: z.number()
    .min(0, 'Interest rate must be positive')
    .max(100, 'Interest rate must be less than 100%')
});

export const step3Schema = z.object({
  employmentType: z.nativeEnum(EmploymentType, {
    errorMap: () => ({ message: 'Please select employment type' })
  }),
  
  employerName: z.string()
    .max(200)
    .optional(),
  
  position: z.string()
    .max(100)
    .optional(),
  
  yearsEmployed: z.number()
    .int()
    .min(0)
    .max(50)
    .optional(),
  
  monthsEmployed: z.number()
    .int()
    .min(0)
    .max(11)
    .optional(),
  
  primaryIncomeType: z.nativeEnum(IncomeType, {
    errorMap: () => ({ message: 'Please select income type' })
  }),
  
  monthlyIncome: z.number()
    .min(15000, 'Monthly income must be at least ฿15,000')
    .max(10000000, 'Income seems unreasonably high'),
  
  hasAdditionalIncome: z.boolean().default(false),
  
  additionalIncomes: z.array(additionalIncomeSchema).optional(),
  
  businessName: z.string()
    .max(200)
    .optional(),
  
  businessType: z.string()
    .max(100)
    .optional(),
  
  businessRegistrationNumber: z.string()
    .max(50)
    .optional(),
  
  monthlyExpenses: z.number()
    .min(0, 'Monthly expenses must be positive')
    .max(10000000),
  
  monthlyRent: z.number()
    .min(0)
    .optional(),
  
  otherMonthlyExpenses: z.number()
    .min(0)
    .optional(),
  
  hasExistingLoans: z.boolean().default(false),
  
  existingLoans: z.array(existingLoanSchema).optional()
}).refine(
  (data) => {
    if ([EmploymentType.PERMANENT, EmploymentType.CONTRACT].includes(data.employmentType)) {
      return data.employerName && data.position;
    }
    return true;
  },
  {
    message: 'Employer name and position are required for employed applicants',
    path: ['employerName']
  }
).refine(
  (data) => {
    if ([EmploymentType.SELF_EMPLOYED, EmploymentType.BUSINESS_OWNER].includes(data.employmentType)) {
      return data.businessName;
    }
    return true;
  },
  {
    message: 'Business name is required for business owners',
    path: ['businessName']
  }
).refine(
  (data) => {
    const totalIncome = data.monthlyIncome + 
      (data.additionalIncomes?.reduce((sum, inc) => sum + inc.amount, 0) || 0);
    const totalDebt = data.existingLoans?.reduce((sum, loan) => sum + loan.monthlyPayment, 0) || 0;
    const dti = totalDebt / totalIncome;
    return dti <= 0.50;
  },
  {
    message: 'Debt-to-Income ratio must not exceed 50%',
    path: ['existingLoans']
  }
);

export type Step3FormData = z.infer<typeof step3Schema>;

// ============================================================================
// STEP 4: CREDIT DETAILS
// ============================================================================

const coApplicantSchema = z.object({
  titleTh: z.string().min(1, 'Title is required'),
  firstNameTh: z.string().min(1, 'First name is required'),
  lastNameTh: z.string().min(1, 'Last name is required'),
  idCardNumber: z.string().regex(thaiIdCardRegex, 'Invalid ID card number'),
  relationship: z.string().min(1, 'Relationship is required'),
  mobilePhone: z.string().regex(thaiPhoneRegex, 'Invalid phone number'),
  email: z.string().regex(emailRegex).optional(),
  monthlyIncome: z.number().min(0).optional()
});

export const step4Schema = z.object({
  loanType: z.nativeEnum(LoanType, {
    errorMap: () => ({ message: 'Please select loan type' })
  }),
  
  requestedAmount: z.number()
    .min(10000, 'Minimum loan amount is ฿10,000')
    .max(100000000, 'Maximum loan amount is ฿100,000,000'),
  
  requestedTenure: z.number()
    .int('Tenure must be a whole number')
    .min(6, 'Minimum tenure is 6 months')
    .max(360, 'Maximum tenure is 360 months'),
  
  purpose: z.string()
    .min(1, 'Loan purpose is required'),
  
  purposeDetails: z.string()
    .max(500)
    .optional(),
  
  preferredPaymentFrequency: z.nativeEnum(PaymentFrequency),
  
  preferredPaymentDate: z.number()
    .int()
    .min(1)
    .max(31)
    .optional(),
  
  hasCollateral: z.boolean().default(false),
  
  collateralType: z.string().optional(),
  
  collateralDescription: z.string()
    .max(500)
    .optional(),
  
  estimatedCollateralValue: z.number()
    .min(0)
    .optional(),
  
  hasCoApplicant: z.boolean().default(false),
  
  coApplicant: coApplicantSchema.optional()
}).refine(
  (data) => {
    if (data.hasCollateral) {
      return data.collateralType && data.collateralDescription && data.estimatedCollateralValue;
    }
    return true;
  },
  {
    message: 'Collateral details are required when collateral is provided',
    path: ['collateralType']
  }
).refine(
  (data) => {
    if (data.hasCoApplicant) {
      return data.coApplicant !== undefined;
    }
    return true;
  },
  {
    message: 'Co-applicant information is required',
    path: ['coApplicant']
  }
);

export type Step4FormData = z.infer<typeof step4Schema>;

// ============================================================================
// STEP 5: DOCUMENTS
// ============================================================================

const documentSchema = z.object({
  id: z.string(),
  type: z.nativeEnum(DocumentType),
  name: z.string().min(1),
  file: z.instanceof(File).optional(),
  fileUrl: z.string().optional(),
  status: z.enum(['pending', 'uploading', 'completed', 'error']),
  isMandatory: z.boolean(),
  uploadProgress: z.number().min(0).max(100).optional(),
  error: z.string().optional()
});

export const step5Schema = z.object({
  documents: z.array(documentSchema)
    .min(1, 'At least one document is required')
}).refine(
  (data) => {
    const mandatoryDocs = data.documents.filter(doc => doc.isMandatory);
    const uploadedMandatory = mandatoryDocs.filter(doc => 
      doc.status === 'completed' && (doc.file || doc.fileUrl)
    );
    return uploadedMandatory.length === mandatoryDocs.length;
  },
  {
    message: 'All mandatory documents must be uploaded',
    path: ['documents']
  }
);

export type Step5FormData = z.infer<typeof step5Schema>;

// ============================================================================
// STEP 6: GUARANTORS & REFERENCES
// ============================================================================

const guarantorSchema = z.object({
  id: z.string(),
  relationType: z.nativeEnum(RelationType),
  titleTh: z.string().min(1, 'Title is required'),
  firstNameTh: z.string().min(1, 'First name is required'),
  lastNameTh: z.string().min(1, 'Last name is required'),
  idCardNumber: z.string().regex(thaiIdCardRegex, 'Invalid ID card number'),
  mobilePhone: z.string().regex(thaiPhoneRegex, 'Invalid phone number'),
  email: z.string().regex(emailRegex).optional(),
  relationshipToApplicant: z.string().min(1, 'Relationship is required'),
  yearsKnown: z.number()
    .int()
    .min(0, 'Years known must be positive')
    .max(100),
  occupation: z.string().optional(),
  monthlyIncome: z.number().min(0).optional(),
  employer: z.string().optional(),
  address: addressSchema.optional()
});

const referenceSchema = z.object({
  id: z.string(),
  name: z.string()
    .min(1, 'Name is required')
    .max(200),
  relationship: z.string()
    .min(1, 'Relationship is required'),
  phoneNumber: z.string()
    .regex(thaiPhoneRegex, 'Invalid phone number'),
  email: z.string()
    .regex(emailRegex)
    .optional()
});

export const step6Schema = z.object({
  hasGuarantor: z.boolean().default(false),
  
  guarantors: z.array(guarantorSchema).optional(),
  
  references: z.array(referenceSchema)
    .min(2, 'At least 2 references are required')
    .max(5, 'Maximum 5 references allowed')
}).refine(
  (data) => {
    if (data.hasGuarantor) {
      return data.guarantors && data.guarantors.length > 0;
    }
    return true;
  },
  {
    message: 'At least one guarantor is required',
    path: ['guarantors']
  }
);

export type Step6FormData = z.infer<typeof step6Schema>;

// ============================================================================
// STEP 7: COMPANY INFORMATION (Corporate Only)
// ============================================================================

const directorSchema = z.object({
  id: z.string(),
  titleTh: z.string().min(1),
  firstNameTh: z.string().min(1),
  lastNameTh: z.string().min(1),
  idCardNumber: z.string().regex(thaiIdCardRegex),
  position: z.string().min(1),
  isAuthorizedSignatory: z.boolean(),
  sharePercentage: z.number().min(0).max(100).optional()
});

const shareholderSchema = z.object({
  id: z.string(),
  name: z.string().min(1),
  idCardNumber: z.string().optional(),
  taxId: z.string().optional(),
  sharePercentage: z.number()
    .min(0, 'Share percentage must be positive')
    .max(100, 'Share percentage cannot exceed 100%'),
  numberOfShares: z.number().int().min(1)
});

export const step7Schema = z.object({
  companyNameTh: z.string().min(1, 'Company name (Thai) is required'),
  companyNameEn: z.string().optional(),
  
  registrationNumber: z.string()
    .min(1, 'Company registration number is required')
    .max(50),
  
  taxId: z.string()
    .min(13, 'Tax ID must be 13 digits')
    .max(13)
    .regex(/^\d{13}$/, 'Tax ID must be 13 digits'),
  
  businessType: z.string().min(1, 'Business type is required'),
  industryCode: z.string().min(1, 'Industry code is required'),
  
  establishedDate: z.string().min(1, 'Established date is required'),
  
  numberOfEmployees: z.number()
    .int()
    .min(1, 'Number of employees must be at least 1'),
  
  paidUpCapital: z.number()
    .min(0, 'Paid-up capital must be positive'),
  
  registeredCapital: z.number()
    .min(0, 'Registered capital must be positive'),
  
  phoneNumber: z.string()
    .regex(thaiPhoneRegex, 'Invalid phone number'),
  
  email: z.string()
    .regex(emailRegex, 'Invalid email address'),
  
  website: z.string()
    .url('Invalid website URL')
    .optional()
    .or(z.literal('')),
  
  annualRevenue: z.number()
    .min(0, 'Annual revenue must be positive'),
  
  annualProfit: z.number().optional(),
  totalAssets: z.number().min(0).optional(),
  totalLiabilities: z.number().min(0).optional(),
  
  companyAddress: addressSchema,
  
  directors: z.array(directorSchema)
    .min(1, 'At least one director is required'),
  
  shareholders: z.array(shareholderSchema)
    .min(1, 'At least one shareholder is required')
}).refine(
  (data) => data.paidUpCapital <= data.registeredCapital,
  {
    message: 'Paid-up capital cannot exceed registered capital',
    path: ['paidUpCapital']
  }
).refine(
  (data) => {
    const totalShares = data.shareholders.reduce((sum, s) => sum + s.sharePercentage, 0);
    return Math.abs(totalShares - 100) < 0.01; // Allow for rounding errors
  },
  {
    message: 'Total share percentage must equal 100%',
    path: ['shareholders']
  }
);

export type Step7FormData = z.infer<typeof step7Schema>;

// ============================================================================
// STEP 8: REVIEW & SUBMIT
// ============================================================================

export const step8Schema = z.object({
  agreeToTerms: z.literal(true, {
    errorMap: () => ({ message: 'You must agree to the terms and conditions' })
  }),
  
  agreeToPrivacyPolicy: z.literal(true, {
    errorMap: () => ({ message: 'You must agree to the privacy policy' })
  }),
  
  agreeToDataSharing: z.literal(true, {
    errorMap: () => ({ message: 'You must agree to data sharing for credit check' })
  }),
  
  confirmInformationAccuracy: z.literal(true, {
    errorMap: () => ({ message: 'You must confirm the information is accurate' })
  }),
  
  signature: z.string()
    .min(10, 'Signature is required')
    .optional(),
  
  signedAt: z.string().optional(),
  
  additionalComments: z.string()
    .max(1000, 'Comments must not exceed 1000 characters')
    .optional()
});

export type Step8FormData = z.infer<typeof step8Schema>;

// ============================================================================
// COMPLETE FORM SCHEMA
// ============================================================================

export const completeFormSchema = z.object({
  step1: step1Schema,
  step2: step2Schema,
  step3: step3Schema,
  step4: step4Schema,
  step5: step5Schema,
  step6: step6Schema,
  step7: step7Schema.optional(),
  step8: step8Schema
});

export type CompleteFormData = z.infer<typeof completeFormSchema>;
