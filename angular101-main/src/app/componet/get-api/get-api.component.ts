import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-get-api',
  imports: [],
  templateUrl: './get-api.component.html',
  styleUrl: './get-api.component.css'
})
export class GetApiComponent {
  catlist: any[] = [];

  // object เก็บข้อมูล จาก api
  catfact: any;

  constructor(private http: HttpClient) {

  }

  getCat() {
    this.http.get("https://catfact.ninja/fact").subscribe((result: any) => {
    // result.fact = เก็บข้อมูล api ไว้ใน catfact
    this.catfact = result.fact;
  });
}
}
