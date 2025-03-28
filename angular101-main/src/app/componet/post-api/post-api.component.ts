import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms'; // 👈 ต้อง import ตรงนี้
import { Router } from '@angular/router'; // เพิ่ม Router เพื่อใช้ในการนำทาง
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-post-api',
  templateUrl: './post-api.component.html',
  styleUrls: ['./post-api.component.css'],
    imports: [FormsModule,CommonModule],
  
})
export class PostApiComponent {
  userObj: any = {
    name: ''
  };
  imtemList: any;  // เก็บข้อมูลจาก API

  http = inject(HttpClient);
  router = inject(Router); // Inject Router

  OnSave() {
  const payload = {
    Name: {
      Name: this.userObj.name
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
    this.router.navigate([`/item/${id}`]); // ไปยังหน้ารายละเอียดของ id นั้นๆ
  }
}
