import { Injectable, OnDestroy, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin, Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';
import { TableService, TableResponseModel, ITableState, BaseModel } from '../../../../_metronic/shared/crud-table';
import { Country } from '../../_models/country.model';
import { baseFilter } from '../../../../_fake/fake-helpers/http-extenstions';
import { environment } from '../../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CountriesService extends TableService<Country> implements OnDestroy {
  API_URL = `${environment.apiUrl}/countries`;
  constructor(@Inject(HttpClient) http) {
    super(http);
  }

  // READ
  find(tableState: ITableState): Observable<TableResponseModel<Country>> {
    return this.http.get<Country[]>(this.API_URL).pipe(
      map((response: Country[]) => {
        const filteredResult = baseFilter(response, tableState);
        const result: TableResponseModel<Country> = {
          items: filteredResult.items,
          total: filteredResult.total
        };
        return result;
      })
    );
  }

  deleteItems(ids: number[] = []): Observable<any> {
    const tasks$ = [];
    ids.forEach(id => {
      tasks$.push(this.delete(id));
    });
    return forkJoin(tasks$);
  }

  updateStatusForItems(ids: number[], status: number): Observable<any> {
    return this.http.get<Country[]>(this.API_URL).pipe(
      map((countries: Country[]) => {
        return countries.filter(c => ids.indexOf(c.id) > -1).map(c => {
          c.status = status;
          return c;
        });
      }),
      exhaustMap((countries: Country[]) => {
        const tasks$ = [];
        countries.forEach(customer => {
          tasks$.push(this.update(customer));
        });
        return forkJoin(tasks$);
      })
    );
  }

  ngOnDestroy() {
    this.subscriptions.forEach(sb => sb.unsubscribe());
  }
}
