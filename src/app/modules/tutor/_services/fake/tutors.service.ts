import { Injectable, OnDestroy, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin, Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';
import { TableService, TableResponseModel, ITableState, BaseModel } from '../../../../_metronic/shared/crud-table';
import { Tutor } from '../../_models/tutor.model';
import { baseFilter } from '../../../../_fake/fake-helpers/http-extenstions';
import { environment } from '../../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TutorsService extends TableService<Tutor> implements OnDestroy {
  API_URL = `${environment.apiUrl}/tutors`;
  constructor(@Inject(HttpClient) http) {
    super(http);
  }

  // READ
  find(tableState: ITableState): Observable<TableResponseModel<Tutor>> {
    return this.http.get<Tutor[]>(this.API_URL).pipe(
      map((response: Tutor[]) => {
        const filteredResult = baseFilter(response, tableState);
        const result: TableResponseModel<Tutor> = {
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
    return this.http.get<Tutor[]>(this.API_URL).pipe(
      map((tutors: Tutor[]) => {
        return tutors.filter(c => ids.indexOf(c.id) > -1).map(c => {
          c.status = status;
          return c;
        });
      }),
      exhaustMap((tutors: Tutor[]) => {
        const tasks$ = [];
        tutors.forEach(customer => {
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
