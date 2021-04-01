import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { Tutor } from '../../../_models/tutor.model';
import { TutorsService } from '../../../_services';

@Component({
  selector: 'app-fetch-tutors-modal',
  templateUrl: './fetch-tutors-modal.component.html',
  styleUrls: ['./fetch-tutors-modal.component.scss']
})
export class FetchTutorsModalComponent implements OnInit, OnDestroy {
  @Input() ids: number[];
  tutors: Tutor[] = [];
  isLoading = false;
  subscriptions: Subscription[] = [];

  constructor(private tutorsService: TutorsService, public modal: NgbActiveModal) { }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers() {
    const sb = this.tutorsService.items$.pipe(
      first()
    ).subscribe((res: Tutor[]) => {
      this.tutors = res.filter(c => this.ids.indexOf(c.id) > -1);
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
