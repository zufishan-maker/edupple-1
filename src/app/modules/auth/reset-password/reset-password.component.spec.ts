import {
  waitForAsync,
  ComponentFixture,
  TestBed,
  getTestBed,
} from '@angular/core/testing';

import { ResetPasswordComponent } from './reset-password.component';
import { Routes } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { Observable, of } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';

const fakeAuth = {
  email: 'admin@demo.com',
  password: 'demo',
};

class FakeAuthService {
  forgotPassword(email: string): Observable<boolean> {
    const isChecked = email.toLowerCase() === fakeAuth.email.toLowerCase();
    return of(isChecked);
  }
}

describe('ResetPasswordComponent', () => {
  let component: ResetPasswordComponent;
  let fixture: ComponentFixture<ResetPasswordComponent>;
  let injector;
  let authService: AuthService;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, FormsModule, HttpClientTestingModule],
      declarations: [ResetPasswordComponent],
      providers: [
        {
          provide: AuthService,
          useClass: FakeAuthService,
        },
      ],
    }).compileComponents();

    injector = getTestBed();
    authService = injector.get(AuthService);
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
