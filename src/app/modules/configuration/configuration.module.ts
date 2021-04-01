import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CitiesComponent } from './cities/cities.component';
import { CountriesComponent } from './countries/countries.component';
import { ConfigurationComponent } from './configuration.component';
import { ConfigurationRoutingModule } from './configuration-routing.module';
import { TranslationModule } from '../i18n/translation.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InlineSVGModule } from 'ng-inline-svg';
import { CRUDTableModule } from 'src/app/_metronic/shared/crud-table';
import { NgbDatepickerModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { DeleteCountryModalComponent } from './countries/components/delete-country-modal/delete-country-modal.component';
import { DeleteCountriesModalComponent } from './countries/components/delete-countries-modal/delete-countries-modal.component';
import { FetchCountriesModalComponent } from './countries/components/fetch-countries-modal/fetch-countries-modal.component';
import { UpdateCountriesStatusModalComponent } from './countries/components/update-countries-status-modal/update-countries-status-modal.component';
import { EditCountryModalComponent } from './countries/components/edit-country-modal/edit-country-modal.component';
import { DeleteCityModalComponent } from './cities/components/delete-city-modal/delete-city-modal.component';
import { DeleteCitiesModalComponent } from './cities/components/delete-cities-modal/delete-cities-modal.component';
import { FetchCitiesModalComponent } from './cities/components/fetch-cities-modal/fetch-cities-modal.component';
import { UpdateCitiesStatusModalComponent } from './cities/components/update-cities-status-modal/update-cities-status-modal.component';
import { EditCityModalComponent } from './cities/components/edit-city-modal/edit-city-modal.component';

@NgModule({
  declarations: [
    ConfigurationComponent,
    CountriesComponent,
    DeleteCountryModalComponent,
    DeleteCountriesModalComponent,
    FetchCountriesModalComponent,
    UpdateCountriesStatusModalComponent,
    EditCountryModalComponent,
    CitiesComponent,
    DeleteCityModalComponent,
    DeleteCitiesModalComponent,
    FetchCitiesModalComponent,
    UpdateCitiesStatusModalComponent,
    EditCityModalComponent,
  ],
  imports: [
    CommonModule,
    ConfigurationRoutingModule,
    TranslationModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    InlineSVGModule,
    CRUDTableModule,
    NgbModalModule,
    NgbDatepickerModule,
  ],
  entryComponents: [
    DeleteCountryModalComponent,
    DeleteCountriesModalComponent,
    FetchCountriesModalComponent,
    UpdateCountriesStatusModalComponent,
    EditCountryModalComponent,
    DeleteCityModalComponent,
    DeleteCitiesModalComponent,
    FetchCitiesModalComponent,
    UpdateCitiesStatusModalComponent,
    EditCityModalComponent,
  ]
})
export class ConfigurationModule { }
