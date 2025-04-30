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
    customerId: string | null = null;
  customerForm!: FormGroup;

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

          // / สร้างฟอร์มด้วยข้อมูลที่ได้รับจาก API

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

//กำหนดค่า
initForm(customer: any) {
  this.customerForm = this.fb.group({
    accountCode: this.fb.group({
      accountId: [customer.accountCode?.accountId || ''],
      accountCode: [customer.accountCode?.accountCode || ''],
      accountName: [customer.accountCode?.accountName || ''],
      accountType: [customer.accountCode?.accountType || ''],
      description: [customer.accountCode?.description || '']
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