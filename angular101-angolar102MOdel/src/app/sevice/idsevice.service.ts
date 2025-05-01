import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IdseviceService {
  private apiUrl = 'http://localhost:5259/api/Regisform'; // URL ของ API
  constructor(private http: HttpClient) { }

  updateCustomer(id: string, updatedUser: any): Observable<any> {
  return this.http.put<any>(`${this.apiUrl}/${id}`, updatedUser);
}
   // ฟังก์ชันเพื่อดึงข้อมูลจาก API ตาม id
  getCustomerById(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
}
