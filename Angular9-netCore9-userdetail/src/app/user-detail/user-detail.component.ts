import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css'],
  imports:[]
})
export class UserDetailComponent implements OnInit {
  userId!: number;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.userId = +this.route.snapshot.paramMap.get('id')!;
    // ✅ คุณสามารถนำ userId นี้ไปหา user detail จาก service หรือ API ได้
    console.log('User ID:', this.userId);
  }
}
