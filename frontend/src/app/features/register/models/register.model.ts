// ============================================================
// ⚠️ TODO: ถ้ามี generated RegisterService (แบบเดียวกับ MasterService)
// ให้ใช้ model ที่ generator สร้างให้แทน interface นี้ทั้งหมด
// ตอนนี้อิงตาม field ที่เห็นใน CreateRegisterHandler.cs ที่แปะมาก่อนหน้า
// ============================================================
export interface CreateRegisterCommand {
  // Step 1 — ข้อมูลบริษัท
  generalName1: string;
  generalName2?: string | null;
  generalTel?: string | null;
  generalFax?: string | null;
  generalEmail?: string | null;
  generalLine?: string | null;
  generalTax?: string | null;
  generalBranch?: string | null;

  // Step 2 — ที่อยู่ (cascading: province -> amphureId -> tambonId)
  addrLine1?: string | null;
  addrLine2?: string | null;
  provinceId?: number | null;
  amphureId?: number | null;
  tambonId?: number | null;
  postalCode?: string | null;
  countryId?: number | null;

  // Step 3 — วงเงิน
  creditLimit?: number | null;
  estimatedPurchase?: number | null;
  timeRequired?: string | null;

  // Step 4 — เอกสาร
  companyCertificate: boolean;
  copyOfPp20: boolean;
  copyOfCoRegis: boolean;
  copyOfIdCard: boolean;
  companyLocationMap: boolean;
  otherSpecify?: string | null;

  // Step 5 — ผู้ลงนาม
  custsignFirstname: string;
  custsignLastname: string;
  custsignTel?: string | null;
  custsignEmail?: string | null;
  custsignLine?: string | null;

  // Sales Assignment
  saleOrg?: number | null;
  saleGroup?: number | null;
  saleDistrict?: number | null;
  salePerson?: number | null;
  saleManager?: number | null;

  // Terms
  termOfPay?: number | null;
  paymentMethod?: number | null;
  currency?: number | null;
  incoterms?: number | null;
  priceList?: number | null;

  // Classification
  busitypeId?: number | null;
  industryType?: number | null;
  shopType?: number | null;
}

export interface CreateRegisterResponse {
  registerId: number;
  message: string;
  createdAt: string;
}
