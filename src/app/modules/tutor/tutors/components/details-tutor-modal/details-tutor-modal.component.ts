import { AfterViewInit, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbDateAdapter, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, finalize, first, tap } from 'rxjs/operators';
import { CustomAdapter, CustomDateParserFormatter, getDateFromString } from '../../../../../_metronic/core';
import { CitiesService, CountriesService } from 'src/app/modules/configuration/_services';
import { Country } from 'src/app/modules/configuration/_models/country.model';
import { City } from 'src/app/modules/configuration/_models/city.model';
import { TranslationService } from 'src/app/modules/i18n/translation.service';
import { Tutor } from '../../../_models/tutor.model';
import { TutorsService } from '../../../_services';

const EMPTY_TUTOR: Tutor = {
  id: undefined,
  fullName: '',
  universityName: '',
  email: '',
  phoneNumber: '',
  gender: 'Female',
  country: 1,
  city: 1,
  hourlyRate: '400 AED',
  description: '',
  subjects: [
    { id: 1, item_text: 'Mumbai' }
  ],
  status: 2,
  dob: undefined,
  dateOfBbirth: ''
};

@Component({
  selector: 'app-details-tutor-modal',
  templateUrl: './details-tutor-modal.component.html',
  styleUrls: ['./details-tutor-modal.component.scss'],
  // NOTE: For this example we are only providing current component, but probably
  // NOTE: you will w  ant to provide your main App Module
  providers: [
    { provide: NgbDateAdapter, useClass: CustomAdapter },
    { provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter }
  ]
})
export class DetailsTutorModalComponent implements OnInit, OnDestroy {
  @Input() id: number;
  isLoading$;
  public tutor: Tutor = EMPTY_TUTOR;
  formGroup: FormGroup;
  private subscriptions: Subscription[] = [];

  public countriesList: Country[] = [];
  public citiesList: City[] = [];
  public citiesListAll: City[] = [];
  public selectedLanguage = this.translationService.getSelectedLanguage();

  tabs = {
    BASIC_TAB: 0,
    PAYMENT_TAB: 1,
    COURSES_TAB: 2
  };
  activeTabId = this.tabs.BASIC_TAB; // 0 => Basic info | 1 => Remarks | 2 => Specifications

  constructor(
    private tutorsService: TutorsService,
    private fb: FormBuilder,
    public modal: NgbActiveModal,
    public countriesService: CountriesService,
    public citiesService: CitiesService,
    public translationService: TranslationService
  ) {
    this.getAllCountries();
    this.getAllCities();
  }

  ngOnInit(): void {
    this.isLoading$ = this.tutorsService.isLoading$;
    this.loadTutor();
  }

  loadTutor() {
    if (!this.id) {
      this.tutor = EMPTY_TUTOR;
      this.loadForm();
      //this.citiesList = this.getAllCitiesByCountryId(this.tutor.country);
    } else {
      const sb = this.tutorsService.getItemById(this.id).pipe(
        first(),
        catchError((errorMessage) => {
          this.modal.dismiss(errorMessage);
          return of(EMPTY_TUTOR);
        })
      ).subscribe((tutor: Tutor) => {
        this.tutor = tutor;
        this.loadForm();
        this.citiesList = this.getAllCitiesByCountryId(tutor.country);
      });
      this.subscriptions.push(sb);
    }
  }

  loadForm() {
    this.formGroup = this.fb.group({
      fullName: [this.tutor.fullName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
      universityName: [this.tutor.universityName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
      email: [this.tutor.email, Validators.compose([Validators.required, Validators.email])],
      phoneNumber: [this.tutor.phoneNumber, Validators.compose([Validators.required])],
      gender: [this.tutor.gender, Validators.compose([Validators.required])],
      country: [this.tutor.country, Validators.compose([Validators.required])],
      city: [this.tutor.city, Validators.compose([Validators.required])],
      subjects: [this.tutor.subjects, Validators.compose([Validators.required])],
      dob: [this.tutor.dateOfBbirth, Validators.compose([Validators.nullValidator])]
    });

    this.formGroup.controls.country.valueChanges.subscribe(val => {
      this.citiesList = this.getAllCitiesByCountryId(val);
    });
  }

  save() {
    this.prepareTutor();
    if (this.tutor.id) {
      this.edit();
    } else {
      this.create();
    }
  }

  edit() {
    const sbUpdate = this.tutorsService.update(this.tutor).pipe(
      tap(() => {
        this.modal.close();
      }),
      catchError((errorMessage) => {
        this.modal.dismiss(errorMessage);
        return of(this.tutor);
      }),
    ).subscribe(res => this.tutor = res);
    this.subscriptions.push(sbUpdate);
  }

  create() {
    const sbCreate = this.tutorsService.create(this.tutor).pipe(
      tap(() => {
        this.modal.close();
      }),
      catchError((errorMessage) => {
        this.modal.dismiss(errorMessage);
        return of(this.tutor);
      }),
    ).subscribe((res: Tutor) => this.tutor = res);
    this.subscriptions.push(sbCreate);
  }

  private prepareTutor() {
    const formData = this.formGroup.value;
    this.tutor.dob = new Date(formData.dob);
    this.tutor.email = formData.email;
    this.tutor.fullName = formData.fullName;
    this.tutor.dateOfBbirth = formData.dob;
    this.tutor.universityName = formData.universityName;
    this.tutor.phoneNumber = formData.phoneNumber;
    this.tutor.country = +formData.country;
    this.tutor.city = formData.city;
    this.tutor.gender = formData.gender;
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
    this.countriesService.fetch(); // Get all Countries
    this.countriesService.items$.subscribe(res => {
      if (res) {
        this.countriesList = res;
      }
    });
  }

  getAllCities() {
    this.citiesService.fetch(); // Get all Cities
    this.citiesService.items$.subscribe(res => {
      if (res) {
        if(this.tutor) {
          this.citiesList = res.filter(x=>x.id == this.tutor.country);
        }
        //this.citiesList = res;
        this.citiesListAll = res;
      }
    });
  }

  getAllCitiesByCountryId(id) {
    var list: City[] = [];
    list.push(this.citiesListAll.find(x => x.country_Id == id));
    return list;
  }

  changeTab(tabId: number) {
    this.activeTabId = tabId;
  }
}
