// Financial calculations and eligibility checks

import {
  BUSINESS_RULES,
  INTEREST_RATE_TABLE,
  RISK_GRADE_BY_DTI,
  LOAN_TYPE_CONFIGS,
} from './business-rules';

/**
 * Calculate Debt-to-Income (DTI) ratio
 * DTI = (Total Monthly Debt Obligations) / (Monthly Income) * 100
 */
export const calculateDTI = (
  monthlyIncome: number,
  monthlyDebtObligation: number
): number => {
  if (monthlyIncome <= 0) return 0;
  return (monthlyDebtObligation / monthlyIncome) * 100;
};

/**
 * Determine risk grade based on DTI ratio
 */
export const getRiskGradeByDTI = (
  dti: number
): 'EXCELLENT' | 'GOOD' | 'ACCEPTABLE' | 'FAIR' | 'POOR' => {
  const ranges = RISK_GRADE_BY_DTI;

  if (dti <= ranges.EXCELLENT.max * 100) return 'EXCELLENT';
  if (dti <= ranges.GOOD.max * 100) return 'GOOD';
  if (dti <= ranges.ACCEPTABLE.max * 100) return 'ACCEPTABLE';
  if (dti <= ranges.FAIR.max * 100) return 'FAIR';
  return 'POOR';
};

/**
 * Calculate monthly payment using amortization formula
 * M = P * [r(1+r)^n] / [(1+r)^n - 1]
 * Where: M = Monthly Payment, P = Principal, r = Monthly Rate, n = Number of Months
 */
export const calculateMonthlyPayment = (
  principal: number,
  annualRate: number,
  tenorMonths: number
): number => {
  const monthlyRate = annualRate / 12;

  if (monthlyRate === 0) {
    return principal / tenorMonths;
  }

  const numerator = monthlyRate * Math.pow(1 + monthlyRate, tenorMonths);
  const denominator = Math.pow(1 + monthlyRate, tenorMonths) - 1;

  return (principal * numerator) / denominator;
};

/**
 * Calculate total interest paid over loan tenure
 */
export const calculateTotalInterest = (
  monthlyPayment: number,
  tenorMonths: number,
  principal: number
): number => {
  return monthlyPayment * tenorMonths - principal;
};

/**
 * Calculate effective interest rate based on risk grade
 */
export const getInterestRateByRiskGrade = (
  riskGrade: string
): number => {
  const rates = INTEREST_RATE_TABLE as Record<string, { min: number; max: number }>;
  const rateRange = rates[riskGrade] || rates.POOR;

  // Use average of min and max
  return (rateRange.min + rateRange.max) / 2;
};

/**
 * Generate amortization schedule
 */
export const generateAmortizationSchedule = (
  principal: number,
  annualRate: number,
  tenorMonths: number
): {
  month: number;
  payment: number;
  principal: number;
  interest: number;
  balance: number;
}[] => {
  const schedule = [];
  const monthlyRate = annualRate / 12;
  const monthlyPayment = calculateMonthlyPayment(principal, annualRate, tenorMonths);

  let balance = principal;

  for (let month = 1; month <= tenorMonths; month++) {
    const interest = balance * monthlyRate;
    const principalPayment = monthlyPayment - interest;
    balance -= principalPayment;

    schedule.push({
      month,
      payment: monthlyPayment,
      principal: Math.max(principalPayment, 0),
      interest: Math.max(interest, 0),
      balance: Math.max(balance, 0),
    });
  }

  return schedule;
};

/**
 * Check eligibility based on income and DTI
 */
export const checkEligibility = (
  monthlyIncome: number,
  monthlyDebtObligation: number,
  age: number,
  employmentMonths: number,
  loanType: string
): {
  isEligible: boolean;
  reasons: string[];
} => {
  const reasons: string[] = [];

  // Age check
  if (age < BUSINESS_RULES.AGE.MIN || age > BUSINESS_RULES.AGE.MAX) {
    reasons.push(
      `Age must be between ${BUSINESS_RULES.AGE.MIN} and ${BUSINESS_RULES.AGE.MAX} years`
    );
  }

  // Income check
  if (monthlyIncome < BUSINESS_RULES.DTI.MIN_MONTHLY_INCOME) {
    reasons.push(
      `Minimum monthly income required: ฿${BUSINESS_RULES.DTI.MIN_MONTHLY_INCOME.toLocaleString()}`
    );
  }

  // Employment check
  if (employmentMonths < BUSINESS_RULES.EMPLOYMENT.MIN_MONTHS) {
    reasons.push(
      `Minimum employment period required: ${BUSINESS_RULES.EMPLOYMENT.MIN_MONTHS} months`
    );
  }

  // DTI check
  const dti = calculateDTI(monthlyIncome, monthlyDebtObligation);
  const maxDti = LOAN_TYPE_CONFIGS[loanType]?.maxDti || BUSINESS_RULES.DTI.MAX_RATIO;

  if (dti > maxDti * 100) {
    reasons.push(
      `DTI ratio ${dti.toFixed(2)}% exceeds maximum allowed ${(maxDti * 100).toFixed(2)}%`
    );
  }

  return {
    isEligible: reasons.length === 0,
    reasons,
  };
};

/**
 * Calculate age from date of birth
 */
export const calculateAge = (dateOfBirth: string): number => {
  const today = new Date();
  const birthDate = new Date(dateOfBirth);

  let age = today.getFullYear() - birthDate.getFullYear();
  const monthDiff = today.getMonth() - birthDate.getMonth();

  if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
    age--;
  }

  return age;
};

/**
 * Calculate months between two dates
 */
export const calculateMonths = (startDate: string, endDate: string = new Date().toISOString()): number => {
  const start = new Date(startDate);
  const end = new Date(endDate);

  let months = (end.getFullYear() - start.getFullYear()) * 12;
  months -= start.getMonth();
  months += end.getMonth();

  return months <= 0 ? 0 : months;
};

/**
 * Validate loan amount against loan type limits
 */
export const validateLoanAmount = (
  loanAmount: number,
  loanType: string
): { isValid: boolean; message?: string } => {
  const config = LOAN_TYPE_CONFIGS[loanType];

  if (!config) {
    return { isValid: false, message: 'Invalid loan type' };
  }

  if (loanAmount < config.minAmount) {
    return {
      isValid: false,
      message: `Minimum loan amount for ${config.name}: ฿${config.minAmount.toLocaleString()}`,
    };
  }

  if (loanAmount > config.maxAmount) {
    return {
      isValid: false,
      message: `Maximum loan amount for ${config.name}: ฿${config.maxAmount.toLocaleString()}`,
    };
  }

  return { isValid: true };
};

/**
 * Validate tenor against loan type limits
 */
export const validateTenor = (
  tenorMonths: number,
  loanType: string
): { isValid: boolean; message?: string } => {
  const config = LOAN_TYPE_CONFIGS[loanType];

  if (!config) {
    return { isValid: false, message: 'Invalid loan type' };
  }

  if (tenorMonths < config.minTenor) {
    return {
      isValid: false,
      message: `Minimum tenor for ${config.name}: ${config.minTenor} months`,
    };
  }

  if (tenorMonths > config.maxTenor) {
    return {
      isValid: false,
      message: `Maximum tenor for ${config.name}: ${config.maxTenor} months`,
    };
  }

  return { isValid: true };
};

/**
 * Calculate maximum loan amount based on income and DTI
 */
export const calculateMaximumLoanAmount = (
  monthlyIncome: number,
  monthlyDebtObligation: number,
  maxDtiRatio: number = 0.50
): number => {
  const maxMonthlyDebt = monthlyIncome * maxDtiRatio;
  const maxNewDebt = maxMonthlyDebt - monthlyDebtObligation;

  // Using average rate and 60-month tenor for estimation
  const avgMonthlyRate = 0.004; // ~4.8% annual
  const tenor = 60;

  // Rearrange: M = P * [r(1+r)^n] / [(1+r)^n - 1]
  // To get: P = M * [(1+r)^n - 1] / [r(1+r)^n]
  const numerator = Math.pow(1 + avgMonthlyRate, tenor) - 1;
  const denominator = avgMonthlyRate * Math.pow(1 + avgMonthlyRate, tenor);

  const maxLoan = maxNewDebt * (numerator / denominator);

  return Math.max(0, maxLoan);
};

/**
 * Format currency in Thai Baht
 */
export const formatBaht = (amount: number): string => {
  return new Intl.NumberFormat('th-TH', {
    style: 'currency',
    currency: 'THB',
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
  }).format(amount);
};

/**
 * Format percentage
 */
export const formatPercentage = (value: number, decimals: number = 2): string => {
  return `${value.toFixed(decimals)}%`;
};
