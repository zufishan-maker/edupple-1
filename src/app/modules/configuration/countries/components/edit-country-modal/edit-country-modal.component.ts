import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbDateAdapter, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, finalize, first, tap } from 'rxjs/operators';
import { Country } from '../../../_models/country.model';
import { CountriesService } from '../../../_services';
import { CustomAdapter, CustomDateParserFormatter, getDateFromString } from '../../../../../_metronic/core';

const EMPTY_COUNTRY: Country = {
  id: undefined,
  enName: '',
  arName: '',
  ISO2: '',
  status: 1,
};

@Component({
  selector: 'app-edit-country-modal',
  templateUrl: './edit-country-modal.component.html',
  styleUrls: ['./edit-country-modal.component.scss'],
  // NOTE: For this example we are only providing current component, but probably
  // NOTE: you will w  ant to provide your main App Module
  providers: [
    {provide: NgbDateAdapter, useClass: CustomAdapter},
    {provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter}
  ]
})
export class EditCountryModalComponent implements OnInit, OnDestroy {
  @Input() id: number;
  isLoading$;
  country: Country;
  formGroup: FormGroup;
  private subscriptions: Subscription[] = [];
  constructor(
    private countriesService: CountriesService,
    private fb: FormBuilder, public modal: NgbActiveModal
    ) { }

  ngOnInit(): void {
    this.isLoading$ = this.countriesService.isLoading$;
    this.loadCountry();
  }

  loadCountry() {
    if (!this.id) {
      this.country = EMPTY_COUNTRY;
      this.loadForm();
    } else {
      const sb = this.countriesService.getItemById(this.id).pipe(
        first(),
        catchError((errorMessage) => {
          this.modal.dismiss(errorMessage);
          return of(EMPTY_COUNTRY);
        })
      ).subscribe((country: Country) => {
        this.country = country;
        this.loadForm();
      });
      this.subscriptions.push(sb);
    }
  }

  loadForm() {
    this.formGroup = this.fb.group({
      enName: [this.country.enName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
      arName: [this.country.arName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
      ISO2: [this.country.ISO2, Validators.compose([Validators.required,])],
    });
  }

  save() {
    this.prepareCountry();
    if (this.country.id) {
      this.edit();
    } else {
      this.create();
    }
  }

  edit() {
    const sbUpdate = this.countriesService.update(this.country).pipe(
      tap(() => {
        this.modal.close();
      }),
      catchError((errorMessage) => {
        this.modal.dismiss(errorMessage);
        return of(this.country);
      }),
    ).subscribe(res => this.country = res);
    this.subscriptions.push(sbUpdate);
  }

  create() {
    const sbCreate = this.countriesService.create(this.country).pipe(
      tap(() => {
        this.modal.close();
      }),
      catchError((errorMessage) => {
        this.modal.dismiss(errorMessage);
        return of(this.country);
      }),
    ).subscribe((res: Country) => this.country = res);
    this.subscriptions.push(sbCreate);
  }

  private prepareCountry() {
    const formData = this.formGroup.value;
    this.country.enName = formData.enName;
    this.country.arName = formData.arName;
    this.country.ISO2 = formData.ISO2;
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sb => sb.unsubscribe());
  }

  // helpers for View
  isControlValid(controlName: string): boolean {
    const control = this.formGroup.controls[controlName];
    return control.valid && (control.dirty || control.touched);
  }

  isControlInvalid(controlName: string): boolean {
    const control = this.formGroup.controls[controlName];
    return control.invalid && (control.dirty || control.touched);
  }

  controlHasError(validation, controlName): boolean {
    const control = this.formGroup.controls[controlName];
    return control.hasError(validation) && (control.dirty || control.touched);
  }

  isControlTouched(controlName): boolean {
    const control = this.formGroup.controls[controlName];
    return control.dirty || control.touched;
  }
}
