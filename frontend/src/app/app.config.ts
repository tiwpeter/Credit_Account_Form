import { ApplicationConfig } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { environment } from '../environments/environment';
import { BASE_PATH } from './api-client/variables';
import { Configuration } from './api-client/configuration';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    provideAnimations(),

    // ให้ generated client (TestService, MasterService, ...) รู้ base URL
    { provide: BASE_PATH, useValue: environment.apiUrl },
    {
      provide: Configuration,
      useValue: new Configuration({ basePath: environment.apiUrl })
    }
  ]
};
