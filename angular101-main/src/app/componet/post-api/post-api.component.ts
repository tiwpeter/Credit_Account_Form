import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms'; // 👈 ต้อง import ตรงนี้

@Component({
  selector: 'app-post-api',
  templateUrl: './post-api.component.html',
  styleUrls: ['./post-api.component.css'],
    imports: [FormsModule],
  
})
export class PostApiComponent {
  userObj: any = {
    name: ''
  };

  http = inject(HttpClient);

  OnSave() {
  const payload = {
    Name: {
      Name: this.userObj.name
    }
  };

  console.log('Sending:', payload);

  this.http.post("http://localhost:5083/api/testPost", payload).subscribe(
    (res) => {
      console.log('Response:', res);
    },
    (error) => {
      console.error('Error:', error);
    }
  );
}
}
