// user.service.ts
import { Injectable } from '@angular/core';

interface User {
  id: number;
  name: string;
  email: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private users: User[] = [
    { id: 1, name: 'สมชาย ใจดี', email: 'somchai@example.com' },
    { id: 2, name: 'สมหญิง สวยงาม', email: 'somying@example.com' }
  ];

  getUserById(id: number): User | undefined {
    return this.users.find(user => user.id === id);
  }

  getUsers(): User[] {
    return this.users;
  }
}
