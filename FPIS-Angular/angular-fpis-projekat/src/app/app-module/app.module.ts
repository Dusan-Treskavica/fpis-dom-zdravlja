import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuBarComponent } from './menu-bar/menu-bar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HomeModule } from '../home-module/home.module';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { RouterModule } from '@angular/router';
import { ROUTES } from './app.routes';
import { KarticaZaEvidencijuModule } from '../kartica-za-evidenciju-module/kartica-za-evidenciju.module';
import { AnalizaModule } from '../analiza-module/analiza.module';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FpisApiHttpInterceptor } from './interceptor/http.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { LoginComponent } from './login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AppAuthService } from '../shared-module/services/app-auth.service';
import { SharedModule } from '../shared-module/shared.module';
import { AuthGuard } from '../shared-module/guards/auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    MenuBarComponent,
    PageNotFoundComponent,
    LoginComponent,
  ],
  imports: [
    RouterModule.forRoot(ROUTES),
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    HomeModule,
    ReactiveFormsModule,
    KarticaZaEvidencijuModule,
    AnalizaModule,
    SharedModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-center',
      closeButton: true
    }),
    MDBBootstrapModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: FpisApiHttpInterceptor,
      multi: true
    },
    HttpClient,
    AppAuthService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
