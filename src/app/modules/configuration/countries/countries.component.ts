// tslint:disable:no-string-literal

import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CountriesService } from '../_services';
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
import { DeleteCountryModalComponent } from './components/delete-country-modal/delete-country-modal.component';
import { DeleteCountriesModalComponent } from './components/delete-countries-modal/delete-countries-modal.component';
import { UpdateCountriesStatusModalComponent } from './components/update-countries-status-modal/update-countries-status-modal.component';
import { FetchCountriesModalComponent } from './components/fetch-countries-modal/fetch-countries-modal.component';
import { EditCountryModalComponent } from './components/edit-country-modal/edit-country-modal.component';
import { TranslationService } from '../../i18n/translation.service';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements
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

constructor(
  private fb: FormBuilder,
  private modalService: NgbModal,
  public countriesService: CountriesService,
  public translationService: TranslationService,
) { }

// angular lifecircle hooks
ngOnInit(): void {
  this.filterForm();
  this.searchForm();
  this.countriesService.fetch();
  this.grouping = this.countriesService.grouping;
  this.paginator = this.countriesService.paginator;
  this.sorting = this.countriesService.sorting;
  const sb = this.countriesService.isLoading$.subscribe(res => this.isLoading = res);
  this.subscriptions.push(sb);
}

ngOnDestroy() {
  this.subscriptions.forEach((sb) => sb.unsubscribe());
}

// filtration
filterForm() {
  this.filterGroup = this.fb.group({
    status: [''],
    type: [''],
    searchTerm: [''],
  });
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
  const status = this.filterGroup.get('status').value;
  if (status) {
    filter['status'] = status;
  }

  const type = this.filterGroup.get('type').value;
  if (type) {
    filter['type'] = type;
  }
  this.countriesService.patchState({ filter });
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
  this.countriesService.patchState({ searchTerm });
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
  this.countriesService.patchState({ sorting });
}

// pagination
paginate(paginator: PaginatorState) {
  this.countriesService.patchState({ paginator });
}

// form actions
create() {
  this.edit(undefined);
}

edit(id: number) {
  const modalRef = this.modalService.open(EditCountryModalComponent, { size: 'xl' });
  modalRef.componentInstance.id = id;
  modalRef.result.then(() =>
    this.countriesService.fetch(),
    () => { }
  );
}

delete(id: number) {
  const modalRef = this.modalService.open(DeleteCountryModalComponent);
  modalRef.componentInstance.id = id;
  modalRef.result.then(() => this.countriesService.fetch(), () => { });
}

deleteSelected() {
  const modalRef = this.modalService.open(DeleteCountriesModalComponent);
  modalRef.componentInstance.ids = this.grouping.getSelectedRows();
  modalRef.result.then(() => this.countriesService.fetch(), () => { });
}

updateStatusForSelected() {
  const modalRef = this.modalService.open(UpdateCountriesStatusModalComponent);
  modalRef.componentInstance.ids = this.grouping.getSelectedRows();
  modalRef.result.then(() => this.countriesService.fetch(), () => { });
}

fetchSelected() {
  const modalRef = this.modalService.open(FetchCountriesModalComponent);
  modalRef.componentInstance.ids = this.grouping.getSelectedRows();
  modalRef.result.then(() => this.countriesService.fetch(), () => { });
}
}
