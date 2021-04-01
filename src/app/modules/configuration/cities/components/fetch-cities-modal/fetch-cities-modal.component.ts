import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { City } from '../../../_models/city.model';
import { CitiesService  } from '../../../_services';

@Component({
  selector: 'app-fetch-cities-modal',
  templateUrl: './fetch-cities-modal.component.html',
  styleUrls: ['./fetch-cities-modal.component.scss']
})
export class FetchCitiesModalComponent implements OnInit, OnDestroy {
  @Input() ids: number[];
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
