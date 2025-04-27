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
// ✅ ใช้ readonly แบบนี้ได้
  readonly countryMap: { [key: string]: number } = {
    Thailand: 66,
    Japan: 81,
    USA: 1,
    China: 86,
    Australia: 61,
  };

  constructor() {
    // หรือ จะกำหนดค่าใน constructor ก็ได้ (แต่ไม่จำเป็นกรณีนี้)
  }


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
  ProvinceId: 1,
  ProvinceName: 'Province', // ✅ เพิ่ม ProvinceName (ตั้งจากชื่อ Province เลย)
  PostalCode: '12345',
  CountryId: 1,
  CountryName: 'Country', // ✅ เพิ่ม CountryName (ตั้งชื่อ Country เลย)
};




  Shipping: any = {
  DeliveryName: 'John Doe',
  Address1: '456 Shipping St',
  Address2: 'Apt 7D',
  District: 'Shipping District',
  SubDistrict: 'Shipping SubDistrict', // เพิ่มตรงนี้
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
    CustomerObj: any = {
  
  CustomerName: 'ABC Corp',
 
};

  imtemList: any;  // เก็บข้อมูลจาก API

  http = inject(HttpClient);
  router = inject(Router); // Inject Router

 OnSave() {
    console.log("Current values:", this.GeneralsObj, this.AddressObj, this.Shipping, this.ShopType, this.IndustryType);

   
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
      CountryId: this.AddressObj.CountryId,
      ProvinceId: this.AddressObj.ProvinceId

      
    };

    const shippingData = {
  DeliveryName: this.Shipping.DeliveryName,
  Address1: this.Shipping.Address1,
  Address2: this.Shipping.Address2,
  District: this.Shipping.District,
  SubDistrict: this.Shipping.SubDistrict, // เพิ่ม SubDistrict
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

  // Combine them into a single payload object
   const payload = {
  CustomerName: this.CustomerObj.CustomerName,
  General: {
    GeneralName: this.GeneralsObj.GeneralName1,
    Address: {
      CustomerName: this.CustomerObj.CustomerName, // ✅ เพิ่ม CustomerName เข้าไปใน Address
      Country: {
        CountryId: this.AddressObj.CountryId,
        CountryName: this.AddressObj.CountryName // ✅ เพิ่ม CountryName เข้าไปใน Country
      },
      Province: {
        ProvinceId: this.AddressObj.ProvinceId,
        ProvinceName: this.AddressObj.ProvinceName // ✅ เพิ่ม ProvinceName เข้าไปใน Province
      }
    }
  },
  Shipping: shippingData,
  ShopType: shopData,
  IndustryType: industryData,
  Company: companyData
};



    console.log('Sending:', payload);

     this.http.post('http://localhost:5259/api/Regisform', payload).subscribe(
        response => {
            console.log('Customer created successfully', response);
        },
        error => {
            console.error('Error occurred:', error);
            if (error.status === 400) {
                // แสดงข้อมูลข้อผิดพลาดที่มาจาก ModelState
                console.log('Validation errors:', error.error.errors);
            }
        }
    );
  }

  getItem() {
    this.http.get("http://localhost:5083/api/testget").subscribe((result: any) => {
      // ตรวจสอบว่า response มีข้อมูลในรูปแบบที่คาดไว้หรือไม่
      console.log(result); // ตรวจสอบข้อมูลที่ได้รับจาก API
      this.imtemList = result; // ปรับให้ตรงกับข้อมูลที่ได้จาก API
    });
  }

  // คลิกแล้วไปที่หน้ารายละเอียด
  onItemClick(id: number) {
    this.router.navigate([`/item-detail`, id]); // 👈 ส่ง id ไปใน path
  }

 selectedPayMethod: string = '';
  paymentDescription: string = '';
 // เมื่อเลือกวิธีการชำระเงิน จะอัพเดตคำอธิบาย
  ngOnChanges(): void {
    if (this.selectedPayMethod === 'CASH') {
      this.paymentDescription = "ชำระเงินด้วยเงินสด ณ จุดบริการ";
    } else if (this.selectedPayMethod === 'CARD') {
      this.paymentDescription = "ชำระเงินด้วยบัตรเครดิตหรือบัตรเดบิต";
    } else if (this.selectedPayMethod === 'TRANSFER') {
      this.paymentDescription = "ชำระเงินผ่านการโอนเงินไปยังบัญชีธนาคาร";
    } else {
      this.paymentDescription = '';
    }
  }

  onSubmit() {
    console.log('Selected Payment Method: ', this.selectedPayMethod);
    console.log('Payment Description: ', this.paymentDescription);
  }


selectedTerm: string = '';

  onTermChange(event: any): void {
    this.selectedTerm = event.target.value;
    const description = document.getElementById('paymentTermDescription');
    switch (this.selectedTerm) {
      case 'NET30':
        description!.innerHTML = '<p>ลูกค้าต้องชำระเงินภายใน 30 วันหลังจากได้รับใบแจ้งหนี้</p>';
        break;
      case 'NET60':
        description!.innerHTML = '<p>ลูกค้าต้องชำระเงินภายใน 60 วันหลังจากได้รับใบแจ้งหนี้</p>';
        break;
      case 'ทันที':
        description!.innerHTML = '<p>ลูกค้าต้องชำระเงินทันทีเมื่อทำการสั่งซื้อ</p>';
        break;
      default:
        description!.innerHTML = '<p>กรุณาเลือกเงื่อนไขการชำระเงิน</p>';
        break;
    }
  }
 // ตัวอย่างข้อมูลเขตการขาย
  saleDistricts = [
    { id: 1, saledisCode: 'SD001', saledisName: 'เขตภาคเหนือ', saledisDes: 'เขตการขายในภาคเหนือ' },
    { id: 2, saledisCode: 'SD002', saledisName: 'เขตภาคกลาง', saledisDes: 'เขตการขายในภาคกลาง' },
    { id: 3, saledisCode: 'SD003', saledisName: 'เขตภาคใต้', saledisDes: 'เขตการขายในภาคใต้' }
  ];

  // ตัวแปรสำหรับเก็บเขตการขายที่เลือก
  selectedDistrict: any;

  // เมื่อเลือกเขตการขาย จะอัพเดทข้อมูล
  onDistrictChange(event: any) {
    const selectedId = event.target.value;
    this.selectedDistrict = this.saleDistricts.find(district => district.id === parseInt(selectedId));
  }

// {/*<!--Incoterms-->*/ }
incoterms = [
    {
      incotermCode: 'FOB',
      incotermName: 'Free on Board',
      incotermDes: 'ผู้ขายส่งของขึ้นเรือที่ท่าเรือต้นทาง',
    },
    {
      incotermCode: 'CIF',
      incotermName: 'Cost, Insurance and Freight',
      incotermDes: 'ผู้ขายรับผิดชอบค่าขนส่งและประกันภัยถึงท่าเรือปลายทาง',
    },
    {
      incotermCode: 'EXW',
      incotermName: 'Ex Works',
      incotermDes: 'ผู้ซื้อรับผิดชอบตั้งแต่โรงงานของผู้ขาย',
    },
  ];
  
}
  