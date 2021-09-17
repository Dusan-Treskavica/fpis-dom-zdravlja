import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import {HttpClient} from '@angular/common/http';
import {Analiza} from '../analiza.model';
import { APIResponse } from "src/app/shared-module/models/APIResponse.model";

@Injectable()
export class AnalizaDataTableService {
  constructor(private http: HttpClient) {  }

  public vratiAnalize(): Observable<APIResponse> {
    return this.http.get<APIResponse>(`api/domzdravlja/v1/analiza`);
  }

  public obrisiAnalizu(sifra: number): Observable<APIResponse> {
     return this.http.delete<APIResponse>(`api/domzdravlja/v1/analiza/${sifra}`);
  }
}