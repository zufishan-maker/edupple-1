import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TutorComponent } from './tutor.component';
import { TutorsComponent } from './tutors/tutors.component';

const routes: Routes = [
  {
    path: '',
    component: TutorComponent,
    children: [
      {
        path: 'tutors',
        component: TutorsComponent,
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
      { path: '', redirectTo: 'tutors', pathMatch: 'full' },
      { path: '**', redirectTo: 'tutors', pathMatch: 'full' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StudentRoutingModule {}
