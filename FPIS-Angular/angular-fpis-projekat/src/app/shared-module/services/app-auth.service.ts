import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, throwError } from "rxjs";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { LoginModel } from "../../app-module/login/login.model";
import { AppUser } from "../models/AppUser.model";
import { catchError, tap } from "rxjs/operators";
import { APIResponse } from "../models/APIResponse.model";
import { IAppUser } from "../models/interfaces/IAppUser.interface";
import { AuthRequestModel } from "../models/AuthRequest.model";
import { Router } from "@angular/router";

@Injectable()
export class AppAuthService {
   
  private _appUserObject: AppUser = new AppUser();

  private isUserAuthenticated = new BehaviorSubject<boolean>(this.checkLoginStatus());
  private userName = new BehaviorSubject<string | null>(localStorage.getItem("username"));

  constructor(private http: HttpClient,
    private router: Router) {  }

  public login(loginModel: LoginModel): Observable<APIResponse> {
    const authRequest : AuthRequestModel = {
      grantType : "password",
      username : loginModel.username,
      password: loginModel.password,
      refreshToken: ""      
    }
    return this.http.post<APIResponse>(`api/domzdravlja/v1/auth`, JSON.stringify(authRequest)).pipe(
        tap(resp => {
            if(resp.success){
                
                Object.assign(this._appUserObject, resp.data);
                localStorage.setItem("appUser", JSON.stringify(this._appUserObject));
                //localStorage.setItem("isAuthenticated", "1");
                //localStorage.setItem("username", resp.data.userName);
                localStorage.setItem("bearerToken", resp.data.bearerToken);
                localStorage.setItem("refreshToken", resp.data.refreshToken);
            }
        }));
  }

  public getNewRefreshToken() : Observable<any> {
    const authRequest : AuthRequestModel = {
      grantType : "refresh_token",
      username : this.Username,
      password: "",
      refreshToken: localStorage.getItem("refreshToken")  
    }
    return this.http.post<APIResponse>(`api/domzdravlja/v1/auth`, JSON.stringify(authRequest)).pipe(
      tap(resp => {
            if(resp.success){
                
                Object.assign(this._appUserObject, resp.data);
                localStorage.setItem("appUser", JSON.stringify(this._appUserObject));
                //localStorage.setItem("isAuthenticated", "1");
                //localStorage.setItem("username", resp.data.userName);
                localStorage.setItem("bearerToken", resp.data.bearerToken);
                localStorage.setItem("refreshToken", resp.data.refreshToken);
            }
        })
      );
  }

  private checkLoginStatus() : boolean{
    if(this.IsUserAuthenticated){
      return true;
    }
    return false;
  }

  logout(): void {
    this.resetSecurityObject();
    this.router.navigate(['login']);
  }

  public get AppUserObject(){
    if(!this._appUserObject.isEmpty()) {
      return this._appUserObject;
    }
    
    var storageAppUser = localStorage.getItem('appUser');
    if (storageAppUser) {
      try {
        this._appUserObject = new AppUser;
        Object.assign(this._appUserObject, JSON.parse(storageAppUser));
      } catch (e) {
        localStorage.removeItem('appUser');
      }
    }
    return this._appUserObject;
  }

  public get IsUserAuthenticated(){
    return this.AppUserObject.isAuthenticated;
    //return this.isUserAuthenticated.asObservable();
  }

  public get Username(){
    return this.AppUserObject.userName;
    //return this.userName.asObservable();
  }

  private resetSecurityObject(): void {
    //this._appUserObject = new AppUser();
    this._appUserObject.userName = "";
    this._appUserObject.bearerToken = "";
    this._appUserObject.isAuthenticated = false;

    this._appUserObject.claims = [];

    // localStorage.removeItem("username");
    // localStorage.removeItem("isAuthenticated");
    localStorage.removeItem("appUser");
    localStorage.removeItem("bearerToken");
  }
}