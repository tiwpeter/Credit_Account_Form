import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private apiUrl = 'http://localhost:5083/api/customer/customer'; // URL ของ API

  
  constructor(private http: HttpClient) {}

  // ฟังก์ชันเพื่อดึงข้อมูลจาก API
  getCustomers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}
