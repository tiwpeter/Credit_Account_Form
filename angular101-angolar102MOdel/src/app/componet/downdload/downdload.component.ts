import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';  // นำเข้า ActivatedRoute
import { CommonModule } from '@angular/common'; // นำเข้า CommonModule
import { DowndloadService } from '../../sevice/downdload.service';

@Component({
  selector: 'app-downdload',
  imports: [CommonModule],
  templateUrl: './downdload.component.html',
  styleUrl: './downdload.component.css'
})
  
export class DowndloadComponent implements OnInit {
   userId: string | null = null;  // ตัวแปรสำหรับเก็บค่า id

  constructor(
    private apiService: DowndloadService,
    private route: ActivatedRoute  // Inject ActivatedRoute
  ) {}

  ngOnInit(): void {
    // ดึงค่า id จาก URL
    this.userId = this.route.snapshot.paramMap.get('id');

    if (this.userId) {
      // เรียก API เพื่อดึงรายงานลูกค้าตาม ID
      this.apiService.getCustomerReport(+this.userId).subscribe({
        next: (fileBlob: Blob) => {
          // สร้างลิงก์ให้ดาวน์โหลด PDF
          const downloadLink = document.createElement('a');
          downloadLink.href = URL.createObjectURL(fileBlob);
          downloadLink.download = 'customer_report.pdf'; // กำหนดชื่อไฟล์
          downloadLink.click(); // คลิกเพื่อดาวน์โหลด
        },
        error: (err) => {
          console.error('เกิดข้อผิดพลาดในการดาวน์โหลดรายงาน:', err);
          alert('ไม่สามารถดาวน์โหลดรายงานได้');
        }
      });
    } else {
      alert('ไม่พบ ID ใน URL');
    }
  }
}