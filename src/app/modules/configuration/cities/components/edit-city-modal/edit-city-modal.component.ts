import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbDateAdapter, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, finalize, first, tap } from 'rxjs/operators';
import { City } from '../../../_models/city.model';
import { CitiesService, CountriesService } from '../../../_services';
import { CustomAdapter, CustomDateParserFormatter, getDateFromString } from '../../../../../_metronic/core';
import { Country } from '../../../_models/country.model';
import { TranslationService } from 'src/app/modules/i18n/translation.service';

const EMPTY_CITY: City = {
  id: undefined,
  enName: '',
  arName: '',
  country_Id: 1,
  status: 1,
};

@Component({
  selector: 'app-edit-city-modal',
  templateUrl: './edit-city-modal.component.html',
  styleUrls: ['./edit-city-modal.component.scss'],
  // NOTE: For this example we are only providing current component, but probably
  // NOTE: you will w  ant to provide your main App Module
  providers: [
    { provide: NgbDateAdapter, useClass: CustomAdapter },
    { provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter }
  ]
})
export class EditCityModalComponent implements OnInit, OnDestroy {
  @Input() id: number;
  isLoading$;
  city: City;
  formGroup: FormGroup;
  private subscriptions: Subscription[] = [];

  public countries: Country[];
  public selectedLanguage = this.translationService.getSelectedLanguage();

  constructor(
    private citiesService: CitiesService,
    private fb: FormBuilder, 
    public modal: NgbActiveModal,
    public countriesService: CountriesService,
    public translationService: TranslationService,
  ) {
    this.countriesService.fetch(); // Get Countries
    this.getAllCountries();
  }

  ngOnInit(): void {
    this.isLoading$ = this.citiesService.isLoading$;
    this.loadCity();
  }

  loadCity() {
    if (!this.id) {
      this.city = EMPTY_CITY;
      this.loadForm();
    } else {
      const sb = this.citiesService.getItemById(this.id).pipe(
        first(),
        catchError((errorMessage) => {
          this.modal.dismiss(errorMessage);
          return of(EMPTY_CITY);
        })
      ).subscribe((city: City) => {
        this.city = city;
        this.loadForm();
      });
      this.subscriptions.push(sb);
    }
  }

  loadForm() {
    this.formGroup = this.fb.group({
      enName: [this.city.enName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
      arName: [this.city.arName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
      country: [this.city.country_Id, Validators.compose([Validators.required,])],
    });
  }

  save() {
    this.prepareCity();
    if (this.city.id) {
      this.edit();
    } else {
      this.create();
    }
  }

  edit() {
    const sbUpdate = this.citiesService.update(this.city).pipe(
      tap(() => {
        this.modal.close();
      }),
      catchError((errorMessage) => {
        this.modal.dismiss(errorMessage);
        return of(this.city);
      }),
    ).subscribe(res => this.city = res);
    this.subscriptions.push(sbUpdate);
  }

  create() {
    const sbCreate = this.citiesService.create(this.city).pipe(
      tap(() => {
        this.modal.close();
      }),
      catchError((errorMessage) => {
        this.modal.dismiss(errorMessage);
        return of(this.city);
      }),
    ).subscribe((res: City) => this.city = res);
    this.subscriptions.push(sbCreate);
  }

  private prepareCity() {
    const formData = this.formGroup.value;
    this.city.enName = formData.enName;
    this.city.arName = formData.arName;
    this.city.country_Id = parseInt(formData.country == "" ? 0 : formData.country);
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

  getAllCountries() {
    this.countriesService.items$.subscribe(res => {
      if (res) {
        this.countries = res;
      }
    });
  }
}
