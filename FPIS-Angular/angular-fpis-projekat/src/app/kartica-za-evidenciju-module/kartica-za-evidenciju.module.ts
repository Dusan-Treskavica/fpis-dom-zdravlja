import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { KarticaZaEvidencijuDatatableComponent } from './kartica-za-evidenciju-datatable/kartica-za-evidenciju-datatable.component';
import { RouterModule } from '@angular/router';
import { ROUTES } from './kartica-za-evidenciju.routes';
import { FormsModule } from '@angular/forms';
import { KarticaZaEvidencijuAddEditComponent } from './kartica-za-evidenciju-add-edit/kartica-za-evidenciju-add-edit.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { SharedModule } from '../shared-module/shared.module';



@NgModule({
  declarations: [
    KarticaZaEvidencijuDatatableComponent,
    KarticaZaEvidencijuAddEditComponent
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
    KarticaZaEvidencijuDatatableComponent
  ],
  providers: [
    DatePipe
  ]
})
export class KarticaZaEvidencijuModule { }
