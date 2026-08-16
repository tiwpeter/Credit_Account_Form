// รูปแบบตัวเลือก dropdown ทั่วไปที่ใช้ทั่วทั้งฟอร์ม (หลัง normalize แล้ว)
export interface MasterOption {
  id: number;
  name: string;
  nameEn: string;
}

// ============================================================
// ⚠️ TODO: apiMasterAllGet() เป็น Observable<any> เพราะไฟล์ generated
// ไม่มี type กำกับ — interface ด้านล่างนี้เป็น "สมมติฐาน" ตาม field
// ที่คาดว่า backend น่าจะส่งมา (อิงชื่อ Handler ที่มีอยู่)
//
// วิธีแก้เมื่อรู้ shape จริง:
//   1. console.log(res) จาก apiMasterAllGet() หนึ่งครั้ง ดู key จริง
//   2. แก้ key ใน RawMasterAllResponse ด้านล่างนี้ให้ตรง
//   3. แก้เฉพาะฟังก์ชัน normalizeMasterAll() ใน master.service.ts
//      ให้ map key ให้ตรง — ไม่ต้องแตะไฟล์อื่นเลย
// ============================================================
export interface RawMasterAllResponse {
  // Sales
  saleOrgs?: RawOption[];
  saleGroups?: RawOption[];
  saleDistricts?: RawOption[];
  salePersons?: RawOption[];
  saleManagers?: RawOption[];

  // Type / Classification
  businessTypes?: RawOption[];
  industryTypes?: RawOption[];
  shopTypes?: RawOption[];

  // Finance
  termOfPays?: RawOption[];
  paymentMethods?: RawOption[];
  currencies?: RawOption[];
  incoterms?: RawOption[];
  priceLists?: RawOption[];

  // Address (ระดับบนสุด — จังหวัด/ประเทศ ไม่ผูก parent)
  provinces?: RawOption[];
  countries?: RawOption[];

  // เผื่อ backend ห่อ key อื่น (เช่น ตัวพิมพ์เล็ก/พหูพจน์ต่างกัน)
  [key: string]: unknown;
}

// รูปแบบ option ดิบก่อน normalize — เผื่อ backend ใช้ field name ต่างจาก id/name
// เช่น อาจเป็น { code, description } หรือ { value, label } ก็ปรับ mapper ที่จุดเดียว
export interface RawOption {
  id?: number | string;
  code?: number | string;
  value?: number | string;
  name?: string;
  label?: string;
  description?: string;
  nameTh?: string;   // ➕ เพิ่ม
  nameEn?: string;   // ➕ เพิ่ม (เผื่อใช้)
  [key: string]: unknown;
}

// ============================================================
// รูปแบบข้อมูลหลัง normalize แล้ว ใช้จริงในฟอร์ม
// ============================================================
export interface SalesMaster {
  saleOrgs: MasterOption[];
  saleGroups: MasterOption[];
  saleDistricts: MasterOption[];
  salePersons: MasterOption[];
  saleManagers: MasterOption[];
}

export interface TypeMaster {
  businessTypes: MasterOption[];
  industryTypes: MasterOption[];
  shopTypes: MasterOption[];
}

export interface FinanceMaster {
  termOfPays: MasterOption[];
  paymentMethods: MasterOption[];
  currencies: MasterOption[];
  incoterms: MasterOption[];
  priceLists: MasterOption[];
}

export interface AddressMaster {
  provinces: MasterOption[];
  countries: MasterOption[];
}

// รวมทุก Master ไว้ใน object เดียว (ยกเว้น amphures/tambons ซึ่งเป็น cascading โหลดแยก)
export interface RegisterMasterData {
  sales: SalesMaster;
  type: TypeMaster;
  finance: FinanceMaster;
  address: AddressMaster;
}
