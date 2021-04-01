import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, delay, finalize, tap } from 'rxjs/operators';
import { StudentsService } from '../../../_services';

@Component({
  selector: 'app-delete-student-modal',
  templateUrl: './delete-student-modal.component.html',
  styleUrls: ['./delete-student-modal.component.scss']
})
export class DeleteStudentModalComponent implements OnInit, OnDestroy {
  @Input() id: number;
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private studentsService: StudentsService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  deleteStudent() {
    this.isLoading = true;
    const sb = this.studentsService.delete(this.id).pipe(
      delay(1000), // Remove it from your code (just for showing loading)
      tap(() => this.modal.close()),
      catchError((err) => {
        this.modal.dismiss(err);
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
