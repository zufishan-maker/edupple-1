import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, delay, finalize, tap } from 'rxjs/operators';
import { TutorsService } from '../../../_services';

@Component({
  selector: 'app-delete-tutor-modal',
  templateUrl: './delete-tutor-modal.component.html',
  styleUrls: ['./delete-tutor-modal.component.scss']
})
export class DeleteTutorModalComponent implements OnInit, OnDestroy {
  @Input() id: number;
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private tutorsService: TutorsService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  deleteTutor() {
    this.isLoading = true;
    const sb = this.tutorsService.delete(this.id).pipe(
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
