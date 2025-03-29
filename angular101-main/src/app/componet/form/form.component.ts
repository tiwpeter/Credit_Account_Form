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
  GeneralName1: '',
  GeneralTel: '',
  GeneralFax: '',
  GeneralEmail: '',
  GeneralLine: '',
  GeneralTax: '',
  GeneralBranch: ''
};
  imtemList: any;  // เก็บข้อมูลจาก API

  http = inject(HttpClient);
  router = inject(Router); // Inject Router

  OnSave() {
  const payload = {
    General: {
      GeneralName1: this.GeneralsObj.GeneralName1,
      GeneralTel: this.GeneralsObj.GeneralTel,
      GeneralFax: this.GeneralsObj.GeneralFax,
      GeneralEmail: this.GeneralsObj.GeneralEmail,
      GeneralLine: this.GeneralsObj.GeneralLine,
      GeneralTax: this.GeneralsObj.GeneralTax,
      GeneralBranch: this.GeneralsObj.GeneralBranch
    }
    };
    
  console.log('Sending:', payload);

  this.http.post("http://localhost:5083/api/testPost", payload).subscribe(
    (res) => {
      console.log('Response:', res);
    },
    (error) => {
      console.error('Error:', error);
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

}
