import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { first } from 'rxjs/operators';

enum ErrorStates {
  NotSubmitted,
  HasError,
  NoError,
}

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm: FormGroup;
  errorState: ErrorStates = ErrorStates.NotSubmitted;
  errorStates = ErrorStates;
  isLoading$: Observable<boolean>;

  // private fields
  private unsubscribe: Subscription[] = []; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/
  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.isLoading$ = this.authService.isLoading$;
  }

  ngOnInit(): void {
    this.initForm();
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.resetPasswordForm.controls;
  }

  initForm() {
    this.resetPasswordForm = this.fb.group({
      otp:
        ['',
          Validators.compose([
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(6)]
          ),
        ],
      newPassword:
        ['',
          Validators.compose([
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(20)]
          ),
        ],
      confirmPassword:
        ['',
          Validators.compose([
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(20)]
          ),
        ],
    }
    // { validator: this.ConfirmedValidator('newPassword', 'confirmPassword') }
    );
  }

  ConfirmedValidator(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      if (matchingControl.errors && !matchingControl.errors.confirmedValidator) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ confirmedValidator: true });
      } else {
        matchingControl.setErrors(null);
      }
    }
  }

  submit() {
    this.errorState = ErrorStates.NotSubmitted;
    const forgotPasswordSubscr = this.authService
      .forgotPassword(this.f.email.value)
      .pipe(first())
      .subscribe((result: boolean) => {
        this.errorState = result ? ErrorStates.NoError : ErrorStates.HasError;
      });
    this.unsubscribe.push(forgotPasswordSubscr);
  }
}
