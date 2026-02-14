// Business rules and credit policies

export const BUSINESS_RULES = {
  // DTI Rules
  DTI: {
    MAX_RATIO: 0.50, // 50% maximum
    MIN_MONTHLY_INCOME: 15000, // ฿15,000 minimum
  },

  // Age Requirements
  AGE: {
    MIN: 20,
    MAX: 65,
  },

  // Employment Requirements
  EMPLOYMENT: {
    MIN_MONTHS: 6, // Minimum 6 months employment
  },

  // Loan Limits by Type
  LOAN_LIMITS: {
    PERSONAL: {
      MIN: 10000,
      MAX: 1000000,
    },
    HOME: {
      MIN: 500000,
      MAX: 20000000,
    },
    AUTO: {
      MIN: 100000,
      MAX: 5000000,
    },
    SME: {
      MIN: 100000,
      MAX: 10000000,
    },
    CORPORATE: {
      MIN: 1000000,
      MAX: 100000000,
    },
  },

  // Tenor Limits
  TENOR: {
    MIN_MONTHS: 6,
    MAX_MONTHS: 360,
  },
};

// Interest Rate Table by Risk Grade
export const INTEREST_RATE_TABLE = {
  EXCELLENT: { min: 0.028, max: 0.035 }, // 2.8% - 3.5%
  GOOD: { min: 0.035, max: 0.045 },      // 3.5% - 4.5%
  ACCEPTABLE: { min: 0.045, max: 0.055 }, // 4.5% - 5.5%
  FAIR: { min: 0.055, max: 0.070 },       // 5.5% - 7.0%
  POOR: { min: 0.070, max: 0.100 },       // 7.0% - 10.0%
};

// Risk Grade by DTI
export const RISK_GRADE_BY_DTI = {
  EXCELLENT: { min: 0, max: 0.25 },       // DTI <= 25%
  GOOD: { min: 0.25, max: 0.40 },         // 25% < DTI <= 40%
  ACCEPTABLE: { min: 0.40, max: 0.50 },   // 40% < DTI <= 50%
  FAIR: { min: 0.50, max: 0.60 },         // 50% < DTI <= 60%
  POOR: { min: 0.60, max: 1.0 },          // DTI > 60%
};

// Loan Type Configuration
export interface LoanTypeConfig {
  name: string;
  minAmount: number;
  maxAmount: number;
  maxTenor: number; // months
  minTenor: number; // months
  requireGuarantor: boolean;
  requireCompanyInfo: boolean;
  maxDti: number; // Maximum DTI ratio
}

export const LOAN_TYPE_CONFIGS: Record<string, LoanTypeConfig> = {
  PERSONAL: {
    name: 'Personal Loan',
    minAmount: 10000,
    maxAmount: 1000000,
    minTenor: 6,
    maxTenor: 72,
    requireGuarantor: false,
    requireCompanyInfo: false,
    maxDti: 0.50,
  },
  HOME: {
    name: 'Home Loan',
    minAmount: 500000,
    maxAmount: 20000000,
    minTenor: 60,
    maxTenor: 360,
    requireGuarantor: true,
    requireCompanyInfo: false,
    maxDti: 0.50,
  },
  AUTO: {
    name: 'Auto Loan',
    minAmount: 100000,
    maxAmount: 5000000,
    minTenor: 36,
    maxTenor: 84,
    requireGuarantor: false,
    requireCompanyInfo: false,
    maxDti: 0.50,
  },
  SME: {
    name: 'SME Loan',
    minAmount: 100000,
    maxAmount: 10000000,
    minTenor: 12,
    maxTenor: 180,
    requireGuarantor: true,
    requireCompanyInfo: true,
    maxDti: 0.60,
  },
  CORPORATE: {
    name: 'Corporate Loan',
    minAmount: 1000000,
    maxAmount: 100000000,
    minTenor: 24,
    maxTenor: 240,
    requireGuarantor: true,
    requireCompanyInfo: true,
    maxDti: 0.70,
  },
};

// Eligibility Rules
export const ELIGIBILITY_RULES = {
  // Basic Requirements
  BASIC: [
    { rule: 'age', description: 'Age must be between 20-65 years' },
    { rule: 'monthlyIncome', description: 'Minimum monthly income ฿15,000' },
    { rule: 'employmentTenure', description: 'At least 6 months employment' },
    { rule: 'dtiRatio', description: 'DTI ratio cannot exceed 50%' },
  ],

  // Additional for Home Loans
  HOME: [
    { rule: 'minIncome', description: 'Minimum monthly income ฿30,000' },
    { rule: 'guarantor', description: 'Requires guarantor' },
  ],

  // Additional for SME
  SME: [
    { rule: 'businessYears', description: 'At least 1 year in business' },
    { rule: 'profitability', description: 'Positive annual profit' },
    { rule: 'guarantor', description: 'Requires guarantor' },
  ],
};

// SLA Definitions (Service Level Agreement - in days)
export const SLA_DEFINITIONS = {
  DOCUMENT_CHECK: 3,        // 3 days to verify documents
  CREDIT_ANALYSIS: 7,       // 7 days for credit analysis
  APPROVAL: 5,              // 5 days for approval
  TOTAL_PROCESSING: 20,     // 20 days total
};

// Loan Purposes
export const LOAN_PURPOSES = [
  { code: 'HOME_PURCHASE', label: 'Home Purchase', thai: 'ซื้อบ้าน' },
  { code: 'HOME_RENO', label: 'Home Renovation', thai: 'ปรับปรุงบ้าน' },
  { code: 'CAR_PURCHASE', label: 'Car Purchase', thai: 'ซื้อรถยนต์' },
  { code: 'EDUCATION', label: 'Education', thai: 'ศึกษาต่อ' },
  { code: 'BUSINESS_CAPITAL', label: 'Business Capital', thai: 'ทุนเดินทำเนียบ' },
  { code: 'DEBT_CONSOLIDATION', label: 'Debt Consolidation', thai: 'รวมหนี้' },
  { code: 'WEDDING', label: 'Wedding Expenses', thai: 'งานแต่งงาน' },
  { code: 'MEDICAL', label: 'Medical Expenses', thai: 'ค่าเบี้ยเลี้ยง' },
  { code: 'INVESTMENT', label: 'Investment', thai: 'ลงทุน' },
  { code: 'TRAVEL', label: 'Travel/Vacation', thai: 'ท่องเที่ยว' },
];

// Industries
export const INDUSTRIES = [
  { code: 'IT', label: 'Information Technology', thai: 'เทคโนโลยีสารสนเทศ' },
  { code: 'FINANCE', label: 'Finance & Banking', thai: 'การเงินและสถาบันการเงิน' },
  { code: 'HEALTHCARE', label: 'Healthcare', thai: 'สุขภาพและการแพทย์' },
  { code: 'MANUFACTURING', label: 'Manufacturing', thai: 'การผลิต' },
  { code: 'RETAIL', label: 'Retail & Commerce', thai: 'ค้นหา' },
  { code: 'REAL_ESTATE', label: 'Real Estate', thai: 'อสังหาริมทรัพย์' },
  { code: 'TOURISM', label: 'Tourism & Hospitality', thai: 'การท่องเที่ยวและโรงแรม' },
  { code: 'EDUCATION', label: 'Education', thai: 'การศึกษา' },
  { code: 'CONSTRUCTION', label: 'Construction', thai: 'ก่อสร้าง' },
  { code: 'AGRICULTURE', label: 'Agriculture', thai: 'เกษตรกรรม' },
];

// Occupations
export const OCCUPATIONS = [
  { code: 'ENGINEER', label: 'Engineer', thai: 'วิศวกร' },
  { code: 'DOCTOR', label: 'Doctor', thai: 'แพทย์' },
  { code: 'LAWYER', label: 'Lawyer', thai: 'ทนายความ' },
  { code: 'ACCOUNTANT', label: 'Accountant', thai: 'บัญชี' },
  { code: 'TEACHER', label: 'Teacher', thai: 'ครู' },
  { code: 'MANAGER', label: 'Manager', thai: 'ผู้จัดการ' },
  { code: 'BUSINESSMAN', label: 'Businessman/woman', thai: 'ผู้ประกอบการ' },
  { code: 'SALES', label: 'Sales Executive', thai: 'พนักงานขาย' },
  { code: 'OFFICER', label: 'Government Officer', thai: 'ข้าราชการ' },
  { code: 'STUDENT', label: 'Student', thai: 'นักเรียน/นักศึกษา' },
];

// Document Requirements by Loan Type
export const DOCUMENT_REQUIREMENTS: Record<string, string[]> = {
  PERSONAL: [
    'ID_CARD',
    'PROOF_OF_ADDRESS',
    'INCOME_STATEMENT',
    'BANK_STATEMENT',
  ],
  HOME: [
    'ID_CARD',
    'PROOF_OF_ADDRESS',
    'INCOME_STATEMENT',
    'TAX_RETURN',
    'BANK_STATEMENT',
    'EMPLOYMENT_LETTER',
  ],
  AUTO: [
    'ID_CARD',
    'PROOF_OF_ADDRESS',
    'INCOME_STATEMENT',
    'BANK_STATEMENT',
    'EMPLOYMENT_LETTER',
  ],
  SME: [
    'ID_CARD',
    'PROOF_OF_ADDRESS',
    'TAX_RETURN',
    'FINANCIAL_STATEMENT',
    'LICENSE',
  ],
  CORPORATE: [
    'ID_CARD',
    'TAX_RETURN',
    'FINANCIAL_STATEMENT',
    'LICENSE',
    'EMPLOYMENT_LETTER',
  ],
};
