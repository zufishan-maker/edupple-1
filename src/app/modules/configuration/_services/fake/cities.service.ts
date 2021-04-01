import { Injectable, OnDestroy, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin, Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';
import { TableService, TableResponseModel, ITableState, BaseModel } from '../../../../_metronic/shared/crud-table';
import { City } from '../../_models/city.model';
import { baseFilter } from '../../../../_fake/fake-helpers/http-extenstions';
import { environment } from '../../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CitiesService extends TableService<City> implements OnDestroy {
  API_URL = `${environment.apiUrl}/cities`;
  constructor(@Inject(HttpClient) http) {
    super(http);
  }

  // READ
  find(tableState: ITableState): Observable<TableResponseModel<City>> {
    return this.http.get<City[]>(this.API_URL).pipe(
      map((response: City[]) => {
        const filteredResult = baseFilter(response, tableState);
        const result: TableResponseModel<City> = {
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
    return this.http.get<City[]>(this.API_URL).pipe(
      map((countries: City[]) => {
        return countries.filter(c => ids.indexOf(c.id) > -1).map(c => {
          c.status = status;
          return c;
        });
      }),
      exhaustMap((countries: City[]) => {
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
