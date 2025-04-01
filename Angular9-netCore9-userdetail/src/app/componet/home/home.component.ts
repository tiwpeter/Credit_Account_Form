import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // ✅ เพิ่มตรงนี้
import { RouterModule } from '@angular/router'; // ✅ เพิ่มตรงนี้

interface User {
  id: number;
  name: string;
  email: string;
}



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],  // ✅ แก้จาก styleUrl
  imports: [CommonModule,RouterModule]
})
export class HomeComponent {
  users: User[] = [
    { id: 1, name: 'สมชาย ใจดี', email: 'somchai@example.com' },
    { id: 2, name: 'สมหญิง สวยงาม', email: 'somying@example.com' }
  ];

  editUser(user: User) {
    console.log('Edit', user);
  }

  deleteUser(userId: number) {
    this.users = this.users.filter(user => user.id !== userId);
  }
}
