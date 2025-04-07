import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IdseviceService } from '../../sevice/idsevice.service';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-getbyid',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './getbyid.component.html',
  styleUrls: ['./getbyid.component.css']
})
export class GetbyidComponent implements OnInit {
  customerForm!: FormGroup;
  customerId: string | null = null;

  constructor(
    private apiService: IdseviceService,
    private route: ActivatedRoute,
    private fb: FormBuilder
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

//เตรียมข้อมูลที่ส่ง
  initForm(customer: any) {
    this.customerForm = this.fb.group({
      generalName: [customer.general?.generalName || ''],

    });
  }

 onSubmit() {
  if (this.customerForm.valid && this.customerId) {
    console.log('ข้อมูลที่ส่งไป:', this.customerForm.value); // ตรวจสอบข้อมูลที่ส่งไป

    this.apiService.updateCustomer(this.customerId, {
      generalName: this.customerForm.value.generalName || "",
    }).subscribe({
      next: () => {
        console.log('API เรียกสำเร็จ');
        alert('อัปเดตข้อมูลสำเร็จ');
      },
      error: (err) => {
        console.error('เกิดข้อผิดพลาดในการอัปเดต:', err);
        alert('ไม่สามารถอัปเดตข้อมูลได้');
      }
    });
  } else {
    alert('กรุณากรอกข้อมูลให้ครบถ้วน');
  }
}


}