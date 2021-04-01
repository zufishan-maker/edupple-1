import { Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { of, Subscription } from 'rxjs';
import { catchError, delay, finalize, first, tap } from 'rxjs/operators';
import { City } from '../../../_models/city.model';
import { CitiesService } from '../../../_services';

@Component({
  selector: 'app-update-cities-status-modal',
  templateUrl: './update-cities-status-modal.component.html',
  styleUrls: ['./update-cities-status-modal.component.scss']
})
export class UpdateCitiesStatusModalComponent implements OnInit, OnDestroy {
  @Input() ids: number[];
  status = 2;
  cities: City[] = [];
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private citiesService: CitiesService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.loadCities();
  }

  loadCities() {
    const sb = this.citiesService.items$.pipe(
      first()
    ).subscribe((res: City[]) => {
      this.cities = res.filter(c => this.ids.indexOf(c.id) > -1);
    });
    this.subscriptions.push(sb);
  }

  updateCitiesStatus() {
    this.isLoading = true;
    const sb = this.citiesService.updateStatusForItems(this.ids, +this.status).pipe(
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
