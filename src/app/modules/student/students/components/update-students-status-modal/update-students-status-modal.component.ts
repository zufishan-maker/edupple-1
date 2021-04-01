import { Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, delay, finalize, first, tap } from 'rxjs/operators';
import { Student } from '../../../_models/student.model';
import { StudentsService } from '../../../_services';

@Component({
  selector: 'app-update-students-status-modal',
  templateUrl: './update-students-status-modal.component.html',
  styleUrls: ['./update-students-status-modal.component.scss']
})
export class UpdateStudentsStatusModalComponent implements OnInit, OnDestroy {
  @Input() ids: number[];
  status = 2;
  students: Student[] = [];
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private studentsService: StudentsService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.loadStudents();
  }

  loadStudents() {
    const sb = this.studentsService.items$.pipe(
      first()
    ).subscribe((res: Student[]) => {
      this.students = res.filter(c => this.ids.indexOf(c.id) > -1);
    });
    this.subscriptions.push(sb);
  }

  updateStudentsStatus() {
    this.isLoading = true;
    const sb = this.studentsService.updateStatusForItems(this.ids, +this.status).pipe(
      delay(1000), // Remove it from your code (just for showing loading)
      tap(() => this.modal.close()),
      catchError((errorMessage) => {
        this.modal.dismiss(errorMessage);
        return of(undefined);
      }),
      finalize(() => {
        this.isLoading = false;
      })
    ).subscribe();
    this.subscriptions.push(sb);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sb => sb.unsubscribe());
  }
}
