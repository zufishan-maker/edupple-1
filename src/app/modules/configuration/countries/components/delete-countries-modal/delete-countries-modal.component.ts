import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, delay, finalize, tap } from 'rxjs/operators';
import { CountriesService } from '../../../_services';

@Component({
  selector: 'app-delete-countries-modal',
  templateUrl: './delete-countries-modal.component.html',
  styleUrls: ['./delete-countries-modal.component.scss']
})
export class DeleteCountriesModalComponent implements OnInit, OnDestroy {
  @Input() ids: number[];
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private countriesService: CountriesService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
  }

  deleteCountries
  () {
    this.isLoading = true;
    const sb = this.countriesService.deleteItems(this.ids).pipe(
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
