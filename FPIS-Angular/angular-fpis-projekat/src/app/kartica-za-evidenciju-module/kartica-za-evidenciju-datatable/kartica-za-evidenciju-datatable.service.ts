import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { APIResponse } from "src/app/shared-module/models/APIResponse.model";
import { KarticaZaEvidenciju } from "../kartica-za-evidenciju.model";

@Injectable()
export class KarticaZaEvidencijuDataTableService {
  constructor(private http: HttpClient) {  }


  public vratiKarticeZaEvidenciju(): Observable<APIResponse> {
    return this.http.get<APIResponse>(`api/domzdravlja/v1/karticazaevidenciju`);
  }

  public obrisiKarticuZaEvidenciju(sifra: number): Observable<APIResponse> {
     return this.http.delete<APIResponse>(`api/domzdravlja/v1/karticazaevidenciju/${sifra}`);
  }

}