import {Route} from '@angular/router';
import { HomePageComponent } from "../home-module/home-page/home-page.component";
import { AuthGuard } from '../shared-module/guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { PageNotFoundComponent } from "./page-not-found/page-not-found.component";

export const ROUTES: Route[] = [
    {
        path: 'home',
        component: HomePageComponent,
    },
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component: LoginComponent,
        canActivate: [AuthGuard],
    },
    { 
        path: '**', 
        component: PageNotFoundComponent
    },
    {
        path: '',
        loadChildren: () => import('../kartica-za-evidenciju-module/kartica-za-evidenciju.module').then(m => m.KarticaZaEvidencijuModule),
        data: { path: 'kartica-za-evidenciju' }
    },
    {
        path: '',
        loadChildren: () => import('../analiza-module/analiza.module').then(m => m.AnalizaModule),
        data: { path: 'analiza' }
    }

]