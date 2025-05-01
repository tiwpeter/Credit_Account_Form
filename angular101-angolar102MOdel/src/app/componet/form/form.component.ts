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
  generalForm: FormGroup;
  shippingForm: FormGroup;
  id!: string; // <-- เก็บ ID ของฟอร์มนี้

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private route: ActivatedRoute
  ) {
    // สร้างฟอร์ม
    this.generalForm = this.fb.group({
      generalName: [''],
      generalName1: [''],
      generalTel: [''],
      generalFax: [''],
      generalEmail: [''],
      generalLine: [''],
      generalTax: [''],
      generalBranch: [''],
      address: this.fb.group({
        addrLine1: [''],
        addrLine2: [''],
        subDistrict: [''],
        district: [''],
        province: [''],
        postalCode: [''],
        country: [''],
        createdDate: [''],
        countryId: [null],
        provinceId: [null]
      })
    });

    this.shippingForm = this.fb.group({
      DeliveryName: [''],
      address1: [''],
      district: [''],
      province: [''],
      postalCode: [''],
      shippingcountry: [''],
      mobile: [''],
      contact_name: [''],
      freight: ['']
    });
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id') || '';
    if (this.id) {
      this.loadFormDataFromApi(this.id);
    }
  }

loadFormDataFromApi(id: string) {
  this.http.get<any>(`http://localhost:5259/api/Regisform/${id}`)
    .subscribe(response => {
      console.log('📦 API Response:', response); // 👈 เพิ่มตรงนี้

      // ดึงข้อมูลจาก api มาเก็บใน response
      // เข้าถึงข้อมูล general ด่้วย response.general
      const general = response.general;

      if (general) {
        // แก้ไขให้ patchValue แค่ครั้งเดียว โดยใช้ข้อมูลที่มาจาก general และ address
        this.generalForm.patchValue({
          generalName: general.generalName,
          generalName1: general.generalName1,
          generalTel: general.generalTel,
          generalFax: general.generalFax,
          generalEmail: general.generalEmail,
          generalLine: general.generalLine,
          generalTax: general.generalTax,
          generalBranch: general.generalBranch,
          address: {
            addrLine1: general.address?.addrLine1,
            addrLine2: general.address?.addrLine2,
            subDistrict: general.address?.subDistrict,
            district: general.address?.district,
            province: general.address?.province,
            postalCode: general.address?.postalCode,
            country: general.address?.country?.name || '', // ใช้ key ที่ถูกต้องจาก object
            createdDate: general.address?.createdDate,
            countryId: general.address?.countryId,
            provinceId: general.address?.provinceId
          }
        });
      }

      // แก้ไขให้ใช้ข้อมูลจาก generalFormData และ shippingFormData เท่านั้น
      // หากมี shippingFormData เพิ่ม สามารถใช้ได้
      if (response.shippingFormData) {
        this.shippingForm.patchValue(response.shippingFormData);
      }
    }, error => {
      console.error('❌ Error loading form data', error);
    });
}


  onSubmit() {
    if (this.generalForm.valid && this.shippingForm.valid) {
      const combinedData = {
        generalFormData: this.generalForm.value,
        shippingFormData: this.shippingForm.value
      };

      console.log('✅ Combined Form Data:', combinedData);

      // ส่งข้อมูลไปอัปเดต
      this.submitFormData(this.id, combinedData);
    } else {
      console.log('❌ One or both forms are invalid');
    }
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
