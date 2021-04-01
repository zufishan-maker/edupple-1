// tslint:disable:no-string-literal

import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CitiesService, CountriesService } from '../_services';
import {
  GroupingState,
  PaginatorState,
  SortState,
  ICreateAction,
  IEditAction,
  IDeleteAction,
  IDeleteSelectedAction,
  IFetchSelectedAction,
  IUpdateStatusForSelectedAction,
  ISortView,
  IFilterView,
  IGroupingView,
  ISearchView,
} from '../../../_metronic/shared/crud-table';
import { DeleteCityModalComponent } from './components/delete-city-modal/delete-city-modal.component';
import { DeleteCitiesModalComponent } from './components/delete-cities-modal/delete-cities-modal.component';
import { UpdateCitiesStatusModalComponent } from './components/update-cities-status-modal/update-cities-status-modal.component';
import { FetchCitiesModalComponent } from './components/fetch-cities-modal/fetch-cities-modal.component';
import { EditCityModalComponent } from './components/edit-city-modal/edit-city-modal.component';
import { TranslationService } from '../../i18n/translation.service';
import { Country } from '../_models/country.model';
import { LanguageService } from 'typescript';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.scss']
})
export class CitiesComponent implements
  OnInit,
  OnDestroy,
  ICreateAction,
  IEditAction,
  IDeleteAction,
  IDeleteSelectedAction,
  IFetchSelectedAction,
  IUpdateStatusForSelectedAction,
  ISortView,
  IFilterView,
  IGroupingView,
  ISearchView,
  IFilterView {
  paginator: PaginatorState;
  sorting: SortState;
  grouping: GroupingState;
  isLoading: boolean;
  filterGroup: FormGroup;
  searchGroup: FormGroup;
  private subscriptions: Subscription[] = []; // Read more: => https://brianflove.com/2016/12/11/anguar-2-unsubscribe-observables/

  public countries: Country[];
  public selectedLanguage = this.translationService.getSelectedLanguage();

  constructor(
    private fb: FormBuilder,
    private modalService: NgbModal,
    public citiesService: CitiesService,
    public countriesService: CountriesService,
    public translationService: TranslationService,
  ) {
    this.countriesService.fetch(); // Get Countries
    this.getAllCountries();
  }

  // angular lifecircle hooks
  ngOnInit(): void {
    this.filterForm();
    this.searchForm();
    this.citiesService.fetch();
    this.grouping = this.citiesService.grouping;
    this.paginator = this.citiesService.paginator;
    this.sorting = this.citiesService.sorting;
    const sb = this.citiesService.isLoading$.subscribe(res => this.isLoading = res);
    this.subscriptions.push(sb);
  }

  ngOnDestroy() {
    this.subscriptions.forEach((sb) => sb.unsubscribe());
  }

  // filtration
  filterForm() {
    this.filterGroup = this.fb.group({
      country: [''],
      status: [''],
      type: [''],
      searchTerm: [''],
    });
    this.subscriptions.push(
      this.filterGroup.controls.country.valueChanges.subscribe(() =>
        this.filter()
      )
    );
    this.subscriptions.push(
      this.filterGroup.controls.status.valueChanges.subscribe(() =>
        this.filter()
      )
    );
    this.subscriptions.push(
      this.filterGroup.controls.type.valueChanges.subscribe(() => this.filter())
    );
  }

  filter() {
    const filter = {};
   
    const country_Id = this.filterGroup.get('country').value;

    if(country_Id) {
      filter['country_Id'] = country_Id;
    }

    const status = this.filterGroup.get('status').value;
    if (status) {
      filter['status'] = status;
    }

    const type = this.filterGroup.get('type').value;
    if (type) {
      filter['type'] = type;
    }
    this.citiesService.patchState({ filter });
  }

  // search
  searchForm() {
    this.searchGroup = this.fb.group({
      searchTerm: [''],
    });
    const searchEvent = this.searchGroup.controls.searchTerm.valueChanges
      .pipe(
        /*
      The user can type quite quickly in the input box, and that could trigger a lot of server requests. With this operator,
      we are limiting the amount of server requests emitted to a maximum of one every 150ms
      */
        debounceTime(150),
        distinctUntilChanged()
      )
      .subscribe((val) => this.search(val));
    this.subscriptions.push(searchEvent);
  }

  search(searchTerm: string) {
    this.citiesService.patchState({ searchTerm });
  }

  // sorting
  sort(column: string) {
    const sorting = this.sorting;
    const isActiveColumn = sorting.column === column;
    if (!isActiveColumn) {
      sorting.column = column;
      sorting.direction = 'asc';
    } else {
      sorting.direction = sorting.direction === 'asc' ? 'desc' : 'asc';
    }
    this.citiesService.patchState({ sorting });
  }

  // pagination
  paginate(paginator: PaginatorState) {
    this.citiesService.patchState({ paginator });
  }

  // form actions
  create() {
    this.edit(undefined);
  }

  edit(id: number) {
    const modalRef = this.modalService.open(EditCityModalComponent, { size: 'xl' });
    modalRef.componentInstance.id = id;
    modalRef.result.then(() =>
      this.citiesService.fetch(),
      () => { }
    );
  }

  delete(id: number) {
    const modalRef = this.modalService.open(DeleteCityModalComponent);
    modalRef.componentInstance.id = id;
    modalRef.result.then(() => this.citiesService.fetch(), () => { });
  }

  deleteSelected() {
    const modalRef = this.modalService.open(DeleteCitiesModalComponent);
    modalRef.componentInstance.ids = this.grouping.getSelectedRows();
    modalRef.result.then(() => this.citiesService.fetch(), () => { });
  }

  updateStatusForSelected() {
    const modalRef = this.modalService.open(UpdateCitiesStatusModalComponent);
    modalRef.componentInstance.ids = this.grouping.getSelectedRows();
    modalRef.result.then(() => this.citiesService.fetch(), () => { });
  }

  fetchSelected() {
    const modalRef = this.modalService.open(FetchCitiesModalComponent);
    modalRef.componentInstance.ids = this.grouping.getSelectedRows();
    modalRef.result.then(() => this.citiesService.fetch(), () => { });
  }

  getAllCountries() {
    this.countriesService.items$.subscribe(res => {
      if (res) {
        this.countries = res;
      }
    });
  }

  getCountryById(id) {
    return this.selectedLanguage == 'ar' ? this.countries.find(x => x.id == id).arName : this.countries.find(x => x.id == id).enName;
  }
}
