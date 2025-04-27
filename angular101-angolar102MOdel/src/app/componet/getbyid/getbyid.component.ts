import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IdseviceService } from '../../sevice/idsevice.service';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-getbyid',
  imports: [CommonModule, ReactiveFormsModule,],
  templateUrl: './getbyid.component.html',
  styleUrls: ['./getbyid.component.css']
})
export class GetbyidComponent implements OnInit {
  customerForm!: FormGroup;
  customerId: string | null = null;

  constructor(
    private apiService: IdseviceService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
        private router: Router  // นำเข้า Router

  ) { }

  ngOnInit(): void {
    // ดึงค่า 'id' จาก URL parameter
    this.customerId = this.route.snapshot.paramMap.get('id');
    

    // เรียก API ด้วย customerId ที่ได้จาก URL
    //ฟังก์ชัน getCustomerById() จะรับ customerId ที่เราได้จาก URL แล้วส่งค่าไปยัง API เพื่อดึงข้อมูล
    this.apiService.getCustomerById(this.customerId!).subscribe({
      //เมื่อ API ส่งข้อมูลกลับมา, Angular จะทำการจับข้อมูลในส่วนของ next ซึ่งในที่นี้จะได้ data
      next: (data) => {
        // เช็คว่า data มีข้อมูลหรือไม่:
        if (data) {
          console.log('✅ ข้อมูลลูกค้าที่ได้จาก API:', data); // log ข้อมูลที่ได้รับจาก API

          // ถ้ามีข้อมูล ก็จะเรียก this.initForm(data); เพื่อกำหนดค่าฟอร์มให้ตรงกับข้อมูลที่ได้รับจาก API.
          this.initForm(data);
        }

      }
    });
  }
  // ฟังก์ชันที่ใช้ในการดาวน์โหลดรายงานลูกค้า
  downloadCustomerReport(): void {
    if (this.customerId) {
      this.router.navigate([`/Download/${this.customerId}`]); // นำทางไปยัง DowndloadComponent
    } else {
      alert('ไม่พบ ID ใน URL');
    }
  }
  // ฟังก์ชันเพื่อกลับไปที่หน้า 'getCustomer'
  goBackToGetCustomer(): void {
    this.router.navigate(['/getCustomer']);  // นำทางไปยังหน้า getCustomer
  }

//เตรียมข้อมูลที่ส่ง
initForm(customer: any) {
  this.customerForm = this.fb.group({
    // ฟิลด์สำหรับ GeneralModel
    generals: this.fb.group({
      // or null
      generalName: [customer.general?.generalName || ''],
      generalFax: [customer.general?.generalFax || ''],
      generalTax: [customer.general?.generalTax || ''],
      generalTel: [customer.general?.generalTel || ''],
      generalLine: [customer.general?.generalLine || ''],
      generalEmail: [customer.general?.generalEmail || ''],
      generalName1: [customer.general?.generalName1 || ''],
      generalBranch: [customer.general?.generalBranch || '']
    }),

    // ฟิลด์สำหรับ AddressesModel
    addresses: this.fb.group({
      addrType: [customer.addresses?.addrType || ''],
      addrLine1: [customer.addresses?.addrLine1 || ''],
      addrLine2: [customer.addresses?.addrLine2 || ''],
      subDistrict: [customer.addresses?.subDistrict || ''],
      district: [customer.addresses?.district || ''],
      province: [customer.addresses?.province || ''],
      postalCode: [customer.addresses?.postalCode || ''],
      country: [customer.addresses?.country || ''],
      createdDate: [customer.addresses?.createdDate || ''],
      countryId: [customer.addresses?.CountryId || 0],
      provinceId: [customer.addresses?.ProvinceId || 0]
    }),

     shipping: this.fb.group({
      DeliveryName: [customer.shipping?.deliveryName || ''],
      address1: [customer.shipping?.address1 || ''],
      address2: [customer.shipping?.address2 || ''],
      district: [customer.shipping?.district || ''],
      province: [customer.shipping?.province || ''],
      postalCode: [customer.shipping?.postalCode || ''],
      shippingcountry: [customer.shipping?.shippingcountry || ''],
      mobile: [customer.shipping?.mobile || ''],
      contact_name: [customer.shipping?.contact_name || ''],
      freight: [customer.shipping?.freight || '']
    }),
  });
}


  
  

  // Method to handle form submission and send the updated data to the API
  onSubmit() {
      console.log('Form submitted');

    if (this.customerForm.valid) {
      // นำค่า่ที่เก็บใส่updatedCustomer
      const updatedCustomer = this.customerForm.value;

      // updatedCustomer 
      this.apiService.updateCustomer(this.customerId!, updatedCustomer).subscribe({
        next: (response) => {
          console.log('✅ Customer updated successfully:', response);
          // Handle the response, e.g., show a success message
        },
        error: (error) => {
          console.error('❌ Error updating customer:', error);
          // Handle the error, e.g., show an error message
        }
      });
    } else {
      console.log('❌ Form is invalid');
    }
  }
}