import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  //กำหนดตัวแปรเก็บ url
  private apiUrl = 'https://jsonplaceholder.typicode.com/posts'; // ตัวอย่าง URL

  // เรียกใช้ HttpClient
  constructor(private http: HttpClient) {}

  //getUser ตัวแปร เรียก HTTP GET request ไปยัง API endpoint ที่กำหนดไว้ใน apiUrl ซึ่งจะคืนค่าเป็นข้อมูลจาก API กลับมาให้เรา
  getUser(): Observable<any> {
    return this.http.get(this.apiUrl); // การเรียก GET request
  }
  
//สร้างฟังก์ชัน createPost() ที่ใช้สำหรับการส่ง HTTP POST request ไปยัง API endpoint
  createPost(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data); // การเรียก POST request
  }
}