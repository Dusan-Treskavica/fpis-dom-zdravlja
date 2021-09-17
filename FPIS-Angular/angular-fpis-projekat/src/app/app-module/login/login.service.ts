import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from '@angular/common/http';
import { LoginModel } from "./login.model";
import { AppUser } from "src/app/shared-module/models/AppUser.model";
import { AppAuthService } from "../../shared-module/services/app-auth.service";
import { APIResponse } from "src/app/shared-module/models/APIResponse.model";

@Injectable()
export class LoginService {
  constructor(private http: HttpClient,
    private appAuthService: AppAuthService) {  }

  public login(loginModel: LoginModel): Observable<APIResponse> {
    return this.appAuthService.login(loginModel);
  }
}