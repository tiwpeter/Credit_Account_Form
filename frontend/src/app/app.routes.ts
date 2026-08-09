import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'register',
    loadComponent: () =>
      import('./features/register/create/register-form.component').then(
        (m) => m.RegisterFormComponent
      )
  },
  
  // เปลี่ยนจาก 'test' เป็น 'register' (หรือ path หน้าแรกที่คุณต้องการ)
  { path: '', redirectTo: 'register', pathMatch: 'full' },
  { path: '**', redirectTo: 'register' }
];