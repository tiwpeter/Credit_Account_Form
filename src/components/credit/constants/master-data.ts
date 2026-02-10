/**
 * Master Data
 * Static reference data for dropdowns and selections
 */

import { 
  Gender, 
  MaritalStatus, 
  EmploymentType, 
  IncomeType,
  LoanType,
  DocumentType,
  RelationType 
} from '../types/entities';

// ============================================================================
// PROVINCES (Sample - Real app would have full list)
// ============================================================================

export const PROVINCES = [
  { code: 'BKK', nameTh: 'กรุงเทพมหานคร', nameEn: 'Bangkok', region: 'Central' },
  { code: 'CNX', nameTh: 'เชียงใหม่', nameEn: 'Chiang Mai', region: 'North' },
  { code: 'PKT', nameTh: 'ภูเก็ต', nameEn: 'Phuket', region: 'South' },
  { code: 'KKN', nameTh: 'ขอนแก่น', nameEn: 'Khon Kaen', region: 'Northeast' },
  { code: 'CHB', nameTh: 'ชลบุรี', nameEn: 'Chonburi', region: 'East' },
  { code: 'NPT', nameTh: 'นนทบุรี', nameEn: 'Nonthaburi', region: 'Central' },
  { code: 'HKT', nameTh: 'หาดใหญ่', nameEn: 'Hat Yai', region: 'South' },
  { code: 'UDN', nameTh: 'อุดรธานี', nameEn: 'Udon Thani', region: 'Northeast' }
] as const;

export const DISTRICTS: Record<string, Array<{ code: string; nameTh: string; nameEn: string }>> = {
  BKK: [
    { code: 'PNM', nameTh: 'ปทุมวัน', nameEn: 'Pathum Wan' },
    { code: 'BKN', nameTh: 'บางกอกน้อย', nameEn: 'Bangkok Noi' },
    { code: 'DND', nameTh: 'ดินแดง', nameEn: 'Din Daeng' },
    { code: 'WAN', nameTh: 'วัฒนา', nameEn: 'Watthana' }
  ],
  CNX: [
    { code: 'MUA', nameTh: 'เมืองเชียงใหม่', nameEn: 'Mueang Chiang Mai' },
    { code: 'DOI', nameTh: 'ดอยสะเก็ด', nameEn: 'Doi Saket' }
  ]
};

// ============================================================================
// TITLES
// ============================================================================

export const TITLES = [
  { value: 'นาย', label: 'นาย (Mr.)', gender: [Gender.MALE] },
  { value: 'นาง', label: 'นาง (Mrs.)', gender: [Gender.FEMALE] },
  { value: 'นางสาว', label: 'นางสาว (Miss)', gender: [Gender.FEMALE] },
  { value: 'ดร.', label: 'ดร. (Dr.)', gender: [Gender.MALE, Gender.FEMALE] },
  { value: 'ศ.', label: 'ศ. (Prof.)', gender: [Gender.MALE, Gender.FEMALE] }
] as const;

// ============================================================================
// EDUCATION LEVELS
// ============================================================================

export const EDUCATION_LEVELS = [
  { value: 'PRIMARY', label: 'ประถมศึกษา (Primary School)' },
  { value: 'SECONDARY', label: 'มัธยมศึกษา (Secondary School)' },
  { value: 'VOCATIONAL', label: 'อาชีวศึกษา (Vocational)' },
  { value: 'DIPLOMA', label: 'ประกาศนียบัตร (Diploma)' },
  { value: 'BACHELOR', label: 'ปริญญาตรี (Bachelor\'s Degree)' },
  { value: 'MASTER', label: 'ปริญญาโท (Master\'s Degree)' },
  { value: 'DOCTORATE', label: 'ปริญญาเอก (Doctorate)' }
] as const;

// ============================================================================
// MARITAL STATUS
// ============================================================================

export const MARITAL_STATUS_OPTIONS = [
  { value: MaritalStatus.SINGLE, label: 'โสด (Single)' },
  { value: MaritalStatus.MARRIED, label: 'สมรส (Married)' },
  { value: MaritalStatus.DIVORCED, label: 'หย่าร้าง (Divorced)' },
  { value: MaritalStatus.WIDOWED, label: 'ม่าย (Widowed)' }
] as const;

// ============================================================================
// EMPLOYMENT TYPES
// ============================================================================

export const EMPLOYMENT_TYPE_OPTIONS = [
  { value: EmploymentType.PERMANENT, label: 'พนักงานประจำ (Permanent Employee)' },
  { value: EmploymentType.CONTRACT, label: 'พนักงานสัญญาจ้าง (Contract Employee)' },
  { value: EmploymentType.SELF_EMPLOYED, label: 'ธุรกิจส่วนตัว (Self-Employed)' },
  { value: EmploymentType.BUSINESS_OWNER, label: 'เจ้าของกิจการ (Business Owner)' },
  { value: EmploymentType.FREELANCE, label: 'ฟรีแลนซ์ (Freelance)' },
  { value: EmploymentType.RETIRED, label: 'เกษียณ (Retired)' },
  { value: EmploymentType.UNEMPLOYED, label: 'ไม่ได้ทำงาน (Unemployed)' }
] as const;

// ============================================================================
// INCOME TYPES
// ============================================================================

export const INCOME_TYPE_OPTIONS = [
  { value: IncomeType.SALARY, label: 'เงินเดือน (Salary)' },
  { value: IncomeType.BUSINESS, label: 'รายได้จากธุรกิจ (Business Income)' },
  { value: IncomeType.COMMISSION, label: 'ค่าคอมมิชชั่น (Commission)' },
  { value: IncomeType.RENTAL, label: 'ค่าเช่า (Rental Income)' },
  { value: IncomeType.INVESTMENT, label: 'รายได้จากการลงทุน (Investment Income)' },
  { value: IncomeType.PENSION, label: 'เงินบำนาญ (Pension)' },
  { value: IncomeType.OTHER, label: 'อื่นๆ (Other)' }
] as const;

// ============================================================================
// LOAN TYPES
// ============================================================================

export const LOAN_TYPE_OPTIONS = [
  { value: LoanType.PERSONAL, label: 'สินเชื่อส่วนบุคคล (Personal Loan)' },
  { value: LoanType.HOME, label: 'สินเชื่อบ้าน (Home Loan)' },
  { value: LoanType.AUTO, label: 'สินเชื่อรถยนต์ (Auto Loan)' },
  { value: LoanType.SME, label: 'สินเชื่อ SME (SME Loan)' },
  { value: LoanType.CORPORATE, label: 'สินเชื่อองค์กร (Corporate Loan)' }
] as const;

// ============================================================================
// LOAN PURPOSES
// ============================================================================

export const LOAN_PURPOSES = [
  { category: 'PERSONAL', value: 'DEBT_CONSOLIDATION', label: 'รวมหนี้ (Debt Consolidation)' },
  { category: 'PERSONAL', value: 'EDUCATION', label: 'การศึกษา (Education)' },
  { category: 'PERSONAL', value: 'MEDICAL', label: 'ค่ารักษาพยาบาล (Medical)' },
  { category: 'PERSONAL', value: 'HOME_RENOVATION', label: 'ปรับปรุงบ้าน (Home Renovation)' },
  { category: 'PERSONAL', value: 'WEDDING', label: 'งานแต่งงาน (Wedding)' },
  { category: 'PERSONAL', value: 'TRAVEL', label: 'ท่องเที่ยว (Travel)' },
  { category: 'PERSONAL', value: 'OTHER_PERSONAL', label: 'อื่นๆ (Other)' },
  
  { category: 'HOME', value: 'HOME_PURCHASE', label: 'ซื้อบ้าน (Home Purchase)' },
  { category: 'HOME', value: 'CONDO_PURCHASE', label: 'ซื้อคอนโด (Condo Purchase)' },
  { category: 'HOME', value: 'LAND_PURCHASE', label: 'ซื้อที่ดิน (Land Purchase)' },
  { category: 'HOME', value: 'REFINANCE', label: 'รีไฟแนนซ์ (Refinance)' },
  
  { category: 'AUTO', value: 'NEW_CAR', label: 'รถยนต์ใหม่ (New Car)' },
  { category: 'AUTO', value: 'USED_CAR', label: 'รถยนต์มือสอง (Used Car)' },
  { category: 'AUTO', value: 'MOTORCYCLE', label: 'รถจักรยานยนต์ (Motorcycle)' },
  
  { category: 'BUSINESS', value: 'WORKING_CAPITAL', label: 'เงินทุนหมุนเวียน (Working Capital)' },
  { category: 'BUSINESS', value: 'EQUIPMENT', label: 'ซื้ออุปกรณ์ (Equipment Purchase)' },
  { category: 'BUSINESS', value: 'EXPANSION', label: 'ขยายธุรกิจ (Business Expansion)' },
  { category: 'BUSINESS', value: 'INVENTORY', label: 'สินค้าคงคลัง (Inventory)' },
  { category: 'BUSINESS', value: 'REAL_ESTATE', label: 'อสังหาริมทรัพย์เพื่อธุรกิจ (Commercial Real Estate)' }
] as const;

// ============================================================================
// DOCUMENT TYPES
// ============================================================================

export const DOCUMENT_TYPE_OPTIONS = [
  { value: DocumentType.ID_CARD, label: 'บัตรประชาชน (ID Card)', isMandatory: true },
  { value: DocumentType.HOUSE_REGISTRATION, label: 'ทะเบียนบ้าน (House Registration)', isMandatory: true },
  { value: DocumentType.INCOME_PROOF, label: 'หลักฐานรายได้ (Income Proof)', isMandatory: true },
  { value: DocumentType.BANK_STATEMENT, label: 'Statement บัญชีธนาคาร (Bank Statement)', isMandatory: true },
  { value: DocumentType.TAX_RETURN, label: 'หนังสือรับรองการหักภาษี (Tax Return)', isMandatory: false },
  { value: DocumentType.COMPANY_REGISTRATION, label: 'ทะเบียนบริษัท (Company Registration)', isMandatory: false },
  { value: DocumentType.FINANCIAL_STATEMENT, label: 'งบการเงิน (Financial Statement)', isMandatory: false },
  { value: DocumentType.COLLATERAL_DOCUMENT, label: 'เอกสารหลักประกัน (Collateral Document)', isMandatory: false },
  { value: DocumentType.OTHER, label: 'อื่นๆ (Other)', isMandatory: false }
] as const;

// ============================================================================
// RELATIONSHIP TYPES
// ============================================================================

export const RELATIONSHIP_OPTIONS = [
  { value: 'PARENT', label: 'บิดา/มารดา (Parent)' },
  { value: 'SPOUSE', label: 'คู่สมรส (Spouse)' },
  { value: 'SIBLING', label: 'พี่น้อง (Sibling)' },
  { value: 'CHILD', label: 'บุตร (Child)' },
  { value: 'FRIEND', label: 'เพื่อน (Friend)' },
  { value: 'COLLEAGUE', label: 'เพื่อนร่วมงาน (Colleague)' },
  { value: 'RELATIVE', label: 'ญาติ (Relative)' },
  { value: 'OTHER', label: 'อื่นๆ (Other)' }
] as const;

// ============================================================================
// RELATION TYPES
// ============================================================================

export const RELATION_TYPE_OPTIONS = [
  { value: RelationType.GUARANTOR, label: 'ผู้ค้ำประกัน (Guarantor)' },
  { value: RelationType.REFERENCE, label: 'บุคคลอ้างอิง (Reference)' },
  { value: RelationType.CO_BORROWER, label: 'ผู้กู้ร่วม (Co-Borrower)' }
] as const;

// ============================================================================
// OCCUPATION CATEGORIES
// ============================================================================

export const OCCUPATION_CATEGORIES = [
  {
    category: 'GOVERNMENT',
    label: 'ข้าราชการ/รัฐวิสาหกิจ',
    occupations: [
      'ข้าราชการพลเรือน',
      'ข้าราชการทหาร',
      'ข้าราชการตำรวจ',
      'พนักงานรัฐวิสาหกิจ',
      'ครู/อาจารย์'
    ]
  },
  {
    category: 'PRIVATE',
    label: 'พนักงานบริษัทเอกชน',
    occupations: [
      'พนักงานบริษัท',
      'ผู้บริหาร',
      'วิศวกร',
      'โปรแกรมเมอร์',
      'นักบัญชี',
      'พนักงานขาย'
    ]
  },
  {
    category: 'BUSINESS',
    label: 'ธุรกิจส่วนตัว',
    occupations: [
      'เจ้าของกิจการ',
      'ค้าขาย',
      'ผู้รับเหมา',
      'อาชีพอิสระ'
    ]
  },
  {
    category: 'PROFESSIONAL',
    label: 'อาชีพอิสระ',
    occupations: [
      'แพทย์',
      'ทันตแพทย์',
      'ทนายความ',
      'สถาปนิก',
      'นักออกแบบ',
      'ช่างภาพ'
    ]
  }
] as const;

// ============================================================================
// INDUSTRY CODES
// ============================================================================

export const INDUSTRY_CODES = [
  { code: 'RETAIL', nameTh: 'ค้าปลีก', nameEn: 'Retail', category: 'Commerce' },
  { code: 'WHOLESALE', nameTh: 'ค้าส่ง', nameEn: 'Wholesale', category: 'Commerce' },
  { code: 'MANUFACTURING', nameTh: 'การผลิต', nameEn: 'Manufacturing', category: 'Industry' },
  { code: 'CONSTRUCTION', nameTh: 'ก่อสร้าง', nameEn: 'Construction', category: 'Industry' },
  { code: 'RESTAURANT', nameTh: 'ร้านอาหาร', nameEn: 'Restaurant', category: 'Service' },
  { code: 'HOTEL', nameTh: 'โรงแรม', nameEn: 'Hotel', category: 'Service' },
  { code: 'TRANSPORT', nameTh: 'ขนส่ง', nameEn: 'Transportation', category: 'Logistics' },
  { code: 'IT', nameTh: 'เทคโนโลยีสารสนเทศ', nameEn: 'Information Technology', category: 'Technology' },
  { code: 'FINANCE', nameTh: 'การเงิน', nameEn: 'Finance', category: 'Financial Services' },
  { code: 'REAL_ESTATE', nameTh: 'อสังหาริมทรัพย์', nameEn: 'Real Estate', category: 'Property' },
  { code: 'HEALTHCARE', nameTh: 'สุขภาพ', nameEn: 'Healthcare', category: 'Medical' },
  { code: 'EDUCATION', nameTh: 'การศึกษา', nameEn: 'Education', category: 'Education' },
  { code: 'AGRICULTURE', nameTh: 'เกษตรกรรม', nameEn: 'Agriculture', category: 'Primary' }
] as const;

// ============================================================================
// PAYMENT FREQUENCIES
// ============================================================================

export const PAYMENT_FREQUENCY_OPTIONS = [
  { value: 'MONTHLY', label: 'รายเดือน (Monthly)', daysPerPeriod: 30 },
  { value: 'QUARTERLY', label: 'รายไตรมาส (Quarterly)', daysPerPeriod: 90 },
  { value: 'SEMI_ANNUALLY', label: 'ราย 6 เดือน (Semi-Annually)', daysPerPeriod: 180 },
  { value: 'ANNUALLY', label: 'รายปี (Annually)', daysPerPeriod: 365 }
] as const;

// ============================================================================
// NATIONALITIES
// ============================================================================

export const NATIONALITIES = [
  { code: 'TH', label: 'ไทย (Thai)' },
  { code: 'US', label: 'อเมริกัน (American)' },
  { code: 'GB', label: 'อังกฤษ (British)' },
  { code: 'CN', label: 'จีน (Chinese)' },
  { code: 'JP', label: 'ญี่ปุ่น (Japanese)' },
  { code: 'KR', label: 'เกาหลี (Korean)' },
  { code: 'IN', label: 'อินเดีย (Indian)' },
  { code: 'OTHER', label: 'อื่นๆ (Other)' }
] as const;

// ============================================================================
// OWNERSHIP STATUS
// ============================================================================

export const OWNERSHIP_STATUS_OPTIONS = [
  { value: 'OWNED', label: 'เป็นเจ้าของ (Owned)' },
  { value: 'RENTED', label: 'เช่า (Rented)' },
  { value: 'FAMILY_OWNED', label: 'บ้านครอบครัว (Family Owned)' },
  { value: 'COMPANY_PROVIDED', label: 'บริษัทจัดให้ (Company Provided)' }
] as const;

// ============================================================================
// COLLATERAL TYPES
// ============================================================================

export const COLLATERAL_TYPES = [
  { value: 'REAL_ESTATE', label: 'อสังหาริมทรัพย์ (Real Estate)' },
  { value: 'VEHICLE', label: 'ยานพาหนะ (Vehicle)' },
  { value: 'DEPOSIT', label: 'เงินฝาก (Deposit)' },
  { value: 'SECURITIES', label: 'หลักทรัพย์ (Securities)' },
  { value: 'MACHINERY', label: 'เครื่องจักร (Machinery)' },
  { value: 'INVENTORY', label: 'สินค้าคงเหลือ (Inventory)' },
  { value: 'OTHER', label: 'อื่นๆ (Other)' }
] as const;

// ============================================================================
// HELPER FUNCTIONS
// ============================================================================

export function getLoanPurposesByCategory(loanType: LoanType): typeof LOAN_PURPOSES {
  const categoryMap: Record<LoanType, string> = {
    [LoanType.PERSONAL]: 'PERSONAL',
    [LoanType.HOME]: 'HOME',
    [LoanType.AUTO]: 'AUTO',
    [LoanType.SME]: 'BUSINESS',
    [LoanType.CORPORATE]: 'BUSINESS'
  };
  
  const category = categoryMap[loanType];
  return LOAN_PURPOSES.filter(p => p.category === category);
}

export function getDocumentTypesByApplicant(
  applicantType: 'PERSONAL' | 'CORPORATE',
  loanType: LoanType
): typeof DOCUMENT_TYPE_OPTIONS {
  if (applicantType === 'CORPORATE') {
    return DOCUMENT_TYPE_OPTIONS.filter(doc => 
      ![DocumentType.ID_CARD, DocumentType.HOUSE_REGISTRATION].includes(doc.value as DocumentType)
    );
  }
  
  return DOCUMENT_TYPE_OPTIONS;
}

export function getProvinceByCode(code: string) {
  return PROVINCES.find(p => p.code === code);
}

export function getDistrictsByProvince(provinceCode: string) {
  return DISTRICTS[provinceCode] || [];
}
