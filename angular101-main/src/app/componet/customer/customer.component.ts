import { Component } from '@angular/core';
import { ApiService } from '../../service/api.service';

@Component({
  selector: 'app-customer',
  imports: [],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent {
 //**การประกาศตัวแปร** ที่ใช้เก็บข้อมูลในรูปแบบของ **อาร์เรย์** (array) 
 User: any[] = [];

  //นำเข้า ApiService
  constructor(private apiService: ApiService) {}

  // เรียกใช้ ApiService ด้วย apiService
  ngOnInit(): void {
    this.apiService.getUser().subscribe((data) => {
      this.User = data;
    });
  }
}