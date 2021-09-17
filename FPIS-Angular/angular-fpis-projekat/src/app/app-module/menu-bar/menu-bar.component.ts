import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AppUser } from 'src/app/shared-module/models/AppUser.model';
import { AppAuthService } from '../../shared-module/services/app-auth.service';

@Component({
  selector: 'fpis-menu-bar',
  templateUrl: './menu-bar.component.html',
  styleUrls: ['./menu-bar.component.scss']
})
export class MenuBarComponent implements OnInit {

  public appUserObject: AppUser | null;
  menu = "Meni";
  constructor(private appAuthService: AppAuthService) {
    this.appUserObject = appAuthService.AppUserObject;
    //this.isAuthenticated = appAuthService.IsUserAuthenticated;
  }

  ngOnInit(): void {

  }

  logout(){
    this.appAuthService.logout();
  }

}
