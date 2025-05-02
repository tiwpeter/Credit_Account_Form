import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule, HttpClientModule],
})
export class FormComponent {
countries: any[] = [];
provinces: any[] = [];

    loadFormDataFromApi(id: string) {
    this.http.get<any>(`http://localhost:5259/api/Regisform/${id}`)
      .subscribe(response => {
            this.customerData = response;
        console.log('📦 API Response:', response);

        // นำข้อมูลจาก API มาป้อนในแต่ละ Object 
        //อัปเดตค่าจาก API
        const general = response.general;
        this.customerData.general = {
          generalId: general.generalId,
          generalName: general.generalName,
          generalName1: general.generalName1,
          generalTel: general.generalTel,
          generalFax: general.generalFax,
          generalEmail: general.generalEmail,
          generalLine: general.generalLine,
          generalTax: general.generalTax,
          generalBranch: general.generalBranch,
          addressId: general.addressId,
          address: {
            addressId: general.address.addressId,
            addrLine1: general.address.addrLine1,
            addrLine2: general.address.addrLine2,
            subDistrict: general.address.subDistrict,
            district: general.address.district,
            postalCode: general.address.postalCode,
            createdDate: general.address.createdDate,
            country: {
              countryId: general.address.country.countryId,
              countryName: general.address.country.countryName
            },
            province: {
              provinceId: general.address.province.provinceId,
              provinceName: general.address.province.provinceName
            }
          }
        };

        const shipping = response.shipping;
        this.customerData.shipping = {
          shippingId: shipping.shippingId,
          deliveryName: shipping.deliveryName,
          address1: shipping.address1,
          address2: shipping.address2,
          subDistrict: shipping.subDistrict,
          district: shipping.district,
          postalCode: shipping.postalCode,
          contact_name: shipping.contact_name,
          mobile: shipping.mobile,
          freight: shipping.freight,
          province: {
            provinceId: shipping.province.provinceId,
            provinceName: shipping.province.provinceName
          },
          country: {
            countryId: shipping.country.countryId,
            countryName: shipping.country.countryName
          }
        };
        const shopType = response.shopType;


this.customerData.custGroupType = {
  id: response.custGroupType.id,
  groupCode: response.custGroupType.groupCode,
  groupName: response.custGroupType.groupName,
  description: response.custGroupType.description
};

        //เตรียมพร้อมข้อมูลส่ง
        this.customerData.shopType = {
          id: shopType.id,
          shopCode: shopType.shopCode,
          shopName: shopType.shopName,
          shopDes: shopType.shopDes,
          accGroupName: shopType.accGroupName
        };
      });
  }
  
  //กำหนด โครงสร้างของข้อมูล (object structure) ที่ฟอร์มจะใช้ก่อนโหลดข้อมูลจาก API
  //ช่วยให้ HTML template (ใน form.component.html) สามารถ [(ngModel)] หรือ formControlName ได้
  
  customerData = {
    customerId: 1,
    general: {
      generalId: 0,
      generalName: '',
      generalName1: '',
      generalTel: '',
      generalFax: '',
      generalEmail: '',
      generalLine: '',
      generalTax: '',
      generalBranch: '',
      addressId: 2,
      address: {
        addressId: 0,
        addrLine1: '',
        addrLine2: '',
        subDistrict: '',
        district: '',
        postalCode: '',
        createdDate: '',
        country: {
          countryId: 1,
          countryName: ''
        },
        province: {
          provinceId: 1,
          provinceName: ''
        }
      }
    },
    shipping: {
      shippingId: 0,
      deliveryName: '',
      address1: '',
      address2: '',
      subDistrict: '',
      district: '',
      postalCode: '',
      contact_name: '',
      mobile: '',
      freight: '',
      province: {
        provinceId: 1,
        provinceName: ''
      },
      country: {
        countryId: 1,
        countryName: ''
      }
    },
    shopType: {
      id: 0,
      shopCode: '',
      shopName: '',
      shopDes: '',
      accGroupName: ''
    },
    industryType: {
      id: 0,
      induTypeCode: '',
      induTypeName: '',
      induTypeDes: ''
    },
    company: {
      company_id: 0,
      companyCode: '',
      companyName: '',
      companyAddr: ''
    },
    saleOrg: {
      id: 0,
      saleOrgCode: '',
      saleOrgName: '',
      saleOrgDes: ''
    },
    accountCode: {
        accountId: 1,
        accountCode: '',
        accountName: '',
        accountType: '',
        description: ''
    },
    accountGroup: {
        id: 1,
        accGroupCode: '',
        accGroupName: '',
        accGroupDes: ''
    },
    businessType: {
        busiTypeID: 0,
        busiTypeCode: '',
        busiTypeName: '',
        busiTypeDes: '',
        registrationDate: '',
        registeredCapital: ''
    },
    creditInfo: {
        creditInfoId: 0,
        estimatedPurchase: '',
        timeRequired: '',
        creditLimit:'' 
    },
    docCreditId: '',
    docCredit: {
        docCreditId: 0,
        companyCertificate: '',
        copyOfPP_20: '',
        copyOfCoRegis: '',
        copyOfIDCard: '',
        companyLocationMap: '',
        otherSpecify: ''
    },
    customerSign: {
        custSignId: 2,
        custSignFirstName: '',
        custsignTel: '',
        custsignEmail: '',
        custsignLine: ''
    },
    sortKey: {
        id: 0,
        sortkeyCode: '',
        sortkeyName: '',
        sortkeyDes: ''
    },
    cashGroup: '',
    paymentMethod: {
        id: 0,
        paymentMethodCode: '',
        paymentMethodName: '',
        description: ''
    },
    termOfPayment: {
        id: 0,
        termCode: '',
        termName: '',
        description: ''
    },
    saleDistrict: {
        id: 0,
        districtCode: '',
        districtName: '',
        description: ''
    },
    saleGroup: {
        id: 0,
        groupCode: '',
        groupName: '',
        description: ''
    },
    custGroupType: {
        id: 0,
        groupCode: '',
        groupName: '',
        description: ''
    },
    currency: {
        id: 0,
        currencyCode: '',
        currencyName: '',
        symbol: ''
    },
    exchRateType: {
        id: 0,
        rateTypeCode: '',
        rateTypeName: '',
        description: ''
    },
    custPricProc: {
        id: 0,
        pricProcCode: '',
        pricProcName: '',
        description: ''
    },
    priceList: {
        id: 0,
        priceListCode: '',
        priceListName: '',
        priceListDes: ''
    },
    incoterm: {
        id: 0,
        incotermCode: '',
        incotermName: '',
        incotermDes: ''
    },
    saleManager: {
        id: 0,
        saleGroupCode: '',
        saleGroupName: '',
        saleGroupDes: ''
    },
    custGroupCountry: {
        id: 0,
        countryCode: '',
        countryName: '',
        countryDes: ''
    }
  };


  constructor(
    private http: HttpClient,
    private route: ActivatedRoute
  ) {}
  id!: string; // สำหรับเก็บ ID ของฟอร์ม


  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id') || '';
    this.loadCountries();

    if (this.id) {
      this.loadFormDataFromApi(this.id);
    }
  }
  loadCountries() {
  this.http.get<any[]>('http://localhost:5259/api/countries')
    .subscribe(data => {
      this.countries = data;
    });
  }

    onSubmit() {
    // ส่งข้อมูลไป API
    const formData = this.customerData;
    this.submitFormData(this.id, formData);
  }


  submitFormData(id: string, data: any) {
    this.http.put(`https://your-api-url.com/form-data/${id}`, data)
      .subscribe(response => {
        console.log('✅ Data updated successfully', response);
      }, error => {
        console.error('❌ Error updating data', error);
      });
  }
}
