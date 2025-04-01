import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms'; // 👈 ต้อง import ตรงนี้
import { Router } from '@angular/router'; // เพิ่ม Router เพื่อใช้ในการนำทาง
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http'; // 👈 เพิ่ม HttpClientModule

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrl: './form.component.css',
  imports: [FormsModule, CommonModule, HttpClientModule],
})
export class FormComponent {

  GeneralsObj: any = {
    GeneralName1: 'Default Name',
    GeneralTel: '0891234567',
    GeneralFax: '026789123',
    GeneralEmail: 'example@email.com',
    GeneralLine: 'line_id',
    GeneralTax: '123456789',
    GeneralBranch: 'Main Branch'
  };

  AddressObj: any = {
    AddrType: 'Home',
    AddrLine1: '123 Main St',
    AddrLine2: 'Apt 4B',
    SubDistrict: 'SubDistrict',
    District: 'District',
    Province: 'Province',
    PostalCode: '12345',
    Country: 'Country'
  };

  Shipping: any = {
    DeliveryName: 'John Doe',
    Address1: '456 Shipping St',
    Address2: 'Apt 7D',
    District: 'Shipping District',
    Province: 'Shipping Province',
    PostalCode: '67890',
    Country: 'Shipping Country'
  };

  // ข้อมูลร้านค้า (ShopType)
  ShopType: any = {
    shopCode: '001',
    shopName: 'My Shop',
    shopDes: 'This is a sample shop',
    accGroupName: 'Group A'
  };

 // ข้อมูลประเภทอุตสาหกรรม (IndustryType)
  IndustryType: any = {
    InduTypeCode: 'IT001',
    InduTypeName: 'Information Technology',
    InduTypeDes: 'Technology related to computing and IT services.'
  };

  CompanyObj: any = {
  companyCode: 'C001',
  companyName: 'ABC Corp',
  companyAddr: '123 Tech St'
};

creditInfo: any = {
  customer_id: 'C001',
  estimatedPurchase: 50000,
  timeRequired: '2025-12-31',
  creditLimit: 100000,
  monthlyPayment: 2500,
  paymentStatus: 'Pending'
};

  imtemList: any;  // เก็บข้อมูลจาก API

  http = inject(HttpClient);
  router = inject(Router); // Inject Router

 OnSave() {
    console.log("Current values:", this.GeneralsObj, this.AddressObj, this.Shipping, this.ShopType, this.IndustryType);

    // Separate General, Address, Shipping, ShopType, and IndustryType into distinct objects
    const generalData = {
      GeneralName1: this.GeneralsObj.GeneralName1,
      GeneralTel: this.GeneralsObj.GeneralTel,
      GeneralFax: this.GeneralsObj.GeneralFax,
      GeneralEmail: this.GeneralsObj.GeneralEmail,
      GeneralLine: this.GeneralsObj.GeneralLine,
      GeneralTax: this.GeneralsObj.GeneralTax,
      GeneralBranch: this.GeneralsObj.GeneralBranch
    };

    const addressData = {
      AddrType: this.AddressObj.AddrType,
      AddrLine1: this.AddressObj.AddrLine1,
      AddrLine2: this.AddressObj.AddrLine2,
      SubDistrict: this.AddressObj.SubDistrict,
      District: this.AddressObj.District,
      Province: this.AddressObj.Province,
      PostalCode: this.AddressObj.PostalCode,
      Country: this.AddressObj.Country
    };

    const shippingData = {
      DeliveryName: this.Shipping.DeliveryName,
      Address1: this.Shipping.Address1,
      Address2: this.Shipping.Address2,
      District: this.Shipping.District,
      Province: this.Shipping.Province,
      PostalCode: this.Shipping.PostalCode,
      Country: this.Shipping.Country
    };

    const shopData = {
      shopCode: this.ShopType.shopCode,
      shopName: this.ShopType.shopName,
      shopDes: this.ShopType.shopDes,
      accGroupName: this.ShopType.accGroupName
    };

    const industryData = {
      InduTypeCode: this.IndustryType.InduTypeCode,
      InduTypeName: this.IndustryType.InduTypeName,
      InduTypeDes: this.IndustryType.InduTypeDes
    };

  const companyData = {
    companyCode: this.CompanyObj.companyCode,
    companyName: this.CompanyObj.companyName,
    companyAddr: this.CompanyObj.companyAddr
  };

  const creditInfo = {
    customer_id: this.creditInfo.customer_id,
    estimatedPurchase: this.creditInfo.estimatedPurchase,
    timeRequired: this.creditInfo.timeRequired,
    creditLimit: this.creditInfo.creditLimit,
    monthlyPayment: this.creditInfo.monthlyPayment,
    paymentStatus: this.creditInfo.paymentStatus
  };
  
  


  // Combine them into a single payload object
  const payload = {
    General: generalData,
    Address: addressData,
    Shipping: shippingData,
    ShopType: shopData,
    IndustryType: industryData,
    Company: companyData,  // เพิ่มข้อมูลบริษัทที่นี่
    CreditInfo: creditInfo  // ✅ เพิ่มตรงนี้

  };


    console.log('Sending:', payload);

    this.http.post("http://localhost:5083/api/register", payload).subscribe(
      (res) => {
        console.log('Response:', res);
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }

}
