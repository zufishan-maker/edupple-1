import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LogoutComponent } from './logout/logout.component';
import { AuthComponent } from './auth.component';
import {TranslationModule} from '../i18n/translation.module';
import { NgbDropdownModule, NgbProgressbarModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from 'src/app/_metronic/core';
import { SubheaderModule } from 'src/app/_metronic/partials/layout/subheader/subheader.module';
import { DropdownMenusModule } from 'src/app/_metronic/partials/content/dropdown-menus/dropdown-menus.module';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegistrationComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    LogoutComponent,
    AuthComponent,
  ],
  imports: [
    CommonModule,
    TranslationModule,
    AuthRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbDropdownModule,
    NgbProgressbarModule,
    NgbTooltipModule,
    CoreModule,
    SubheaderModule,
    DropdownMenusModule,
  ]
})
export class AuthModule {}
