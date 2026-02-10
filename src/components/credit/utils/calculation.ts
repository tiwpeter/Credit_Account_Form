/**
 * Calculation Utilities
 * Financial calculations for credit application
 */

import { CREDIT_POLICY, calculateDTI as calcDTI, calculateMonthlyPayment as calcPayment, calculateInterestRate as calcRate } from '../constants/business-rules';
import type { LoanType, EmploymentType } from '../types/entities';

// ============================================================================
// DTI CALCULATION
// ============================================================================

export interface DTICalculationInput {
  monthlyIncome: number;
  additionalIncomes?: Array<{ amount: number }>;
  monthlyExpenses: number;
  existingLoans?: Array<{ monthlyPayment: number }>;
  monthlyRent?: number;
}

export interface DTICalculationResult {
  totalMonthlyIncome: number;
  totalMonthlyExpenses: number;
  totalMonthlyDebtPayment: number;
  netMonthlyIncome: number;
  debtToIncomeRatio: number;
  isWithinLimit: boolean;
  remainingCapacity: number;
}

export function calculateDTI(input: DTICalculationInput): DTICalculationResult {
  const additionalIncome = input.additionalIncomes?.reduce((sum, inc) => sum + inc.amount, 0) || 0;
  const totalMonthlyIncome = input.monthlyIncome + additionalIncome;
  
  const existingDebtPayment = input.existingLoans?.reduce((sum, loan) => sum + loan.monthlyPayment, 0) || 0;
  const totalMonthlyDebtPayment = existingDebtPayment;
  
  const totalMonthlyExpenses = input.monthlyExpenses + (input.monthlyRent || 0);
  
  const netMonthlyIncome = totalMonthlyIncome - totalMonthlyExpenses - totalMonthlyDebtPayment;
  
  const dti = totalMonthlyIncome > 0 ? totalMonthlyDebtPayment / totalMonthlyIncome : 0;
  
  const maxAllowedDebt = totalMonthlyIncome * CREDIT_POLICY.MAX_DTI_RATIO;
  const remainingCapacity = Math.max(0, maxAllowedDebt - totalMonthlyDebtPayment);
  
  return {
    totalMonthlyIncome,
    totalMonthlyExpenses,
    totalMonthlyDebtPayment,
    netMonthlyIncome,
    debtToIncomeRatio: dti,
    isWithinLimit: dti <= CREDIT_POLICY.MAX_DTI_RATIO,
    remainingCapacity
  };
}

// ============================================================================
// LOAN ELIGIBILITY
// ============================================================================

export interface EligibilityCheckInput {
  monthlyIncome: number;
  age: number;
  dtiRatio: number;
  loanType: LoanType;
  requestedAmount: number;
  requestedTenure: number;
  employmentType: EmploymentType;
  yearsEmployed?: number;
  creditScore?: number;
}

export interface EligibilityCheckResult {
  isEligible: boolean;
  issues: Array<{
    code: string;
    message: string;
    severity: 'error' | 'warning';
  }>;
  recommendations: string[];
  maxLoanAmount: number;
  estimatedInterestRate: number;
  estimatedMonthlyPayment: number;
}

export function checkEligibility(input: EligibilityCheckInput): EligibilityCheckResult {
  const issues: EligibilityCheckResult['issues'] = [];
  const recommendations: string[] = [];
  
  // Age Check
  if (input.age < CREDIT_POLICY.MIN_AGE) {
    issues.push({
      code: 'AGE_TOO_LOW',
      message: `You must be at least ${CREDIT_POLICY.MIN_AGE} years old`,
      severity: 'error'
    });
  }
  
  if (input.age > CREDIT_POLICY.MAX_AGE) {
    issues.push({
      code: 'AGE_TOO_HIGH',
      message: `Maximum age is ${CREDIT_POLICY.MAX_AGE} years`,
      severity: 'error'
    });
  }
  
  const ageAtMaturity = input.age + (input.requestedTenure / 12);
  if (ageAtMaturity > CREDIT_POLICY.MAX_AGE_AT_MATURITY) {
    issues.push({
      code: 'AGE_AT_MATURITY_EXCEEDED',
      message: `Age at loan maturity (${Math.floor(ageAtMaturity)}) would exceed maximum ${CREDIT_POLICY.MAX_AGE_AT_MATURITY}`,
      severity: 'error'
    });
    recommendations.push(`Consider reducing tenure to ${Math.floor((CREDIT_POLICY.MAX_AGE_AT_MATURITY - input.age) * 12)} months`);
  }
  
  // Income Check
  if (input.monthlyIncome < CREDIT_POLICY.MIN_MONTHLY_INCOME_THB) {
    issues.push({
      code: 'INCOME_TOO_LOW',
      message: `Monthly income must be at least ฿${CREDIT_POLICY.MIN_MONTHLY_INCOME_THB.toLocaleString()}`,
      severity: 'error'
    });
  }
  
  // DTI Check
  if (input.dtiRatio > CREDIT_POLICY.MAX_DTI_RATIO) {
    issues.push({
      code: 'DTI_EXCEEDED',
      message: `Debt-to-Income ratio (${(input.dtiRatio * 100).toFixed(1)}%) exceeds maximum ${(CREDIT_POLICY.MAX_DTI_RATIO * 100)}%`,
      severity: 'error'
    });
    recommendations.push('Consider reducing existing debt or increasing income');
  } else if (input.dtiRatio > CREDIT_POLICY.RECOMMENDED_DTI_RATIO) {
    issues.push({
      code: 'DTI_HIGH',
      message: `Debt-to-Income ratio (${(input.dtiRatio * 100).toFixed(1)}%) is above recommended ${(CREDIT_POLICY.RECOMMENDED_DTI_RATIO * 100)}%`,
      severity: 'warning'
    });
  }
  
  // Employment Check
  if (input.employmentType === EmploymentType.PERMANENT || input.employmentType === EmploymentType.CONTRACT) {
    const totalMonths = (input.yearsEmployed || 0) * 12;
    if (totalMonths < CREDIT_POLICY.MIN_EMPLOYMENT_MONTHS) {
      issues.push({
        code: 'EMPLOYMENT_TOO_SHORT',
        message: `Minimum ${CREDIT_POLICY.MIN_EMPLOYMENT_MONTHS} months employment required`,
        severity: 'error'
      });
    }
  }
  
  // Credit Score Check
  if (input.creditScore && input.creditScore < CREDIT_POLICY.MIN_CREDIT_SCORE) {
    issues.push({
      code: 'CREDIT_SCORE_LOW',
      message: `Credit score (${input.creditScore}) is below minimum ${CREDIT_POLICY.MIN_CREDIT_SCORE}`,
      severity: 'error'
    });
    recommendations.push('Work on improving your credit score before applying');
  }
  
  // Calculate max loan amount and rates
  const maxLoanAmount = calculateMaxLoanAmount(input.monthlyIncome, input.loanType);
  const estimatedRate = calcRate(
    input.loanType,
    input.creditScore,
    input.dtiRatio,
    input.employmentType,
    false
  );
  const estimatedPayment = calcPayment(
    input.requestedAmount,
    estimatedRate,
    input.requestedTenure
  );
  
  // Amount Check
  if (input.requestedAmount > maxLoanAmount) {
    issues.push({
      code: 'AMOUNT_EXCEEDED',
      message: `Requested amount (฿${input.requestedAmount.toLocaleString()}) exceeds maximum (฿${maxLoanAmount.toLocaleString()}) for your income`,
      severity: 'error'
    });
    recommendations.push(`Consider requesting up to ฿${maxLoanAmount.toLocaleString()}`);
  }
  
  // Payment Check
  const paymentToIncomeRatio = estimatedPayment / input.monthlyIncome;
  if (paymentToIncomeRatio > 0.40) {
    issues.push({
      code: 'PAYMENT_TOO_HIGH',
      message: `Estimated monthly payment (฿${estimatedPayment.toLocaleString()}) is ${(paymentToIncomeRatio * 100).toFixed(1)}% of income`,
      severity: 'warning'
    });
    recommendations.push('Consider extending the loan tenure to reduce monthly payment');
  }
  
  const hasErrors = issues.some(issue => issue.severity === 'error');
  
  return {
    isEligible: !hasErrors,
    issues,
    recommendations,
    maxLoanAmount,
    estimatedInterestRate: estimatedRate,
    estimatedMonthlyPayment: estimatedPayment
  };
}

// ============================================================================
// MONTHLY PAYMENT CALCULATION
// ============================================================================

export interface PaymentCalculationInput {
  principal: number;
  annualInterestRate: number;
  tenureMonths: number;
}

export interface PaymentCalculationResult {
  monthlyPayment: number;
  totalPayment: number;
  totalInterest: number;
  effectiveInterestRate: number;
}

export function calculatePayment(input: PaymentCalculationInput): PaymentCalculationResult {
  const monthlyPayment = calcPayment(
    input.principal,
    input.annualInterestRate,
    input.tenureMonths
  );
  
  const totalPayment = monthlyPayment * input.tenureMonths;
  const totalInterest = totalPayment - input.principal;
  const effectiveInterestRate = (totalInterest / input.principal) * 100;
  
  return {
    monthlyPayment,
    totalPayment,
    totalInterest,
    effectiveInterestRate
  };
}

// ============================================================================
// AMORTIZATION SCHEDULE
// ============================================================================

export interface AmortizationEntry {
  period: number;
  payment: number;
  principal: number;
  interest: number;
  balance: number;
}

export function generateAmortizationSchedule(
  principal: number,
  annualInterestRate: number,
  tenureMonths: number
): AmortizationEntry[] {
  const monthlyRate = annualInterestRate / 100 / 12;
  const monthlyPayment = calcPayment(principal, annualInterestRate, tenureMonths);
  
  const schedule: AmortizationEntry[] = [];
  let balance = principal;
  
  for (let period = 1; period <= tenureMonths; period++) {
    const interest = balance * monthlyRate;
    const principalPayment = monthlyPayment - interest;
    balance -= principalPayment;
    
    // Handle rounding for final payment
    if (period === tenureMonths) {
      balance = 0;
    }
    
    schedule.push({
      period,
      payment: monthlyPayment,
      principal: principalPayment,
      interest,
      balance: Math.max(0, balance)
    });
  }
  
  return schedule;
}

// ============================================================================
// LOAN COMPARISON
// ============================================================================

export interface LoanComparisonInput {
  amount: number;
  options: Array<{
    tenure: number;
    interestRate: number;
    label: string;
  }>;
}

export interface LoanComparisonResult {
  comparisons: Array<{
    label: string;
    tenure: number;
    interestRate: number;
    monthlyPayment: number;
    totalPayment: number;
    totalInterest: number;
  }>;
  recommended: number; // Index of recommended option
}

export function compareLoanOptions(input: LoanComparisonInput): LoanComparisonResult {
  const comparisons = input.options.map(option => {
    const payment = calculatePayment({
      principal: input.amount,
      annualInterestRate: option.interestRate,
      tenureMonths: option.tenure
    });
    
    return {
      label: option.label,
      tenure: option.tenure,
      interestRate: option.interestRate,
      monthlyPayment: payment.monthlyPayment,
      totalPayment: payment.totalPayment,
      totalInterest: payment.totalInterest
    };
  });
  
  // Recommend option with lowest total interest
  const recommended = comparisons.reduce((minIdx, curr, idx, arr) => 
    curr.totalInterest < arr[minIdx].totalInterest ? idx : minIdx
  , 0);
  
  return {
    comparisons,
    recommended
  };
}

// ============================================================================
// HELPER FUNCTIONS
// ============================================================================

function calculateMaxLoanAmount(monthlyIncome: number, loanType: LoanType): number {
  // Simplified - real implementation in business-rules.ts
  const multipliers: Record<LoanType, number> = {
    PERSONAL: 5,
    HOME: 50,
    AUTO: 10,
    SME: 20,
    CORPORATE: 100
  };
  
  return monthlyIncome * (multipliers[loanType] || 5);
}

export function formatCurrency(amount: number): string {
  return `฿${amount.toLocaleString('en-US', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
  })}`;
}

export function formatPercentage(value: number): string {
  return `${(value * 100).toFixed(2)}%`;
}

export function roundToTwoDecimals(value: number): number {
  return Math.round(value * 100) / 100;
}
