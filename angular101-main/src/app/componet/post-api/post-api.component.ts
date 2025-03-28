import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-post-api',
  imports: [],
  templateUrl: './post-api.component.html',
  styleUrl: './post-api.component.css'
})
export class PostApiComponent {
  userObj: any = {

  }
  http = inject(HttpClient);

  OnSave() {
    this.http.post("", this.userObj).subscribe((res: any) => {
      
    })
  }

}
