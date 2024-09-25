import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, provideHttpClient } from '@angular/common/http';
import { jwtInterceptorInterceptor } from './interceptors/jwt-interceptor.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideClientHydration(),provideHttpClient(), 
    provideHttpClient(),
    {
      provide: HTTP_INTERCEPTORS,
      useValue: jwtInterceptorInterceptor, // Registro del interceptor JWT
      multi: true, // Indica que pueden haber múltiples interceptores
    }
  ]
};
