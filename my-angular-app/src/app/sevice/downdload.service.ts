import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class DowndloadService {
 private apiUrl = 'http://localhost:5083/api/customer'; // URL ของ API ที่ปรับใหม่

  constructor(private http: HttpClient) {}

  // ฟังก์ชันเพื่อดึงข้อมูลรายงานลูกค้าตาม customerId และส่งกลับไฟล์ PDF
  getCustomerReport(customerId: number): Observable<Blob> {
    return this.http.get<Blob>(`${this.apiUrl}/customer-report/${customerId}`, {
      responseType: 'blob' as 'json'  // ใช้ responseType เป็น blob เพื่อดึงไฟล์
    });
  }
}