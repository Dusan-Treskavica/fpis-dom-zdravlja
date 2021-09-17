import {Route} from '@angular/router';
import { AuthGuard } from '../shared-module/guards/auth.guard';
import { AnalizaAddEditComponent } from './analiza-add-edit/analiza-add-edit.component';
import { AnalizaDatatableComponent } from './analiza-datatable/analiza-datatable.component';

export const ROUTES: Route[] = [
    {
        path: 'analiza',
        component: AnalizaDatatableComponent,
        canActivate: [AuthGuard],
    },
    {
        path: 'analiza/analiza-add-edit/:sifraAnalize',
        component: AnalizaAddEditComponent,
        canActivate: [AuthGuard],
    }
]