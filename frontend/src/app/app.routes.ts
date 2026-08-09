import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'register',
    loadComponent: () =>
      import('./features/register/create/register-form.component').then(
        (m) => m.RegisterFormComponent
      )
  },
  {
    path: 'test',
    loadComponent: () =>
      import('./features/test-connection/test-connection.component').then(
        (m) => m.TestConnectionComponent
      )
  },
  { path: '', redirectTo: 'test', pathMatch: 'full' },
  { path: '**', redirectTo: 'test' }
];
