import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';  // นำเข้า ActivatedRoute
import { CommonModule } from '@angular/common'; // นำเข้า CommonModule
import { ApiService } from '../../sevice/api.service';

@Component({
  selector: 'app-test',
  standalone: true, // ✅ ต้องมีถ้าใช้ imports
  imports: [CommonModule], // เพิ่ม CommonModule ที่นี่
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {
  user: any = null; // เปลี่ยนจาก array เป็น object เพราะดึงข้อมูลเฉพาะ 1 คน
  userId: string | null = null;  // ตัวแปรสำหรับเก็บค่า id

  constructor(
    private apiService: ApiService,
    private route: ActivatedRoute  // Inject ActivatedRoute
  ) {}

  ngOnInit(): void {
    // ดึงค่า id จาก URL
    this.userId = this.route.snapshot.paramMap.get('id');

    if (this.userId) {
      this.apiService.getCustomerById(this.userId).subscribe({
        next: (data) => {
          console.log('ข้อมูลที่ได้รับใน component:', data); // แสดงใน console เมื่อได้ข้อมูล
          this.user = data; // เก็บข้อมูลลูกค้าในตัวแปร user
        },
        error: (err) => {
          console.error('เกิดข้อผิดพลาด:', err);
          alert('ไม่สามารถโหลดข้อมูลได้');
        }
      });
    } else {
      alert('ไม่พบ ID ใน URL');
    }
  }
}
