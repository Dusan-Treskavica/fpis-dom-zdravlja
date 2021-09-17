import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import {HttpClient} from '@angular/common/http';
import {EvidencijaTermina, KarticaZaEvidenciju, TerminTerapije, UputZaTerapiju, Usluga} from '../kartica-za-evidenciju.model';
import { APIResponse } from "src/app/shared-module/models/APIResponse.model";
import { map } from 'rxjs/operators';

@Injectable()
export class KarticaZaEvidencijuAddEditService {
  constructor(private http: HttpClient) {  }

  public vratiUsluge(): Observable<Array<Usluga>> {
    return this.http.get<Array<Usluga>>(`api/domzdravlja/v1/karticazaevidenciju/usluga`);
  }

  public kreirajSifruNoveKarticu(): Observable<number> {
    return this.http.post<number>(`api/domzdravlja/v1/karticazaevidenciju/sifra`, JSON.stringify(""));
  }

  public vratiUputZaNovuKarticu(brojUputa: string): Observable<UputZaTerapiju> {
    return this.http.get<UputZaTerapiju>(`api/domzdravlja/v1/karticazaevidenciju/uput/${brojUputa}`);
  }

  public vratiKarticuZaEvidenciju(sifraKarticeZaEvidenciju: number): Observable<KarticaZaEvidenciju> {
    return this.http.get<KarticaZaEvidenciju>(`api/domzdravlja/v1/karticazaevidenciju/${sifraKarticeZaEvidenciju}`). pipe(
      map((kartica: KarticaZaEvidenciju) => {
        kartica.datumIzdavanja = new Date(kartica.datumIzdavanja);
                
        return kartica;
      })
    );
  }

  public odaberiTerminTerapije(evidencijaTermina: EvidencijaTermina): Observable<APIResponse> {
    return this.http.post<APIResponse>(`api/domzdravlja/v1/karticazaevidenciju/evidencijatermina/add`, JSON.stringify(evidencijaTermina));
  }

  public ukloniOdabraniTerminTerapije(evidencijaTermina: EvidencijaTermina): Observable<APIResponse> {
    return this.http.post<APIResponse>(`api/domzdravlja/v1/karticazaevidenciju/evidencijatermina/remove`, JSON.stringify(evidencijaTermina));
  }

  public sacuvajKarticuZaEvidenciju(karticaZaEvidenciju: KarticaZaEvidenciju): Observable<APIResponse> {
    return this.http.post<APIResponse>(`api/domzdravlja/v1/karticazaevidenciju/`, JSON.stringify(karticaZaEvidenciju));
  }

  public izmeniKarticuZaEvidenciju(karticaZaEvidenciju: KarticaZaEvidenciju): Observable<APIResponse> {
    return this.http.put<APIResponse>(`api/domzdravlja/v1/karticazaevidenciju/${karticaZaEvidenciju.sifraKartice}`, JSON.stringify(karticaZaEvidenciju));
  }

  public vratiSlobodneTermineTerapije(sifraUsluge: number, datumTerapije: Date): Observable<APIResponse>{
    return this.http.post<APIResponse>('api/domzdravlja/v1/karticazaevidenciju/slobodniTermin', JSON.stringify({"sifraUsluge": sifraUsluge, "vremeDatumTerapije": datumTerapije}));
  }

  public vratiStatuseTermina(): Observable<Array<any>>{
    return this.http.get<Array<any>>('api/domzdravlja/v1/karticazaevidenciju/statusitermina');
  }
  
  public promeniStatusTermina(evidencijaTermina: EvidencijaTermina): Observable<APIResponse>{
    return this.http.post<APIResponse>('api/domzdravlja/v1/karticazaevidenciju/evidencijatermina/status', JSON.stringify(evidencijaTermina));
  }

}