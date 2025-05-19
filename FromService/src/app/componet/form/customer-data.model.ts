export interface Address {
  addressId: number;
  addrLine1: string;
  addrLine2: string;
  subDistrict: string;
  district: string;
  postalCode: string;
  createdDate: string;
  country: { countryId: number; countryName: string };
  province: { provinceId: number; provinceName: string };
}

export interface General {
  generalId: number;
  generalName: string;
  generalName1: string;
  generalTel: string;
  generalFax: string;
  generalEmail: string;
  generalLine: string;
  generalTax: string;
  generalBranch: string;
  addressId: number;
  address: Address;
}

export interface Shipping {
  shippingId: number;
  deliveryName: string;
  address1: string;
  address2: string;
  subDistrict: string;
  district: string;
  postalCode: string;
  contact_name: string;
  mobile: string;
  freight: string;
  province: { provinceId: number; provinceName: string };
  country: { countryId: number; countryName: string };
}

export interface ShopType {
  id: number;
  shopCode: string;
  shopName: string;
  shopDes: string;
  accGroupName: string;
}

export interface IndustryType {
  id: number;
  induTypeCode?: string;
  induTypeName?: string;
  induTypeDes?: string;
}

export interface SaleOrg {
  id: number;
  saleOrgCode?: string;
  saleOrgName?: string;
  saleOrgDes?: string;
}

export interface Company {
  company_id: number;
  companyCode?: string;
  companyName?: string;
  companyAddr?: string;
}

export interface AccountGroup {
  id: number;
  accGroupCode?: string;
  accGroupName?: string;
  accGroupDes?: string;
}

export interface AccountCode {
  accountId: number;
  accountCode?: string;
  accountName?: string;
  accountType?: string;
  description?: string;
}

export interface SortKey {
  id: number;
  sortkeyCode?: string;
  sortkeyName?: string;
  sortkeyDes?: string;
}

export interface CashGroup {
  id: number;
  cashGroupCode?: string;
  cashGroupName?: string;
  description?: string;
}

export interface PaymentMethod {
  id: number;
  paymentMethodCode?: string;
  paymentMethodName?: string;
  description?: string;
}

export interface TermOfPayment {
  id: number;
  termCode?: string;
  termName?: string;
  description?: string;
}

export interface SaleDistrict {
  id: number;
  districtCode?: string;
  districtName?: string;
  description?: string;
}

export interface BusinessType {
  busiTypeID: number;
  busiTypeCode: string;
  busiTypeName: string;
  busiTypeDes: string;
  registrationDate?: string;       // ใช้ string สำหรับ DateTime ISO format (optional)
  registeredCapital?: number;      // ใช้ number สำหรับ decimal? (optional)
}

export interface CreditInfo {
  creditInfoId: number;
  estimatedPurchase: number;
  timeRequired: number;
  creditLimit: number;
}

export interface DocCreditModel {
  docCreditId: number;
  companyCertificate: boolean;
  copyOfPP_20: boolean;
  copyOfCoRegis: boolean;
  copyOfIDCard: boolean;
  companyLocationMap: boolean;
  otherSpecify?: string;
}

export interface CustomerSign {
  custSignId: number;
  custSignFirstName: string;
  customerId: number;
  custsignTel?: string;
  custsignEmail?: string;
  custsignLine?: string;
}

export interface AccountGroup {
  id: number;
  accGroupCode?: string;
  accGroupName?: string;
  accGroupDes?: string;
}

export interface CashGroup {
  id: number;
  cashGroupCode?: string;
  cashGroupName?: string;
  description?: string;
}

export interface PaymentMethod {
  id: number;
  paymentMethodCode?: string;
  paymentMethodName?: string;
  description?: string;
}
export interface TermOfPayment {
  id: number;
  termCode?: string;       // เช่น "NET30", "COD"
  termName?: string;       // เช่น "Net 30 Days"
  description?: string;    // อธิบายเพิ่มเติม
}

export interface SaleDistrict {
  id: number;
  districtCode?: string;   // เช่น "D001"
  districtName?: string;   // เช่น "ภาคกลาง"
  description?: string;
}

export interface SaleGroup {
  id: number;
  groupCode?: string;      // เช่น "SG01"
  groupName?: string;      // เช่น "กลุ่มขายสินค้าอุปโภค"
  description?: string;
}

export interface CustGroupType {
  id: number;
  groupCode?: string;      // เช่น "CUST001"
  groupName?: string;      // เช่น "ลูกค้าทั่วไป"
  description?: string;
}

export interface Currency {
  id: number;
  currencyCode?: string;     // เช่น "THB", "USD"
  currencyName?: string;     // เช่น "บาท", "ดอลลาร์สหรัฐ"
  symbol?: string;           // เช่น "฿", "$"
}

export interface ExchRateType {
  id: number;
  rateTypeCode?: string;     // เช่น "M", "B", "C"
  rateTypeName?: string;     // เช่น "Market Rate"
  description?: string;
}

export interface CustPricProc {
  id: number;
  pricProcCode?: string;     // เช่น "PRC01"
  pricProcName?: string;     // เช่น "Retail Pricing"
  description?: string;
}

export interface PriceList {
  id: number;
  priceListCode?: string;
  priceListName?: string;
  priceListDes?: string;
}

export interface Incoterm {
  id: number;
  incotermCode?: string;
  incotermName?: string;
  incotermDes?: string;
}

export interface SaleManager {
  id: number;                   // รหัสผู้จัดการฝ่ายขาย
  saleGroupCode?: string;      // รหัสกลุ่มผู้จัดการขาย
  saleGroupName?: string;      // ชื่อกลุ่มผู้จัดการขาย
  saleGroupDes?: string;       // คำอธิบายกลุ่มผู้จัดการขาย
}

export interface CustGroupCountry {
  id: number;
  countryCode?: string;        // รหัสประเทศ
  countryName?: string;        // ชื่อประเทศ
  countryDes?: string;         // คำอธิบายประเทศ
}


// เพิ่ม interfaces อื่นๆ ที่จำเป็น...
export interface CustomerData {
  customerId: number;
  general: General;
  shipping: Shipping;
  shopType: ShopType;
  industryType: IndustryType;
  saleOrg: SaleOrg;
  company: Company;
  accountGroup: AccountGroup;
  accountCode: AccountCode;
  sortKey: SortKey;
  cashGroup: CashGroup;
  paymentMethod: PaymentMethod;
  termOfPayment: TermOfPayment;
  saleDistrict: SaleDistrict;
  saleGroup: SaleGroup;
  custGroupType: CustGroupType;
  businessType: BusinessType;
  creditInfo: CreditInfo;
  customerSign: CustomerSign;
  docCredit: DocCreditModel;
  currency: Currency;
  exchRateType: ExchRateType;
  custPricProc: CustPricProc;
  priceList: PriceList;

  incoterm: Incoterm;                    // ✅ ใหม่
  saleManager: SaleManager;              // ✅ ใหม่
  custGroupCountry: CustGroupCountry;    // ✅ ใหม่

  // ...เพิ่มเติมตามข้อมูล JSON
}


