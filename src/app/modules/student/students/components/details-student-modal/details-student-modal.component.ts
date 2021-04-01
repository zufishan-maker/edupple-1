import { AfterViewInit, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbDateAdapter, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, finalize, first, tap } from 'rxjs/operators';
import { Student } from '../../../_models/student.model';
import { StudentsService } from '../../../_services';
import { CustomAdapter, CustomDateParserFormatter, getDateFromString } from '../../../../../_metronic/core';
import { CitiesService, CountriesService } from 'src/app/modules/configuration/_services';
import { Country } from 'src/app/modules/configuration/_models/country.model';
import { City } from 'src/app/modules/configuration/_models/city.model';
import { TranslationService } from 'src/app/modules/i18n/translation.service';

const EMPTY_STUDENT: Student = {
  id: undefined,
  fullName: '',
  universityName: '',
  email: '',
  phoneNumber: '',
  gender: 'Female',
  country: 1,
  city: 1,
  subjects: [],
  status: 2,
  dob: undefined,
  dateOfBbirth: ''
};

@Component({
  selector: 'app-details-student-modal',
  templateUrl: './details-student-modal.component.html',
  styleUrls: ['./details-student-modal.component.scss'],
  // NOTE: For this example we are only providing current component, but probably
  // NOTE: you will w  ant to provide your main App Module
  providers: [
    { provide: NgbDateAdapter, useClass: CustomAdapter },
    { provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter }
  ]
})
export class DetailsStudentModalComponent implements OnInit, OnDestroy {
  @Input() id: number;
  isLoading$;
  public student: Student = EMPTY_STUDENT;
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
    private studentsService: StudentsService,
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
    this.isLoading$ = this.studentsService.isLoading$;
    this.loadStudent();
  }

  loadStudent() {
    if (!this.id) {
      this.student = EMPTY_STUDENT;
      this.loadForm();
      //this.citiesList = this.getAllCitiesByCountryId(this.student.country);
    } else {
      const sb = this.studentsService.getItemById(this.id).pipe(
        first(),
        catchError((errorMessage) => {
          this.modal.dismiss(errorMessage);
          return of(EMPTY_STUDENT);
        })
      ).subscribe((student: Student) => {
        this.student = student;
        this.loadForm();
        this.citiesList = this.getAllCitiesByCountryId(student.country);
      });
      this.subscriptions.push(sb);
    }
  }

  loadForm() {
    this.formGroup = this.fb.group({
      fullName: [this.student.fullName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
      universityName: [this.student.universityName, Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
      email: [this.student.email, Validators.compose([Validators.required, Validators.email])],
      phoneNumber: [this.student.phoneNumber, Validators.compose([Validators.required])],
      gender: [this.student.gender, Validators.compose([Validators.required])],
      country: [this.student.country, Validators.compose([Validators.required])],
      city: [this.student.city, Validators.compose([Validators.required])],
      subjects: [this.student.subjects, Validators.compose([Validators.required])],
      dob: [this.student.dateOfBbirth, Validators.compose([Validators.nullValidator])]
    });

    this.formGroup.controls.country.valueChanges.subscribe(val => {
      this.citiesList = this.getAllCitiesByCountryId(val);
    });
  }

  save() {
    this.prepareStudent();
    if (this.student.id) {
      this.edit();
    } else {
      this.create();
    }
  }

  edit() {
    const sbUpdate = this.studentsService.update(this.student).pipe(
      tap(() => {
        this.modal.close();
      }),
      catchError((errorMessage) => {
        this.modal.dismiss(errorMessage);
        return of(this.student);
      }),
    ).subscribe(res => this.student = res);
    this.subscriptions.push(sbUpdate);
  }

  create() {
    const sbCreate = this.studentsService.create(this.student).pipe(
      tap(() => {
        this.modal.close();
      }),
      catchError((errorMessage) => {
        this.modal.dismiss(errorMessage);
        return of(this.student);
      }),
    ).subscribe((res: Student) => this.student = res);
    this.subscriptions.push(sbCreate);
  }

  private prepareStudent() {
    const formData = this.formGroup.value;
    this.student.dob = new Date(formData.dob);
    this.student.email = formData.email;
    this.student.fullName = formData.fullName;
    this.student.dateOfBbirth = formData.dob;
    this.student.universityName = formData.universityName;
    this.student.phoneNumber = formData.phoneNumber;
    this.student.country = +formData.country;
    this.student.city = formData.city;
    this.student.gender = formData.gender;
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
        if(this.student) {
          this.citiesList = res.filter(x=>x.id == this.student.country);
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
