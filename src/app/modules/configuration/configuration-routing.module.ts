import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConfigurationComponent } from './configuration.component';
import { CitiesComponent } from './cities/cities.component';
import { CountriesComponent } from './countries/countries.component';

const routes: Routes = [
  {
    path: '',
    component: ConfigurationComponent,
    children: [
      {
        path: 'countries',
        component: CountriesComponent,
      },
      {
        path: 'cities',
        component: CitiesComponent,
      },
      { path: '', redirectTo: 'countries', pathMatch: 'full' },
      { path: '**', redirectTo: 'countries', pathMatch: 'full' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ConfigurationRoutingModule {}
