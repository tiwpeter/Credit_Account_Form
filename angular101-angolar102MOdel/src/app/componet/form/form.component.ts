import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CustomerData } from './customer-data.model'; // import จากข้อ 1
import { FormsModule } from '@angular/forms'; // ✅ เพิ่มตรงนี้
import { CommonModule } from '@angular/common'; // ✅ เพิ่มตรงนี้

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
  imports:[FormsModule,CommonModule]
})
export class FormComponent implements OnInit {
  customerData!: CustomerData; // ใช้ Interface
  id!: string;

  constructor(private http: HttpClient, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') || '';
    if (this.id) {
      this.loadFormDataFromApi(this.id);
    }
  }

  loadFormDataFromApi(id: string): void {
    this.http.get<CustomerData>(`http://localhost:5259/api/Regisform/${id}`)
      .subscribe({
        next: (response) => {
          this.customerData = response;
          console.log('✅ Data loaded:', response);
        },
        error: (err) => {
          console.error('❌ Failed to load data:', err);
        }
      });
  }
}
