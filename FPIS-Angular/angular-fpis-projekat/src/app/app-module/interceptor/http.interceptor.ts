import {Injectable} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse} from '@angular/common/http';
import { BehaviorSubject, empty, Observable, of, Subject, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError, filter, finalize, switchMap, take, tap } from 'rxjs/operators';
import { AppAuthService } from 'src/app/shared-module/services/app-auth.service';
import { APIResponse } from 'src/app/shared-module/models/APIResponse.model';

@Injectable()
export class FpisApiHttpInterceptor implements HttpInterceptor {
  
    private isTokenRefreshing: boolean = false;
    tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);

    refreshingAccessToken: boolean;
    accessTokenRefreshed: Subject<any> = new Subject();

    constructor(private appAuthService : AppAuthService){
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        return next.handle(this.prepareHeadersAndUrl(request)).pipe(
            catchError((error: Observable<any>) => {
                //console.log(error);
                if(error instanceof HttpErrorResponse) {
                    let errorStatusCode = (<HttpErrorResponse> error).status;
                    if(errorStatusCode == 401){
                        if(this.isTokenRefreshing){
                            console.log(error);
                            this.appAuthService.logout();
                            window.alert(error.error.data.message);
                            return of({});
                        }
                        // return this.handleH401ttpResponseError(request, next).pipe(
                        //     switchMap((response: any) => {
                        //         return next.handle(this.prepareHeadersAndUrl(request));
                        //     }),
                        //     catchError((err: any) => {
                        //         console.log(err);
                        //         this.appAuthService.logout();
                        //         return of({});
                        //       })
                        // );
                        
                        return this.handleHttpResponseError(request, next);
                    }
                    else if(errorStatusCode >=400 && errorStatusCode < 500){
                        return of(<APIResponse>error.error);
                    }
                    return throwError(this.handleError);
                } else {
                    return throwError(this.handleError);
                }
            })
        ) as Observable<HttpEvent<any>>;
    }

    private prepareHeadersAndUrl(req: HttpRequest<any>){
        const headers: {[key:string]: string} = {
            'Authorization': `Bearer ${this.getToken()}`,
            'Pragma': 'no-cache',
            'Cache-Control': 'no-cache',
            'Expires': 'Sat, 01 Dec 2001 00:00:00 GMT'
        };

        if (req.url.indexOf('file') === -1) {
            headers["Content-Type"] = 'application/json';
        }
        
        const preparedRequestUrl : string = req.url[0] == '/' ? req.url.substring(1) : req.url;
        return req.clone({ 
            withCredentials: true,
            setHeaders: headers,
            url: `${environment.url}/${preparedRequestUrl}` 
        });
    }

    private getToken(): string {
        return localStorage.getItem("bearerToken") || '';
    }

    private handleH401ttpResponseError(request : HttpRequest<any>, next : HttpHandler) : Observable<any>{
        if (this.refreshingAccessToken) {
            return new Observable(observer => {
              this.accessTokenRefreshed.subscribe(() => {
                // this code will run when the access token has been refreshed
                observer.next();
                observer.complete();
              })
            })
          }else{
            this.refreshingAccessToken = true;
            // Any existing value is set to null
            // Reset here so that the following requests wait until the token comes back from the refresh token API call
            this.tokenSubject.next(null);

            /// call the API to refresh the token
            return this.appAuthService.getNewRefreshToken().pipe(
                tap((response: APIResponse) => {
                    this.refreshingAccessToken = false;
                    this.accessTokenRefreshed.next(); 
                })
            );
          }

        // First thing to check if the token is in process of refreshing
        // if(!this.isTokenRefreshing)  // If the Token Refresheing is not true
        // {
        //     this.isTokenRefreshing = true;
        //     // Any existing value is set to null
        //     // Reset here so that the following requests wait until the token comes back from the refresh token API call
        //     this.tokenSubject.next(null);

        //     /// call the API to refresh the token
        //     return this.appAuthService.getNewRefreshToken().pipe(
        //         tap((response: APIResponse) => {
        //             this.isTokenRefreshing = false;
        //             this.tokenSubject.next(response.data.bearerToken); 
        //         })
        //     );
        // }
        // else  {
        //     this.isTokenRefreshing = false;
        //     return this.tokenSubject.pipe(
        //         filter(token => token != null),
        //         take(1),
        //         switchMap(token => {
        //             return next.handle(this.prepareHeadersAndUrl(request));
        //         })
        //     );
        // }

    }

    private handleHttpResponseError(request : HttpRequest<any>, next : HttpHandler) {
        // First thing to check if the token is in process of refreshing
        if(!this.isTokenRefreshing)  // If the Token Refresheing is not true
        {
            this.isTokenRefreshing = true;

            // Any existing value is set to null
            // Reset here so that the following requests wait until the token comes back from the refresh token API call
            this.tokenSubject.next(null);

            /// call the API to refresh the token
            return this.appAuthService.getNewRefreshToken().pipe(
                switchMap((response: APIResponse) => {
                    if(response.success) 
                    {
                        this.tokenSubject.next(response.data.bearerToken); 
                        return next.handle(this.prepareHeadersAndUrl(request));
                    }
                    return <any>this.appAuthService.logout();
                }),
                finalize(() => {
                  this.isTokenRefreshing = false;
                })
            );
        }
        else 
        {
            this.isTokenRefreshing = false;
            return this.tokenSubject.pipe(
                filter(token => token != null),
                take(1),
                switchMap(token => {
                    return next.handle(this.prepareHeadersAndUrl(request));
                })
            );
        }

    }

    // Global error handler method 
    private handleError(errorResponse : HttpErrorResponse) 
    {
        let errorMsg : string;

        if(errorResponse.error instanceof Error) 
        {
             // A client-side or network error occurred. Handle it accordingly.
            errorMsg = "An error occured : " + errorResponse.error.message;
        } else 
        {
            // The backend returned an unsuccessful response code.
        // The response body may contain clues as to what went wrong,
        errorMsg = `Backend returned code ${errorResponse.status}, body was: ${errorResponse.error}`;
        }

         return throwError(errorMsg);
    }
}