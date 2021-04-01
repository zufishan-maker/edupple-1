import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, delay, finalize, tap } from 'rxjs/operators';
import { TutorsService } from '../../../_services';

@Component({
  selector: 'app-delete-tutors-modal',
  templateUrl: './delete-tutors-modal.component.html',
  styleUrls: ['./delete-tutors-modal.component.scss']
})
export class DeleteTutorsModalComponent implements OnInit, OnDestroy {
  @Input() ids: number[];
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private tutorsService: TutorsService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  deleteTutors() {
    this.isLoading = true;
    const sb = this.tutorsService.deleteItems(this.ids).pipe(
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
