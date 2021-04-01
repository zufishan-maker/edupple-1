import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { StudentComponent } from './student.component';
import { StudentsComponent } from './students/students.component';

const routes: Routes = [
  {
    path: '',
    component: StudentComponent,
    children: [
      {
        path: 'students',
        component: StudentsComponent,
      },
      // {
      //   path: 'products',
      //   component: ProductsComponent,
      // },
      // {
      //   path: 'product/add',
      //   component: ProductEditComponent
      // },
      // {
      //   path: 'product/edit',
      //   component: ProductEditComponent
      // },
      // {
      //   path: 'product/edit/:id',
      //   component: ProductEditComponent
      // },
      { path: '', redirectTo: 'students', pathMatch: 'full' },
      { path: '**', redirectTo: 'students', pathMatch: 'full' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StudentRoutingModule {}
