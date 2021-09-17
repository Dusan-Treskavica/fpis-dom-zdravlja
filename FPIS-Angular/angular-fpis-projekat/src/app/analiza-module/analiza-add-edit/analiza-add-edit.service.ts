import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import {HttpClient} from '@angular/common/http';
import {Analiza} from '../analiza.model';
import { APIResponse } from "src/app/shared-module/models/APIResponse.model";
import { catchError, map } from "rxjs/operators";

@Injectable()
export class AnalizaAddEditService {
  constructor(private http: HttpClient) {  }

  public vratiJediniceMere(): Observable<Array<any>> {
    return this.http.get<Array<Analiza>>(`api/domzdravlja/v1/analiza/jedinicamere`);
  }

  public vratiSifruAnalize(): Observable<APIResponse> {
    return this.http.get<APIResponse>(`api/domzdravlja/v1/analiza/sifra`);
  }

  public vratiAnalizu(sifraAnalize: number): Observable<APIResponse> {
    return this.http.get<APIResponse>(`api/domzdravlja/v1/analiza/${sifraAnalize}`);
  }

  public kreirajAnalizu(analiza: Analiza): Observable<APIResponse> {
    return this.http.post<APIResponse>(`api/domzdravlja/v1/analiza/`, JSON.stringify(analiza));
  }

  public izmeniAnalizu(analiza: Analiza): Observable<APIResponse> {
    return this.http.put<APIResponse>(`api/domzdravlja/v1/analiza/${analiza.sifraAnalize}`, JSON.stringify(analiza)).pipe(
      map(response => {
        console.log(response);
        return response;
      })
    );
  }

}