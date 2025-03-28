import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-connect-c',
  templateUrl: './connect-c.component.html',
  styleUrls: ['./connect-c.component.css']  // ปรับให้เป็น styleUrls แทน styleUrl
})
export class ConnectCComponent {
  imtemList: any;  // เก็บข้อมูลจาก API
  
  constructor(private http: HttpClient) { }

  getItem() {
    this.http.get("http://localhost:5083/api/testget").subscribe((result: any) => {
      // ตรวจสอบว่า response มีข้อมูลในรูปแบบที่คาดไว้หรือไม่
      console.log(result); // ตรวจสอบข้อมูลที่ได้รับจาก API
      this.imtemList = result; // ปรับให้ตรงกับข้อมูลที่ได้จาก API
    });
  }
}
