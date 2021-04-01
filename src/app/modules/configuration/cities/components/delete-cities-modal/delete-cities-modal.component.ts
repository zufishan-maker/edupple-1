import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, delay, finalize, tap } from 'rxjs/operators';
import { CitiesService } from '../../../_services';

@Component({
  selector: 'app-delete-cities-modal',
  templateUrl: './delete-cities-modal.component.html',
  styleUrls: ['./delete-cities-modal.component.scss']
})
export class DeleteCitiesModalComponent implements OnInit, OnDestroy {
  @Input() ids: number[];
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private citiesService: CitiesService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  deleteCities() {
    this.isLoading = true;
    const sb = this.citiesService.deleteItems(this.ids).pipe(
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
