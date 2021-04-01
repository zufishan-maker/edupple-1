import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { Country } from '../../../_models/country.model';
import { CountriesService  } from '../../../_services';

@Component({
  selector: 'app-fetch-countries-modal',
  templateUrl: './fetch-countries-modal.component.html',
  styleUrls: ['./fetch-countries-modal.component.scss']
})
export class FetchCountriesModalComponent implements OnInit, OnDestroy {
  @Input() ids: number[];
  countries: Country[] = [];
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private countriesService: CountriesService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.loadCountries();
  }

  loadCountries() {
    const sb = this.countriesService.items$.pipe(
      first()
    ).subscribe((res: Country[]) => {
      this.countries = res.filter(c => this.ids.indexOf(c.id) > -1);
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
