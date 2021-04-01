import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { InlineSVGModule } from 'ng-inline-svg';
import { StudentsComponent } from './students/students.component';
import { StudentComponent } from './student.component';
import { StudentRoutingModule } from './student-routing.module';
import { CRUDTableModule } from '../../_metronic/shared/crud-table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DeleteStudentModalComponent } from './students/components/delete-student-modal/delete-student-modal.component';
import { DeleteStudentsModalComponent } from './students/components/delete-students-modal/delete-students-modal.component';
import { FetchStudentsModalComponent } from './students/components/fetch-students-modal/fetch-students-modal.component';
import { UpdateStudentsStatusModalComponent } from './students/components/update-students-status-modal/update-students-status-modal.component';
import { EditStudentModalComponent } from './students/components/edit-student-modal/edit-student-modal.component';
import { NgbDatepickerModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslationModule } from '../i18n/translation.module';
import { CoreModule } from 'src/app/_metronic/core';
import { TranslateModule } from '@ngx-translate/core';
import { DetailsStudentModalComponent } from './students/components/details-student-modal/details-student-modal.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

@NgModule({
  declarations: [
    StudentComponent,
    StudentsComponent,
    DeleteStudentModalComponent,
    DeleteStudentsModalComponent,
    FetchStudentsModalComponent,
    UpdateStudentsStatusModalComponent,
    EditStudentModalComponent,
    DetailsStudentModalComponent
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
    DeleteStudentModalComponent,
    DeleteStudentsModalComponent,
    UpdateStudentsStatusModalComponent,
    FetchStudentsModalComponent,
    EditStudentModalComponent,
    DetailsStudentModalComponent
  ]
})
export class StudentModule {}
