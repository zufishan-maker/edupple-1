import { Injectable, Inject, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TableService } from '../../../_metronic/shared/crud-table';
import { Student } from '../_models/student.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StudentsService extends TableService<Student> implements OnDestroy {
  API_URL = `${environment.apiUrl}/students`;
  constructor(@Inject(HttpClient) http) {
    super(http);
  }

  ngOnDestroy() {
    this.subscriptions.forEach(sb => sb.unsubscribe());
  }
}
