import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { KarticaZaEvidenciju } from '../kartica-za-evidenciju.model';
import { KarticaZaEvidencijuDataTableService } from './kartica-za-evidenciju-datatable.service';

@Component({
  selector: 'app-kartica-za-evidenciju-datatable',
  templateUrl: './kartica-za-evidenciju-datatable.component.html',
  styleUrls: ['./kartica-za-evidenciju-datatable.component.scss'],
  providers: [KarticaZaEvidencijuDataTableService]
})
export class KarticaZaEvidencijuDatatableComponent implements OnInit {

  public karticeZaEvidenciju: Array<KarticaZaEvidenciju>;
  public filtriraneKariceZaEvidenciju: Array<KarticaZaEvidenciju>;
  public kariceZaEvidencijuPrethodnaStrana: Array<KarticaZaEvidenciju> = [];
  public tekstPretrage: string;
  public validacionaPoruka: string;
  public paginationConfig: any;

  constructor(private service: KarticaZaEvidencijuDataTableService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.vratiKarticeZaEvidenciju();
    this.setPaginationConfig(0);
    this.tekstPretrage = "";
  }

  pageChanged(event: any){
    this.paginationConfig.currentPage = event;
  }

  vratiKarticeZaEvidenciju(){
    this.service.vratiKarticeZaEvidenciju().subscribe(result => {
      if(result.success){
        this.karticeZaEvidenciju = result.data;
        if(this.tekstPretrage){
          this.pretraziKarticeZaEvidenciju();
        }else{
          this.filtriraneKariceZaEvidenciju = result.data;
          this.setPaginationConfig(this.filtriraneKariceZaEvidenciju.length);
        }
      }else{
        this.processValidacionaPoruka(result.message);
      }
    });
  }

  
  prikaziKarticuZaEvidenciju(karticaZaEvidenciju: KarticaZaEvidenciju){
    this.router.navigate([`/kartica-za-evidenciju/kartica-za-evidenciju-add-edit/${karticaZaEvidenciju.sifraKartice}`]);
  }

  obrisiKarticuZaEvidenciju(karticaZaEvidenciju: KarticaZaEvidenciju){
    if(confirm("Da li ste sigurni da zelite da obrisete karticu za evidenciju za pacijenta \"" + karticaZaEvidenciju.uputZaTerapiju.pacijent.imePrezime + "\" ?")) {
      this.service.obrisiKarticuZaEvidenciju(karticaZaEvidenciju.sifraKartice).subscribe(response => {
        if(response.success){
          this.toastr.success("Uspesno obrisana kartica za evidenciju za pacijenta \"" + karticaZaEvidenciju.uputZaTerapiju.pacijent.imePrezime + "\".");
          this.vratiKarticeZaEvidenciju();
        }else{
          this.processValidacionaPoruka(response.message);
        }
      });
    }
  }

  pretraziKarticeZaEvidenciju(){
    this.filtriraneKariceZaEvidenciju = this.karticeZaEvidenciju.filter((karticaZaEvidenciju) => {
      return karticaZaEvidenciju.uputZaTerapiju.pacijent.imePrezime.toLowerCase().includes(this.tekstPretrage.toLowerCase());
    })
    this.setPaginationConfig(this.filtriraneKariceZaEvidenciju.length);
  }
  
  pretraziNaEnter(event: KeyboardEvent){
    if(event.key === 'Enter'){
      this.pretraziKarticeZaEvidenciju();
    }
  }

  setPaginationConfig(itemsCount: number){
    this.paginationConfig = {
      id: "karticeZaEvidencijuPagination",
      itemsPerPage: 5,
      currentPage: 1,
      totalItems: itemsCount
    };
  }

  processValidacionaPoruka(validacionaPoruka: string){
    this.validacionaPoruka = validacionaPoruka;
    if(validacionaPoruka){
      window.scroll(0,0);
    }
  }
}
