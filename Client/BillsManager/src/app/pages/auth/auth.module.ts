import { FilterPipe } from './../../shared/pipes/filter.pipe';
import { SharedModule } from '../shared.module';
import { AuthRoutingModule } from './auth-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { PasswordModule } from 'primeng/password';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, FilterPipe],
  imports: [CommonModule, AuthRoutingModule, SharedModule, PasswordModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
})
export class AuthModule {}
