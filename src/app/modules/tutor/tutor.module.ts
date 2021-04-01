import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { InlineSVGModule } from 'ng-inline-svg';
import { TutorsComponent } from './tutors/tutors.component';
import { TutorComponent } from './tutor.component';
import { StudentRoutingModule } from './tutor-routing.module';
import { CRUDTableModule } from '../../_metronic/shared/crud-table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DeleteTutorModalComponent } from './tutors/components/delete-tutor-modal/delete-tutor-modal.component';
import { DeleteTutorsModalComponent } from './tutors/components/delete-tutors-modal/delete-tutors-modal.component';
import { FetchTutorsModalComponent } from './tutors/components/fetch-tutors-modal/fetch-tutors-modal.component';
import { UpdateTutorsStatusModalComponent } from './tutors/components/update-tutors-status-modal/update-tutors-status-modal.component';
import { EditTutorModalComponent } from './tutors/components/edit-tutor-modal/edit-tutor-modal.component';
import { NgbDatepickerModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslationModule } from '../i18n/translation.module';
import { CoreModule } from 'src/app/_metronic/core';
import { TranslateModule } from '@ngx-translate/core';
import { DetailsTutorModalComponent } from './tutors/components/details-tutor-modal/details-tutor-modal.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

@NgModule({
  declarations: [
    TutorComponent,
    TutorsComponent,
    DeleteTutorModalComponent,
    DeleteTutorsModalComponent,
    FetchTutorsModalComponent,
    UpdateTutorsStatusModalComponent,
    EditTutorModalComponent,
    DetailsTutorModalComponent
  ],
  imports: [
    CommonModule,
    TranslationModule,
    HttpClientModule,
    StudentRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    InlineSVGModule,
    CRUDTableModule,
    NgbModalModule,
    NgbDatepickerModule,
    NgMultiSelectDropDownModule.forRoot()

  ],
  entryComponents: [
    DeleteTutorModalComponent,
    DeleteTutorsModalComponent,
    FetchTutorsModalComponent,
    UpdateTutorsStatusModalComponent,
    EditTutorModalComponent,
    DetailsTutorModalComponent
  ]
})
export class TutorModule { }
