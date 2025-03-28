import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-item-detail',
  templateUrl: './item-detail.component.html',
  styleUrls: ['./item-detail.component.css']
})
export class ItemDetailComponent implements OnInit {
  itemId: number = 0;
  itemDetail: any;

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    this.itemId = +this.route.snapshot.paramMap.get('id')!; // Get id from the URL

    // ดึงข้อมูลของ item ตาม id
    this.http.get(`http://localhost:5083/api/testget/${this.itemId}`).subscribe((data: any) => {
      this.itemDetail = data;
      console.log('Item Detail:', this.itemDetail);
    });
  }
}
