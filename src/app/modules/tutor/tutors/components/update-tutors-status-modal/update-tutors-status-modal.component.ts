import { Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, delay, finalize, first, tap } from 'rxjs/operators';
import { Tutor } from '../../../_models/tutor.model';
import { TutorsService } from '../../../_services';

@Component({
  selector: 'app-update-tutors-status-modal',
  templateUrl: './update-tutors-status-modal.component.html',
  styleUrls: ['./update-tutors-status-modal.component.scss']
})
export class UpdateTutorsStatusModalComponent implements OnInit, OnDestroy {
  @Input() ids: number[];
  status = 2;
  tutors: Tutor[] = [];
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private tutorsService: TutorsService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.loadTutors();
  }

  loadTutors() {
    const sb = this.tutorsService.items$.pipe(
      first()
    ).subscribe((res: Tutor[]) => {
      this.tutors = res.filter(c => this.ids.indexOf(c.id) > -1);
    });
    this.subscriptions.push(sb);
  }

  updateTutorsStatus() {
    this.isLoading = true;
    const sb = this.tutorsService.updateStatusForItems(this.ids, +this.status).pipe(
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
