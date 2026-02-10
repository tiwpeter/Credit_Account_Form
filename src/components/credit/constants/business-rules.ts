/**
 * Business Rules
 * Credit application business validation rules
 */

import { LoanType, ApplicantType, EmploymentType } from '../types/entities';

// ============================================================================
// CREDIT POLICY RULES
// ============================================================================

export const CREDIT_POLICY = {
  // DTI Ratio
  MAX_DTI_RATIO: 0.50, // 50%
  RECOMMENDED_DTI_RATIO: 0.40, // 40%
  
  // Income Requirements
  MIN_MONTHLY_INCOME_THB: 15000,
  MIN_MONTHLY_INCOME_SME_THB: 50000,
  MIN_MONTHLY_INCOME_CORPORATE_THB: 100000,
  
  // Age Requirements
  MIN_AGE: 20,
  MAX_AGE: 65,
  MAX_AGE_AT_MATURITY: 70,
  
  // Employment
  MIN_EMPLOYMENT_MONTHS: 6,
  MIN_BUSINESS_YEARS: 2,
  
  // Credit Score
  MIN_CREDIT_SCORE: 600,
  GOOD_CREDIT_SCORE: 700,
  EXCELLENT_CREDIT_SCORE: 750,
  
  // Debt
  MAX_EXISTING_LOANS: 5,
  
  // Down Payment
  MIN_DOWN_PAYMENT_PERCENTAGE: 0.10, // 10%
} as const;

// ============================================================================
// LOAN TYPE CONFIGURATIONS
// ============================================================================

export interface LoanTypeConfig {
  type: LoanType;
  nameTh: string;
  nameEn: string;
  
  // Amount Limits
  minAmount: number;
  maxAmount: number;
  maxAmountMultiplier?: number; // Multiplier of monthly income
  
  // Tenure Limits
  minTenureMonths: number;
  maxTenureMonths: number;
  
  // Interest Rates
  baseInterestRate: number;
  minInterestRate: number;
  maxInterestRate: number;
  
  // Requirements
  requiresCollateral: boolean;
  requiresGuarantor: boolean;
  allowsCoApplicant: boolean;
  allowsCorporate: boolean;
  
  // Documents
  requiredDocuments: string[];
  
  // Special Rules
  maxDTI?: number;
  minIncome?: number;
}

export const LOAN_TYPE_CONFIGS: Record<LoanType, LoanTypeConfig> = {
  [LoanType.PERSONAL]: {
    type: LoanType.PERSONAL,
    nameTh: 'สินเชื่อส่วนบุคคล',
    nameEn: 'Personal Loan',
    minAmount: 10000,
    maxAmount: 1000000,
    maxAmountMultiplier: 5, // Max 5x monthly income
    minTenureMonths: 6,
    maxTenureMonths: 60,
    baseInterestRate: 15.0,
    minInterestRate: 12.0,
    maxInterestRate: 20.0,
    requiresCollateral: false,
    requiresGuarantor: false,
    allowsCoApplicant: true,
    allowsCorporate: false,
    requiredDocuments: ['ID_CARD', 'HOUSE_REGISTRATION', 'INCOME_PROOF', 'BANK_STATEMENT'],
    maxDTI: 0.50
  },
  
  [LoanType.HOME]: {
    type: LoanType.HOME,
    nameTh: 'สินเชื่อบ้าน',
    nameEn: 'Home Loan',
    minAmount: 500000,
    maxAmount: 20000000,
    maxAmountMultiplier: 50, // Special calculation for home loans
    minTenureMonths: 60,
    maxTenureMonths: 360,
    baseInterestRate: 5.5,
    minInterestRate: 4.0,
    maxInterestRate: 8.0,
    requiresCollateral: true,
    requiresGuarantor: false,
    allowsCoApplicant: true,
    allowsCorporate: false,
    requiredDocuments: [
      'ID_CARD',
      'HOUSE_REGISTRATION',
      'INCOME_PROOF',
      'BANK_STATEMENT',
      'COLLATERAL_DOCUMENT'
    ],
    maxDTI: 0.40
  },
  
  [LoanType.AUTO]: {
    type: LoanType.AUTO,
    nameTh: 'สินเชื่อรถยนต์',
    nameEn: 'Auto Loan',
    minAmount: 100000,
    maxAmount: 5000000,
    maxAmountMultiplier: 10,
    minTenureMonths: 12,
    maxTenureMonths: 84,
    baseInterestRate: 7.0,
    minInterestRate: 5.0,
    maxInterestRate: 10.0,
    requiresCollateral: true,
    requiresGuarantor: false,
    allowsCoApplicant: true,
    allowsCorporate: false,
    requiredDocuments: [
      'ID_CARD',
      'HOUSE_REGISTRATION',
      'INCOME_PROOF',
      'BANK_STATEMENT',
      'COLLATERAL_DOCUMENT'
    ],
    maxDTI: 0.45
  },
  
  [LoanType.SME]: {
    type: LoanType.SME,
    nameTh: 'สินเชื่อ SME',
    nameEn: 'SME Loan',
    minAmount: 100000,
    maxAmount: 10000000,
    minTenureMonths: 12,
    maxTenureMonths: 120,
    baseInterestRate: 8.0,
    minInterestRate: 6.0,
    maxInterestRate: 12.0,
    requiresCollateral: true,
    requiresGuarantor: true,
    allowsCoApplicant: false,
    allowsCorporate: true,
    requiredDocuments: [
      'ID_CARD',
      'COMPANY_REGISTRATION',
      'FINANCIAL_STATEMENT',
      'BANK_STATEMENT',
      'TAX_RETURN'
    ],
    minIncome: 50000
  },
  
  [LoanType.CORPORATE]: {
    type: LoanType.CORPORATE,
    nameTh: 'สินเชื่อองค์กร',
    nameEn: 'Corporate Loan',
    minAmount: 1000000,
    maxAmount: 100000000,
    minTenureMonths: 12,
    maxTenureMonths: 240,
    baseInterestRate: 7.0,
    minInterestRate: 5.0,
    maxInterestRate: 10.0,
    requiresCollateral: true,
    requiresGuarantor: true,
    allowsCoApplicant: false,
    allowsCorporate: true,
    requiredDocuments: [
      'COMPANY_REGISTRATION',
      'FINANCIAL_STATEMENT',
      'BANK_STATEMENT',
      'TAX_RETURN',
      'COLLATERAL_DOCUMENT'
    ],
    minIncome: 100000
  }
};

// ============================================================================
// INTEREST RATE CALCULATION
// ============================================================================

export interface InterestRateModifier {
  condition: string;
  adjustment: number; // Percentage points
}

export const INTEREST_RATE_MODIFIERS: InterestRateModifier[] = [
  // Credit Score Based
  { condition: 'CREDIT_SCORE_EXCELLENT', adjustment: -2.0 },
  { condition: 'CREDIT_SCORE_GOOD', adjustment: -1.0 },
  { condition: 'CREDIT_SCORE_FAIR', adjustment: 0.0 },
  { condition: 'CREDIT_SCORE_POOR', adjustment: 2.0 },
  
  // DTI Based
  { condition: 'DTI_VERY_LOW', adjustment: -0.5 }, // < 20%
  { condition: 'DTI_LOW', adjustment: -0.25 }, // 20-30%
  { condition: 'DTI_MODERATE', adjustment: 0.0 }, // 30-40%
  { condition: 'DTI_HIGH', adjustment: 0.5 }, // 40-50%
  
  // Employment Based
  { condition: 'PERMANENT_EMPLOYMENT', adjustment: -0.5 },
  { condition: 'CONTRACT_EMPLOYMENT', adjustment: 0.25 },
  { condition: 'SELF_EMPLOYED', adjustment: 0.5 },
  
  // Collateral Based
  { condition: 'HIGH_VALUE_COLLATERAL', adjustment: -0.75 },
  { condition: 'MEDIUM_VALUE_COLLATERAL', adjustment: -0.5 },
  { condition: 'LOW_VALUE_COLLATERAL', adjustment: 0.0 },
  
  // Relationship Based
  { condition: 'EXISTING_CUSTOMER_5_YEARS', adjustment: -0.5 },
  { condition: 'EXISTING_CUSTOMER_3_YEARS', adjustment: -0.25 },
  { condition: 'NEW_CUSTOMER', adjustment: 0.0 }
];

// ============================================================================
// VALIDATION RULES
// ============================================================================

export interface ValidationRule {
  code: string;
  message: string;
  severity: 'ERROR' | 'WARNING' | 'INFO';
  validator: (data: any) => boolean;
}

export const VALIDATION_RULES = {
  // Income Validation
  MIN_INCOME: {
    code: 'MIN_INCOME',
    message: 'Monthly income must be at least ฿15,000',
    severity: 'ERROR' as const,
    validator: (income: number) => income >= CREDIT_POLICY.MIN_MONTHLY_INCOME_THB
  },
  
  // DTI Validation
  MAX_DTI: {
    code: 'MAX_DTI',
    message: 'Debt-to-Income ratio must not exceed 50%',
    severity: 'ERROR' as const,
    validator: (dti: number) => dti <= CREDIT_POLICY.MAX_DTI_RATIO
  },
  
  DTI_WARNING: {
    code: 'DTI_WARNING',
    message: 'Debt-to-Income ratio is above recommended 40%',
    severity: 'WARNING' as const,
    validator: (dti: number) => dti > CREDIT_POLICY.RECOMMENDED_DTI_RATIO
  },
  
  // Age Validation
  MIN_AGE: {
    code: 'MIN_AGE',
    message: 'Applicant must be at least 20 years old',
    severity: 'ERROR' as const,
    validator: (age: number) => age >= CREDIT_POLICY.MIN_AGE
  },
  
  MAX_AGE: {
    code: 'MAX_AGE',
    message: 'Applicant must be under 65 years old',
    severity: 'ERROR' as const,
    validator: (age: number) => age <= CREDIT_POLICY.MAX_AGE
  },
  
  // Employment Validation
  MIN_EMPLOYMENT: {
    code: 'MIN_EMPLOYMENT',
    message: 'Minimum 6 months employment required',
    severity: 'ERROR' as const,
    validator: (months: number) => months >= CREDIT_POLICY.MIN_EMPLOYMENT_MONTHS
  }
};

// ============================================================================
// CALCULATION UTILITIES
// ============================================================================

export function calculateDTI(
  totalMonthlyIncome: number,
  totalMonthlyDebtPayment: number
): number {
  if (totalMonthlyIncome === 0) return 0;
  return totalMonthlyDebtPayment / totalMonthlyIncome;
}

export function calculateMaxLoanAmount(
  monthlyIncome: number,
  loanType: LoanType
): number {
  const config = LOAN_TYPE_CONFIGS[loanType];
  
  if (config.maxAmountMultiplier) {
    const calculatedMax = monthlyIncome * config.maxAmountMultiplier;
    return Math.min(calculatedMax, config.maxAmount);
  }
  
  return config.maxAmount;
}

export function calculateMonthlyPayment(
  principal: number,
  annualInterestRate: number,
  tenureMonths: number
): number {
  const monthlyRate = annualInterestRate / 100 / 12;
  
  if (monthlyRate === 0) {
    return principal / tenureMonths;
  }
  
  const payment = principal * 
    (monthlyRate * Math.pow(1 + monthlyRate, tenureMonths)) /
    (Math.pow(1 + monthlyRate, tenureMonths) - 1);
  
  return Math.round(payment * 100) / 100;
}

export function calculateInterestRate(
  loanType: LoanType,
  creditScore?: number,
  dti?: number,
  employmentType?: EmploymentType,
  hasCollateral?: boolean
): number {
  const config = LOAN_TYPE_CONFIGS[loanType];
  let rate = config.baseInterestRate;
  
  // Credit Score Adjustment
  if (creditScore) {
    if (creditScore >= CREDIT_POLICY.EXCELLENT_CREDIT_SCORE) {
      rate -= 2.0;
    } else if (creditScore >= CREDIT_POLICY.GOOD_CREDIT_SCORE) {
      rate -= 1.0;
    } else if (creditScore < CREDIT_POLICY.MIN_CREDIT_SCORE) {
      rate += 2.0;
    }
  }
  
  // DTI Adjustment
  if (dti) {
    if (dti < 0.20) {
      rate -= 0.5;
    } else if (dti < 0.30) {
      rate -= 0.25;
    } else if (dti > 0.40) {
      rate += 0.5;
    }
  }
  
  // Employment Type Adjustment
  if (employmentType) {
    if (employmentType === EmploymentType.PERMANENT) {
      rate -= 0.5;
    } else if (employmentType === EmploymentType.SELF_EMPLOYED) {
      rate += 0.5;
    } else if (employmentType === EmploymentType.CONTRACT) {
      rate += 0.25;
    }
  }
  
  // Collateral Adjustment
  if (hasCollateral && config.requiresCollateral) {
    rate -= 0.5;
  }
  
  // Ensure rate is within bounds
  return Math.max(
    config.minInterestRate,
    Math.min(config.maxInterestRate, rate)
  );
}

export function isEligibleForLoan(
  monthlyIncome: number,
  age: number,
  dti: number,
  loanType: LoanType,
  requestedAmount: number
): { eligible: boolean; reasons: string[] } {
  const reasons: string[] = [];
  const config = LOAN_TYPE_CONFIGS[loanType];
  
  // Income Check
  const minIncome = config.minIncome || CREDIT_POLICY.MIN_MONTHLY_INCOME_THB;
  if (monthlyIncome < minIncome) {
    reasons.push(`Monthly income must be at least ฿${minIncome.toLocaleString()}`);
  }
  
  // Age Check
  if (age < CREDIT_POLICY.MIN_AGE) {
    reasons.push(`Must be at least ${CREDIT_POLICY.MIN_AGE} years old`);
  }
  if (age > CREDIT_POLICY.MAX_AGE) {
    reasons.push(`Must be under ${CREDIT_POLICY.MAX_AGE} years old`);
  }
  
  // DTI Check
  const maxDTI = config.maxDTI || CREDIT_POLICY.MAX_DTI_RATIO;
  if (dti > maxDTI) {
    reasons.push(`Debt-to-Income ratio (${(dti * 100).toFixed(1)}%) exceeds maximum ${(maxDTI * 100).toFixed(0)}%`);
  }
  
  // Loan Amount Check
  if (requestedAmount < config.minAmount) {
    reasons.push(`Minimum loan amount is ฿${config.minAmount.toLocaleString()}`);
  }
  
  const maxAmount = calculateMaxLoanAmount(monthlyIncome, loanType);
  if (requestedAmount > maxAmount) {
    reasons.push(`Maximum loan amount for your income is ฿${maxAmount.toLocaleString()}`);
  }
  
  return {
    eligible: reasons.length === 0,
    reasons
  };
}

// ============================================================================
// DOCUMENT REQUIREMENTS
// ============================================================================

export function getRequiredDocuments(
  applicantType: ApplicantType,
  loanType: LoanType
): string[] {
  const config = LOAN_TYPE_CONFIGS[loanType];
  
  if (applicantType === ApplicantType.CORPORATE) {
    return config.requiredDocuments.filter(doc => 
      !doc.includes('ID_CARD') || doc.includes('COMPANY')
    );
  }
  
  return config.requiredDocuments;
}

// ============================================================================
// RISK GRADING
// ============================================================================

export function calculateRiskGrade(
  creditScore?: number,
  dti?: number,
  loanToValue?: number,
  yearsEmployed?: number
): string {
  let score = 0;
  
  // Credit Score (40 points)
  if (creditScore) {
    if (creditScore >= 750) score += 40;
    else if (creditScore >= 700) score += 30;
    else if (creditScore >= 650) score += 20;
    else if (creditScore >= 600) score += 10;
  }
  
  // DTI (30 points)
  if (dti !== undefined) {
    if (dti <= 0.20) score += 30;
    else if (dti <= 0.30) score += 25;
    else if (dti <= 0.40) score += 15;
    else if (dti <= 0.50) score += 5;
  }
  
  // LTV (20 points)
  if (loanToValue !== undefined) {
    if (loanToValue <= 0.50) score += 20;
    else if (loanToValue <= 0.70) score += 15;
    else if (loanToValue <= 0.80) score += 10;
    else if (loanToValue <= 0.90) score += 5;
  }
  
  // Employment (10 points)
  if (yearsEmployed !== undefined) {
    if (yearsEmployed >= 5) score += 10;
    else if (yearsEmployed >= 3) score += 7;
    else if (yearsEmployed >= 1) score += 5;
    else if (yearsEmployed >= 0.5) score += 2;
  }
  
  // Grade Assignment
  if (score >= 90) return 'AAA';
  if (score >= 80) return 'AA';
  if (score >= 70) return 'A';
  if (score >= 60) return 'BBB';
  if (score >= 50) return 'BB';
  if (score >= 40) return 'B';
  if (score >= 30) return 'CCC';
  if (score >= 20) return 'CC';
  if (score >= 10) return 'C';
  return 'D';
}
