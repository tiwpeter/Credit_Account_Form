import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http'; // เพิ่มการ import HttpClient
import { provideAnimations } from '@angular/platform-browser/animations'; // เพิ่มการ import Animations (สำหรับ Angular Material)
import { BASE_PATH } from './core/api-client/variables';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    provideAnimations(),
   { provide: BASE_PATH, useValue: '' },
  ]
};