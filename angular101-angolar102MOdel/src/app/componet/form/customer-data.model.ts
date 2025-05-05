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

// เพิ่ม interfaces อื่นๆ ที่จำเป็น...
export interface CustomerData {
  customerId: number;
  general: General;
  shipping: Shipping;
  shopType: ShopType;
  // ...ใส่เพิ่มเติมตามข้อมูล JSON ที่เหลือ
}
