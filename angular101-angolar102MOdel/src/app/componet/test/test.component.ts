import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../sevice/api.service';
import { FormsModule } from '@angular/forms';  // Import FormsModule

@Component({
  selector: 'app-test',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {
  user: any = null;
  updatedUser: any = {};
  userId: string | null = null;
  isLoading: boolean = true; // Added loading state

  constructor(
    private apiService: ApiService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id');

    if (this.userId) {
      this.apiService.getCustomerById(this.userId).subscribe({
        next: (data) => {
          console.log('ข้อมูลที่ได้รับใน component:', data);
          this.user = data;
          this.updatedUser = { ...data }; // Initialize the updatedUser with the current user data
          this.isLoading = false; // Set loading to false after data is fetched
        },
        error: (err) => {
          console.error('เกิดข้อผิดพลาด:', err);
          alert('ไม่สามารถโหลดข้อมูลได้');
          this.isLoading = false; // Set loading to false if there's an error
        }
      });
    } else {
      alert('ไม่พบ ID ใน URL');
      this.isLoading = false; // Set loading to false if there's no ID in the URL
    }
  }

  // ฟังก์ชันสำหรับอัปเดตข้อมูลลูกค้า
  updateCustomer(): void {
  if (this.userId && this.updatedUser) {
    this.apiService.updateCustomer(this.userId, this.updatedUser).subscribe({
      next: (data) => {
        console.log('ข้อมูลที่อัปเดต:', data);
        this.user = data; // แสดงข้อมูลที่อัปเดตแล้ว
      },
      error: (err) => {
        console.error('เกิดข้อผิดพลาดในการอัปเดต:', err);
        alert('ไม่สามารถอัปเดตข้อมูลได้');
      }
    });
  } else {
    alert('กรุณากรอกข้อมูลที่ต้องการอัปเดต');
  }
}

}
