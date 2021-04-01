// tslint:disable:no-string-literal

import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TutorsService } from '../_services';
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
import { DeleteTutorModalComponent } from './components/delete-tutor-modal/delete-tutor-modal.component';
import { DeleteTutorsModalComponent } from './components/delete-tutors-modal/delete-tutors-modal.component';
import { UpdateTutorsStatusModalComponent } from './components/update-tutors-status-modal/update-tutors-status-modal.component';
import { FetchTutorsModalComponent } from './components/fetch-tutors-modal/fetch-tutors-modal.component';
import { EditTutorModalComponent } from './components/edit-tutor-modal/edit-tutor-modal.component';
import { TranslationService } from '../../i18n/translation.service';
import { DetailsTutorModalComponent } from './components/details-tutor-modal/details-tutor-modal.component';

@Component({
  selector: 'app-tutors',
  templateUrl: './tutors.component.html',
  styleUrls: ['./tutors.component.scss'],
})
export class TutorsComponent
  implements
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
    public tutorsService: TutorsService,
    public translationService: TranslationService,
  ) { }

  // angular lifecircle hooks
  ngOnInit(): void {
    this.filterForm();
    this.searchForm();
    this.tutorsService.fetch();
    this.grouping = this.tutorsService.grouping;
    this.paginator = this.tutorsService.paginator;
    this.sorting = this.tutorsService.sorting;
    const sb = this.tutorsService.isLoading$.subscribe(res => this.isLoading = res);
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
    this.tutorsService.patchState({ filter });
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
    this.tutorsService.patchState({ searchTerm });
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
    this.tutorsService.patchState({ sorting });
  }

  // pagination
  paginate(paginator: PaginatorState) {
    this.tutorsService.patchState({ paginator });
  }

  // form actions
  create() {
    this.edit(undefined);
  }

  edit(id: number) {
    const modalRef = this.modalService.open(EditTutorModalComponent, { size: 'xl' });
    modalRef.componentInstance.id = id;
    modalRef.result.then(() =>
      this.tutorsService.fetch(),
      () => { }
    );
  }

  delete(id: number) {
    const modalRef = this.modalService.open(DeleteTutorModalComponent);
    modalRef.componentInstance.id = id;
    modalRef.result.then(() => this.tutorsService.fetch(), () => { });
  }

  deleteSelected() {
    const modalRef = this.modalService.open(DeleteTutorsModalComponent);
    modalRef.componentInstance.ids = this.grouping.getSelectedRows();
    modalRef.result.then(() => this.tutorsService.fetch(), () => { });
  }

  updateStatusForSelected() {
    const modalRef = this.modalService.open(UpdateTutorsStatusModalComponent);
    modalRef.componentInstance.ids = this.grouping.getSelectedRows();
    modalRef.result.then(() => this.tutorsService.fetch(), () => { });
  }

  fetchSelected() {
    const modalRef = this.modalService.open(FetchTutorsModalComponent);
    modalRef.componentInstance.ids = this.grouping.getSelectedRows();
    modalRef.result.then(() => this.tutorsService.fetch(), () => { });
  }

  details(id: number) {
    const modalRef = this.modalService.open(DetailsTutorModalComponent, { size: 'xl' });
    modalRef.componentInstance.id = id;
    modalRef.result.then(() =>
      this.tutorsService.fetch(),
      () => { }
    );
  }
}
