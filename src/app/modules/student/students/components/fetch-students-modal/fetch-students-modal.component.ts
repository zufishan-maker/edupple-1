import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { Student } from '../../../_models/student.model';
import { StudentsService } from '../../../_services';

@Component({
  selector: 'app-fetch-students-modal',
  templateUrl: './fetch-students-modal.component.html',
  styleUrls: ['./fetch-students-modal.component.scss']
})
export class FetchStudentsModalComponent implements OnInit, OnDestroy {
  @Input() ids: number[];
  students: Student[] = [];
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private studentsService: StudentsService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers() {
    const sb = this.studentsService.items$.pipe(
      first()
    ).subscribe((res: Student[]) => {
      this.students = res.filter(c => this.ids.indexOf(c.id) > -1);
    });
    this.subscriptions.push(sb);
  }

  fetchSelected() {
    this.isLoading = true;
    // just imitation, call server for fetching data
    setTimeout(() => {
      this.isLoading = false;
      this.modal.close();
    }, 1000);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sb => sb.unsubscribe());
  }
}
