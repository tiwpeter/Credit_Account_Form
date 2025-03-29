import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common'; // 👈 เพิ่ม CommonModule ด้วย

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css'],
  standalone: true, // ถ้าใช้ standalone component
  imports: [CommonModule, HttpClientModule], // 👈 ต้องมี CommonModule ถึงจะใช้ *ngIf ได้
})
export class ItemDetailComponent implements OnInit {
  itemId: number = 0;
  itemDetail: any;

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    this.itemId = +this.route.snapshot.paramMap.get('id')!;

    this.http.get(`http://localhost:5083/api/testget/${this.itemId}`).subscribe((data: any) => {
      this.itemDetail = data;
      console.log('Item Detail:', this.itemDetail);
    });
  }
}
