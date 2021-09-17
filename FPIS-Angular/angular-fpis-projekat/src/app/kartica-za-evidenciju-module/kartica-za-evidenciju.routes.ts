import {Route} from '@angular/router';
import { AuthGuard } from '../shared-module/guards/auth.guard';
import { KarticaZaEvidencijuAddEditComponent } from './kartica-za-evidenciju-add-edit/kartica-za-evidenciju-add-edit.component';
import { KarticaZaEvidencijuDatatableComponent } from "./kartica-za-evidenciju-datatable/kartica-za-evidenciju-datatable.component";

export const ROUTES: Route[] = [
    {
      path: 'kartica-za-evidenciju',
      component: KarticaZaEvidencijuDatatableComponent,
      canActivate: [AuthGuard],
    },
    {
        path: 'kartica-za-evidenciju/kartica-za-evidenciju-add-edit/:sifraKartice',
        component: KarticaZaEvidencijuAddEditComponent,
        canActivate: [AuthGuard],
    }
]