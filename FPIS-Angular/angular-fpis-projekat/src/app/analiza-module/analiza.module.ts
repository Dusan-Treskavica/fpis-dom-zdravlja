import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ROUTES } from './analiza.routes';
import { AnalizaDatatableComponent } from './analiza-datatable/analiza-datatable.component';
import { AnalizaAddEditComponent } from './analiza-add-edit/analiza-add-edit.component';
import { FormsModule } from '@angular/forms';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { NgxPaginationModule } from 'ngx-pagination';
import { SharedModule } from '../shared-module/shared.module';



@NgModule({
  declarations: [
    AnalizaDatatableComponent,
    AnalizaAddEditComponent
  ],
  imports: [
    RouterModule.forChild(ROUTES),
    CommonModule,
    FormsModule,
    MDBBootstrapModule,
    NgxPaginationModule,
    SharedModule
  ],
  exports: [
    AnalizaDatatableComponent,
    AnalizaAddEditComponent
  ]
})
export class AnalizaModule { }
