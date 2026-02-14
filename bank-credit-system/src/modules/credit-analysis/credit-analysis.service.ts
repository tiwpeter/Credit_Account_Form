import { Injectable, Logger } from '@nestjs/common';
import { PrismaService } from '../prisma/prisma.service';
import {
  ApplicationType,
  RiskGrade,
  EmploymentType,
} from '../../common/enums';
import {
  InsufficientIncomeException,
  DtiExceededException,
  LoanAmountExceededException,
  TenureExceededException,
} from '../../common/exceptions';

// ============================================================
// BUSINESS RULES CONSTANTS
// ============================================================
const BUSINESS_RULES = {
  MINIMUM_MONTHLY_INCOME_THB: 15_000,
  MAX_DTI_RATIO: 0.50,                    // 50%
  PERSONAL_LOAN_INCOME_MULTIPLIER: 5,     // Max 5× monthly income
  PERSONAL_LOAN_MAX_TENURE_MONTHS: 60,
  SME_LOAN_MAX_TENURE_MONTHS: 120,
  MAX_MONTHLY_PAYMENT_RATIO: 0.40,        // Payment should not exceed 40% of income
} as const;

// ============================================================
// INTERFACES
// ============================================================
export interface DtiCalculationInput {
  monthlyIncome: number;
  existingMonthlyDebtPayment: number;
  proposedMonthlyPayment: number;
}

export interface DtiCalculationResult {
  totalMonthlyIncome: number;
  existingDebt: number;
  proposedPayment: number;
  totalDebt: number;
  dtiRatio: number;
  dtiPassed: boolean;
  maxAllowedPayment: number;
}

export interface EligibilityCheckInput {
  monthlyIncome: number;
  requestedAmount: number;
  requestedTenureMonths: number;
  existingMonthlyDebt: number;
  loanType: ApplicationType;
}

export interface EligibilityResult {
  isEligible: boolean;
  incomeEligible: boolean;
  amountEligible: boolean;
  tenureEligible: boolean;
  maxEligibleAmount: number;
  maxTenureMonths: number;
  failureReasons: string[];
  estimatedMonthlyPayment: number;
}

export interface RiskAssessmentInput {
  monthlyIncome: number;
  requestedAmount: number;
  tenureMonths: number;
  existingDebt: number;
  employmentType: EmploymentType;
  yearsEmployed?: number;
  dtiRatio: number;
  loanType: ApplicationType;
  // For corporate
  annualRevenue?: number;
  annualNetProfit?: number;
  yearsInBusiness?: number;
}

export interface RiskAssessmentResult {
  creditScore: number;   // 300–850 scale
  riskGrade: RiskGrade;
  suggestedInterestRate: number;  // As decimal, e.g. 0.085
  factors: RiskFactor[];
  recommendation: string;
}

interface RiskFactor {
  factor: string;
  weight: number;
  score: number;
  description: string;
}

// ============================================================
// CREDIT ANALYSIS SERVICE
// ============================================================
@Injectable()
export class CreditAnalysisService {
  private readonly logger = new Logger(CreditAnalysisService.name);

  constructor(private readonly prisma: PrismaService) {}

  // ============================================================
  // DTI CALCULATION
  // ============================================================

  /**
   * Calculate Debt-to-Income ratio
   * DTI = (Existing Debt + Proposed Payment) / Monthly Income
   */
  calculateDti(input: DtiCalculationInput): DtiCalculationResult {
    const { monthlyIncome, existingMonthlyDebtPayment, proposedMonthlyPayment } = input;

    if (monthlyIncome <= 0) {
      throw new Error('Monthly income must be greater than zero');
    }

    const totalDebt = existingMonthlyDebtPayment + proposedMonthlyPayment;
    const dtiRatio = totalDebt / monthlyIncome;
    const maxAllowedPayment =
      monthlyIncome * BUSINESS_RULES.MAX_DTI_RATIO - existingMonthlyDebtPayment;

    return {
      totalMonthlyIncome: monthlyIncome,
      existingDebt: existingMonthlyDebtPayment,
      proposedPayment: proposedMonthlyPayment,
      totalDebt,
      dtiRatio: parseFloat(dtiRatio.toFixed(4)),
      dtiPassed: dtiRatio <= BUSINESS_RULES.MAX_DTI_RATIO,
      maxAllowedPayment: Math.max(0, maxAllowedPayment),
    };
  }

  /**
   * Calculate estimated monthly payment using flat-rate method
   * Monthly Payment = (Principal × Monthly Rate × (1 + Monthly Rate)^N)
   *                   / ((1 + Monthly Rate)^N - 1)
   */
  calculateMonthlyPayment(
    principal: number,
    annualInterestRate: number,
    tenureMonths: number,
  ): number {
    if (annualInterestRate === 0) {
      return principal / tenureMonths;
    }

    const monthlyRate = annualInterestRate / 12;
    const payment =
      (principal * monthlyRate * Math.pow(1 + monthlyRate, tenureMonths)) /
      (Math.pow(1 + monthlyRate, tenureMonths) - 1);

    return parseFloat(payment.toFixed(2));
  }

  // ============================================================
  // ELIGIBILITY CHECK
  // ============================================================

  /**
   * Full loan eligibility assessment
   * Used before allowing submission AND at credit analysis stage
   */
  checkEligibility(input: EligibilityCheckInput): EligibilityResult {
    const {
      monthlyIncome,
      requestedAmount,
      requestedTenureMonths,
      existingMonthlyDebt,
      loanType,
    } = input;

    const failureReasons: string[] = [];

    // 1. Income check
    const incomeEligible =
      monthlyIncome >= BUSINESS_RULES.MINIMUM_MONTHLY_INCOME_THB;
    if (!incomeEligible) {
      failureReasons.push(
        `Monthly income ${monthlyIncome} THB is below minimum requirement of ${BUSINESS_RULES.MINIMUM_MONTHLY_INCOME_THB} THB`,
      );
    }

    // 2. Loan amount check
    const maxEligibleAmount =
      loanType === ApplicationType.PERSONAL_LOAN
        ? monthlyIncome * BUSINESS_RULES.PERSONAL_LOAN_INCOME_MULTIPLIER
        : monthlyIncome * 24; // For SME: ~24x monthly revenue

    const amountEligible = requestedAmount <= maxEligibleAmount;
    if (!amountEligible) {
      failureReasons.push(
        `Requested amount ${requestedAmount} THB exceeds maximum eligible amount ${maxEligibleAmount} THB`,
      );
    }

    // 3. Tenure check
    const maxTenureMonths =
      loanType === ApplicationType.PERSONAL_LOAN
        ? BUSINESS_RULES.PERSONAL_LOAN_MAX_TENURE_MONTHS
        : BUSINESS_RULES.SME_LOAN_MAX_TENURE_MONTHS;

    const tenureEligible = requestedTenureMonths <= maxTenureMonths;
    if (!tenureEligible) {
      failureReasons.push(
        `Tenure ${requestedTenureMonths} months exceeds maximum ${maxTenureMonths} months for ${loanType}`,
      );
    }

    // Estimate monthly payment using average rate (for preliminary check)
    const avgRate = 0.10; // 10% as default for estimation
    const estimatedMonthlyPayment = this.calculateMonthlyPayment(
      requestedAmount,
      avgRate,
      requestedTenureMonths,
    );

    return {
      isEligible: incomeEligible && amountEligible && tenureEligible,
      incomeEligible,
      amountEligible,
      tenureEligible,
      maxEligibleAmount,
      maxTenureMonths,
      failureReasons,
      estimatedMonthlyPayment,
    };
  }

  /**
   * Validate eligibility and throw specific exceptions if failed
   */
  assertEligible(input: EligibilityCheckInput): void {
    const { monthlyIncome, requestedAmount, requestedTenureMonths, loanType } = input;

    if (monthlyIncome < BUSINESS_RULES.MINIMUM_MONTHLY_INCOME_THB) {
      throw new InsufficientIncomeException(
        BUSINESS_RULES.MINIMUM_MONTHLY_INCOME_THB,
        monthlyIncome,
      );
    }

    const maxAmount =
      loanType === ApplicationType.PERSONAL_LOAN
        ? monthlyIncome * BUSINESS_RULES.PERSONAL_LOAN_INCOME_MULTIPLIER
        : monthlyIncome * 24;

    if (requestedAmount > maxAmount) {
      throw new LoanAmountExceededException(requestedAmount, maxAmount);
    }

    const maxTenure =
      loanType === ApplicationType.PERSONAL_LOAN
        ? BUSINESS_RULES.PERSONAL_LOAN_MAX_TENURE_MONTHS
        : BUSINESS_RULES.SME_LOAN_MAX_TENURE_MONTHS;

    if (requestedTenureMonths > maxTenure) {
      throw new TenureExceededException(requestedTenureMonths, maxTenure, loanType);
    }
  }

  // ============================================================
  // RISK GRADING
  // ============================================================

  /**
   * Comprehensive risk assessment using weighted scoring model
   * Returns credit score (300-850), risk grade, and suggested rate
   */
  assessRisk(input: RiskAssessmentInput): RiskAssessmentResult {
    const factors: RiskFactor[] = [];
    let totalScore = 0;
    let totalWeight = 0;

    // 1. DTI Ratio (Weight: 30%)
    const dtiScore = this.scoreDti(input.dtiRatio);
    factors.push({
      factor: 'DTI_RATIO',
      weight: 30,
      score: dtiScore,
      description: `DTI ratio: ${(input.dtiRatio * 100).toFixed(1)}%`,
    });
    totalScore += dtiScore * 30;
    totalWeight += 30;

    // 2. Loan-to-Income ratio (Weight: 25%)
    const ltiRatio = input.requestedAmount / (input.monthlyIncome * 12);
    const ltiScore = this.scoreLti(ltiRatio);
    factors.push({
      factor: 'LOAN_TO_INCOME',
      weight: 25,
      score: ltiScore,
      description: `Loan-to-annual-income: ${(ltiRatio * 100).toFixed(1)}%`,
    });
    totalScore += ltiScore * 25;
    totalWeight += 25;

    // 3. Employment stability (Weight: 20%)
    const employmentScore = this.scoreEmployment(
      input.employmentType,
      input.yearsEmployed,
    );
    factors.push({
      factor: 'EMPLOYMENT_STABILITY',
      weight: 20,
      score: employmentScore,
      description: `Employment type: ${input.employmentType}, Years: ${input.yearsEmployed || 'N/A'}`,
    });
    totalScore += employmentScore * 20;
    totalWeight += 20;

    // 4. Tenure risk (Weight: 15%)
    const maxTenure =
      input.loanType === ApplicationType.PERSONAL_LOAN ? 60 : 120;
    const tenureRatio = input.tenureMonths / maxTenure;
    const tenureScore = this.scoreTenure(tenureRatio);
    factors.push({
      factor: 'TENURE_RISK',
      weight: 15,
      score: tenureScore,
      description: `Tenure: ${input.tenureMonths} months (${(tenureRatio * 100).toFixed(0)}% of max)`,
    });
    totalScore += tenureScore * 15;
    totalWeight += 15;

    // 5. Income level (Weight: 10%)
    const incomeScore = this.scoreIncome(
      input.monthlyIncome,
      input.loanType,
    );
    factors.push({
      factor: 'INCOME_LEVEL',
      weight: 10,
      score: incomeScore,
      description: `Monthly income: ${input.monthlyIncome.toLocaleString()} THB`,
    });
    totalScore += incomeScore * 10;
    totalWeight += 10;

    // Normalize to 0-100 scale
    const normalizedScore = totalScore / totalWeight;
    // Map to 300-850 credit score range
    const creditScore = Math.round(300 + (normalizedScore / 100) * 550);

    const riskGrade = this.scoreToRiskGrade(normalizedScore);
    const suggestedInterestRate = this.riskGradeToRate(riskGrade, input.loanType);

    const recommendation = this.buildRecommendation(
      riskGrade,
      normalizedScore,
      input,
    );

    return {
      creditScore: Math.min(850, Math.max(300, creditScore)),
      riskGrade,
      suggestedInterestRate,
      factors,
      recommendation,
    };
  }

  // ============================================================
  // SAVE ANALYSIS RESULTS
  // ============================================================

  async saveAnalysis(params: {
    applicationId: string;
    analyzedById: string;
    verifiedMonthlyIncome: number;
    dtiResult: DtiCalculationResult;
    eligibilityResult: EligibilityResult;
    riskResult: RiskAssessmentResult;
    analystNote?: string;
  }) {
    const {
      applicationId,
      analyzedById,
      verifiedMonthlyIncome,
      dtiResult,
      eligibilityResult,
      riskResult,
      analystNote,
    } = params;

    return this.prisma.creditAnalysis.upsert({
      where: { applicationId },
      create: {
        applicationId,
        analyzedById,
        verifiedMonthlyIncome,
        totalMonthlyDebt: dtiResult.totalDebt,
        proposedMonthlyPayment: dtiResult.proposedPayment,
        dtiRatio: dtiResult.dtiRatio,
        dtiPassed: dtiResult.dtiPassed,
        incomeEligible: eligibilityResult.incomeEligible,
        amountEligible: eligibilityResult.amountEligible,
        tenureEligible: eligibilityResult.tenureEligible,
        creditScore: riskResult.creditScore,
        riskGrade: riskResult.riskGrade,
        suggestedInterestRate: riskResult.suggestedInterestRate,
        analysisFactors: riskResult.factors as any,
        recommendation: riskResult.recommendation,
        analystNote,
      },
      update: {
        analyzedById,
        verifiedMonthlyIncome,
        totalMonthlyDebt: dtiResult.totalDebt,
        proposedMonthlyPayment: dtiResult.proposedPayment,
        dtiRatio: dtiResult.dtiRatio,
        dtiPassed: dtiResult.dtiPassed,
        incomeEligible: eligibilityResult.incomeEligible,
        amountEligible: eligibilityResult.amountEligible,
        tenureEligible: eligibilityResult.tenureEligible,
        creditScore: riskResult.creditScore,
        riskGrade: riskResult.riskGrade,
        suggestedInterestRate: riskResult.suggestedInterestRate,
        analysisFactors: riskResult.factors as any,
        recommendation: riskResult.recommendation,
        analystNote,
        updatedAt: new Date(),
      },
    });
  }

  // ============================================================
  // SCORING HELPERS (PRIVATE)
  // ============================================================

  private scoreDti(dti: number): number {
    if (dti <= 0.20) return 100;
    if (dti <= 0.30) return 85;
    if (dti <= 0.40) return 65;
    if (dti <= 0.45) return 45;
    if (dti <= 0.50) return 20;
    return 0; // Exceeds limit
  }

  private scoreLti(lti: number): number {
    if (lti <= 0.5) return 100;
    if (lti <= 1.0) return 80;
    if (lti <= 2.0) return 60;
    if (lti <= 3.0) return 40;
    if (lti <= 4.0) return 20;
    return 10;
  }

  private scoreEmployment(
    type: EmploymentType,
    yearsEmployed?: number,
  ): number {
    const baseScores: Record<EmploymentType, number> = {
      [EmploymentType.EMPLOYED]: 80,
      [EmploymentType.SELF_EMPLOYED]: 65,
      [EmploymentType.BUSINESS_OWNER]: 70,
      [EmploymentType.RETIRED]: 75,
      [EmploymentType.UNEMPLOYED]: 10,
    };

    let score = baseScores[type] || 50;

    // Bonus for employment stability
    if (yearsEmployed !== undefined) {
      if (yearsEmployed >= 5) score = Math.min(100, score + 15);
      else if (yearsEmployed >= 2) score = Math.min(100, score + 5);
      else if (yearsEmployed < 1) score = Math.max(0, score - 20);
    }

    return score;
  }

  private scoreTenure(tenureRatio: number): number {
    if (tenureRatio <= 0.25) return 100;
    if (tenureRatio <= 0.50) return 75;
    if (tenureRatio <= 0.75) return 50;
    return 25;
  }

  private scoreIncome(income: number, loanType: ApplicationType): number {
    if (loanType === ApplicationType.PERSONAL_LOAN) {
      if (income >= 100_000) return 100;
      if (income >= 50_000) return 80;
      if (income >= 30_000) return 60;
      if (income >= 20_000) return 40;
      return 20;
    } else {
      // Corporate: monthly revenue
      if (income >= 500_000) return 100;
      if (income >= 200_000) return 80;
      if (income >= 100_000) return 60;
      if (income >= 50_000) return 40;
      return 20;
    }
  }

  private scoreToRiskGrade(score: number): RiskGrade {
    if (score >= 85) return RiskGrade.A_PLUS;
    if (score >= 72) return RiskGrade.A;
    if (score >= 60) return RiskGrade.B_PLUS;
    if (score >= 48) return RiskGrade.B;
    if (score >= 35) return RiskGrade.C;
    return RiskGrade.D;
  }

  private riskGradeToRate(
    grade: RiskGrade,
    loanType: ApplicationType,
  ): number {
    // Interest rates by grade (annual, as decimal)
    const rates: Record<RiskGrade, { personal: number; corporate: number }> = {
      [RiskGrade.A_PLUS]: { personal: 0.045, corporate: 0.035 },
      [RiskGrade.A]: { personal: 0.065, corporate: 0.055 },
      [RiskGrade.B_PLUS]: { personal: 0.085, corporate: 0.075 },
      [RiskGrade.B]: { personal: 0.110, corporate: 0.095 },
      [RiskGrade.C]: { personal: 0.150, corporate: 0.130 },
      [RiskGrade.D]: { personal: 0.999, corporate: 0.999 }, // Reject signal
    };

    return loanType === ApplicationType.PERSONAL_LOAN
      ? rates[grade].personal
      : rates[grade].corporate;
  }

  private buildRecommendation(
    grade: RiskGrade,
    score: number,
    input: RiskAssessmentInput,
  ): string {
    if (grade === RiskGrade.D) {
      return `REJECT: Risk score ${score.toFixed(1)}/100. DTI or credit profile does not meet minimum requirements.`;
    }

    if (grade === RiskGrade.A_PLUS || grade === RiskGrade.A) {
      return `APPROVE: Excellent risk profile (score: ${score.toFixed(1)}/100). Standard terms apply.`;
    }

    if (grade === RiskGrade.B_PLUS || grade === RiskGrade.B) {
      return `APPROVE WITH CONDITIONS: Acceptable risk profile (score: ${score.toFixed(1)}/100). Consider requiring collateral or guarantor.`;
    }

    return `CONDITIONAL REVIEW: Marginal risk profile (score: ${score.toFixed(1)}/100). Requires senior approval.`;
  }
}
