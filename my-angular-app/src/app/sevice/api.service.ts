import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5083/api/Testfect'; // URL ของ API

  
  constructor(private http: HttpClient) {}

  // ฟังก์ชันเพื่อดึงข้อมูลจาก API
  getCustomers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getCustomerReport(customerId: number): Observable<Blob> {
    return this.http.get<Blob>(`${this.apiUrl}/customer-report/${customerId}`, {
      responseType: 'blob' as 'json'  // ใช้ responseType เป็น blob เพื่อดึงไฟล์
    });
  }
  
  // ฟังก์ชันเพื่อดึงข้อมูลจาก API ตาม id
  getCustomerById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  // ฟังก์ชันเพื่อเพิ่มข้อมูลลูกค้า
  addCustomer(name: string) {
    return this.http.post(this.apiUrl, { name });
  }
}
