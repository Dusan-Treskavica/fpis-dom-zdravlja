import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AppAuthService } from '../services/app-auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private appAuthService: AppAuthService,
    private router: Router) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    // Get property name on security object to check
    //let claimType: string = next.data["claimType"];

    //var route = next.routeConfig?.path;
    
    if (this.appAuthService.IsUserAuthenticated) {
      if(this.appAuthService.IsUserAuthenticated && state.url === '/login' ){
        this.router.navigate(['home']);
        return false;
      }
      return true;
    }
    else {
      if(!this.appAuthService.IsUserAuthenticated && state.url.startsWith('/login')){
        return true;
      }
      this.router.navigate(['login'], { queryParams: { returnUrl: state.url } });
      return false;
    }
  }
}