import { DatePipe, Location } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EvidencijaTermina, KarticaZaEvidenciju, Pacijent, Status, TerminTerapije } from '../kartica-za-evidenciju.model';
import { KarticaZaEvidencijuAddEditService } from './kartica-za-evidenciju-add-edit.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'kartica-za-evidenciju-add-edit',
  templateUrl: './kartica-za-evidenciju-add-edit.component.html',
  styleUrls: ['./kartica-za-evidenciju-add-edit.component.scss'],
  providers: [KarticaZaEvidencijuAddEditService]
})
export class KarticaZaEvidencijuAddEditComponent implements OnInit {
  @ViewChild('form', { static: true }) form: NgForm;
  model: KarticaZaEvidenciju = new KarticaZaEvidenciju();
  public validacionaPoruka: string;
  public datumIzdavanjaKartice: string;
  public izabraniDatumTerapije: Date;
  public sifraKarticeZaEvidenciju: number;
  public uslugeLookup: Array<any> = [];
  public statusiTerminaLookup: Array<any> = [];
  public slobodniTermini: Array<TerminTerapije> = [];
  public filtriraniSlobodniTermini: Array<TerminTerapije> = [];
  public odabraniTerminiTerapije: Array<EvidencijaTermina> = [];
  public paginationSlobodniTerminiConfig: any;
  public paginationOdabraniTerminiConfig: any;
  isSifraKarticeDisabled: boolean;

  constructor(private karticaZaEvidencijuService: KarticaZaEvidencijuAddEditService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private datePipe: DatePipe,
    private location: Location) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.sifraKarticeZaEvidenciju = +params['sifraKartice'];
      if(this.sifraKarticeZaEvidenciju) {
        this.karticaZaEvidencijuService.vratiKarticuZaEvidenciju(this.sifraKarticeZaEvidenciju).subscribe(kartica => {
          this.model = kartica;
          this.isSifraKarticeDisabled = true;
          this.datumIzdavanjaKartice = this.model.datumIzdavanja.toLocaleDateString();
          this.mapirajOdabraneTermine(this.model.odabraniTermini);
          this.odabraniTerminiTerapije = this.model.odabraniTermini;
        });
      } else {
        this.model = new KarticaZaEvidenciju();
        this.isSifraKarticeDisabled = false;
      }

      this.karticaZaEvidencijuService.vratiUsluge().subscribe(result => {
        this.uslugeLookup = result;
      });
      this.karticaZaEvidencijuService.vratiStatuseTermina().subscribe(result => {
        this.statusiTerminaLookup =  Object.entries(result).map(([key, value]) => ({key,value}));
      });
    });
    this.setSlobodniTerminiPaginationConfig();
    this.setOdabraniTerminiPaginationConfig();
  }

  mapirajOdabraneTermine(termini : Array<EvidencijaTermina>){
    return termini.map(termin => {
      let datumTerapije = this.datePipe.transform(termin.terminTerapije.vremeDatumTerapije, 'dd-MM-yyyy');
      let vremeTerapije = this.datePipe.transform(termin.terminTerapije.vremeDatumTerapije, 'HH:mm');
      termin.terminTerapije.datumTerapije = datumTerapije === null ? "": datumTerapije;
      termin.terminTerapije.vremeTerapije = vremeTerapije === null ? "": vremeTerapije;
    });
  }

  pageSlobodniTerminiChanged(event: any){
    this.paginationSlobodniTerminiConfig.currentPage = event;
  }

  pageOdabraniTerminiChanged(event: any){
    this.paginationOdabraniTerminiConfig.currentPage = event;
  }

  kreirajNovuKarticu(){
    if(this.model.brojUputa) {
      this.karticaZaEvidencijuService.kreirajSifruNoveKarticu().subscribe(sifraKartice => {
        this.model.sifraKartice = sifraKartice;
        this.model.datumIzdavanja = new Date();
        this.datumIzdavanjaKartice = this.model.datumIzdavanja.toLocaleDateString();

        this.sifraKarticeZaEvidenciju = 0;
        this.location.go(`analiza/analiza-add-edit/${sifraKartice}`);
        this.karticaZaEvidencijuService.vratiUputZaNovuKarticu(this.model.brojUputa).subscribe(uput => {
          this.model.uputZaTerapiju = uput;
        })
      });
    } else {
      this.validacionaPoruka = "Niste uneli broj uputa za kreiranje kartice."
    }
  }

  promeniStatusTermina(evidencijaTermina: EvidencijaTermina, event: any){
    let noviStatus: Status = event.target.value;
    evidencijaTermina.status = noviStatus;
    this.karticaZaEvidencijuService.promeniStatusTermina(evidencijaTermina).subscribe(response => {
      if(!response.success){
        this.processValidacionaPoruka(response.message);
      }
    });
  }

  sacuvaj(){
    if(this.sifraKarticeZaEvidenciju){
      this.izmeniKarticuZaEvidenciju();
    }else{
      this.kreirajKarticuZaEvidenciju();
    }
  }

  kreirajKarticuZaEvidenciju(){
    this.model.odabraniTermini = this.odabraniTerminiTerapije;
    this.karticaZaEvidencijuService.sacuvajKarticuZaEvidenciju(this.model).subscribe(result => {
      if(result.success){
        this.processValidacionaPoruka("");
        this.toastr.success("Uspesno sacuvana kartica za evidenciju termina terapije za " + this.model.uputZaTerapiju.pacijent.imePrezime);
      } else {
        this.processValidacionaPoruka(result.message);
        this.toastr.error("Neuspesno sacuvana analiza");
      }
    });
  }

  izmeniKarticuZaEvidenciju(){
    this.model.odabraniTermini = this.odabraniTerminiTerapije;
    this.karticaZaEvidencijuService.izmeniKarticuZaEvidenciju(this.model).subscribe(result => {
      if(result.success){
        this.processValidacionaPoruka("");
        this.toastr.success("Uspesno sacuvana kartica za evidenciju termina terapije za " + this.model.uputZaTerapiju.pacijent.imePrezime);
      } else {
        this.processValidacionaPoruka(result.message);
        this.toastr.error("Neuspesno sacuvana analiza");
      }
    });
  }

  prikaziSveKartice(){
    this.router.navigate([`/kartica-za-evidenciju`]);
  }

  onDatumTerapijeChanged(event : any){
    let izabraniDatum = new Date(event.target.value);
    if(isNaN(izabraniDatum.getDate())){
      this.filtriraniSlobodniTermini = this.slobodniTermini;
    } else {
      let datumZaPretragu = this.datePipe.transform(izabraniDatum, 'dd-MM-yyyy');
      this.filtriraniSlobodniTermini = this.slobodniTermini.filter((termin) => {
        return termin.datumTerapije.includes(datumZaPretragu !== null ? datumZaPretragu: '');
      });
    }
    this.setSlobodniTerminiPaginationConfig();
  }

  odaberiTerminTerapije(termin: TerminTerapije){
    let evidencijaTermina: EvidencijaTermina = {
      sifraKartice: this.sifraKarticeZaEvidenciju,
      terminTerapije: termin,
      status: Status.Zakazan
    }
    this.karticaZaEvidencijuService.odaberiTerminTerapije(evidencijaTermina).subscribe(result => {
      if(result.success){
        this.processValidacionaPoruka("");
        this.odabraniTerminiTerapije.push(evidencijaTermina);  
        this.setOdabraniTerminiPaginationConfig();

        const index: number = this.filtriraniSlobodniTermini.indexOf(termin);
        this.filtriraniSlobodniTermini.splice(index, 1);
        this.setSlobodniTerminiPaginationConfig();
      } else {
        this.processValidacionaPoruka(result.message);
      }
    });    
  }

  ukloniIzabraniTerminTerapije(odabraniTermin: EvidencijaTermina){
    this.karticaZaEvidencijuService.ukloniOdabraniTerminTerapije(odabraniTermin).subscribe(result => {
      if(result.success){
        this.processValidacionaPoruka("");
        const index: number = this.odabraniTerminiTerapije.indexOf(odabraniTermin);
        this.odabraniTerminiTerapije.splice(index, 1);
        this.setOdabraniTerminiPaginationConfig();
        
        if(!this.filtriraniSlobodniTermini.find(t => t.vremeDatumTerapije == odabraniTermin.terminTerapije.vremeDatumTerapije && t.sifraUsluge == odabraniTermin.terminTerapije.sifraUsluge)){
          this.filtriraniSlobodniTermini.push(odabraniTermin.terminTerapije);
          this.filtriraniSlobodniTermini.sort((x, y) => new Date(x.vremeDatumTerapije).getTime() - new Date(y.vremeDatumTerapije).getTime());
          this.setSlobodniTerminiPaginationConfig();
        }
      } else {
        this.processValidacionaPoruka(result.message);
      }
    });    
  }

  setSlobodniTerminiPaginationConfig(){
    this.paginationSlobodniTerminiConfig = {
      id: "slobodniTerminiPagination",
      itemsPerPage: 6,
      currentPage: 1,
      totalItems: this.filtriraniSlobodniTermini.length
    };
  }

  setOdabraniTerminiPaginationConfig(){
    this.paginationOdabraniTerminiConfig = {
      id: "odabraniTerminiPagination",
      itemsPerPage: 6,
      currentPage: 1,
      totalItems: this.odabraniTerminiTerapije.length
    };
  }

  vratiSlobodneTermine(){
    if(this.model.sifraUsluge && this.izabraniDatumTerapije){
      this.processValidacionaPoruka("");
      var datum = new Date(this.izabraniDatumTerapije);
      this.karticaZaEvidencijuService.vratiSlobodneTermineTerapije(Number(this.model.sifraUsluge), datum).pipe(map(result => {
        if(result.success){
          let listaTermina : Array<TerminTerapije> = result.data;
          
          result.data = listaTermina.map(item => {
            let datumTerapije = this.datePipe.transform(item.vremeDatumTerapije, 'dd-MM-yyyy');
            let vremeTerapije = this.datePipe.transform(item.vremeDatumTerapije, 'HH:mm');
            const termin: TerminTerapije = {
              radnikId: item.radnikId,
              fizioterapeut: item.fizioterapeut,
              vremeDatumTerapije: item.vremeDatumTerapije,
              datumTerapije: datumTerapije === null ? "": datumTerapije ,
              vremeTerapije: vremeTerapije === null ? "": vremeTerapije,
              kapacitet: item.kapacitet,
              sifraUsluge: item.sifraUsluge,
              nazivUsluge: item.nazivUsluge
            }
            return termin;
          });
          return result;
        }else{
          return result;
        }
      })).subscribe(result => {
        if(result.success){
          this.slobodniTermini = result.data;
          this.filtriraniSlobodniTermini = result.data;
          this.setSlobodniTerminiPaginationConfig();
        }else{
          this.processValidacionaPoruka(result.message);
        }
      });
    } else {
      this.processValidacionaPoruka("Morate izabrati uslugu i datum terapije.");
    }
  }

  processValidacionaPoruka(validacionaPoruka: string){
    this.validacionaPoruka = validacionaPoruka;
    if(validacionaPoruka){
      window.scroll(0,0);
    }
  }
}
