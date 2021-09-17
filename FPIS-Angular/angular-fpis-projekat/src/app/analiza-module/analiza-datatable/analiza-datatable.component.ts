import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Analiza } from '../analiza.model';
import { AnalizaDataTableService } from './analiza-datatable.service';

@Component({
  selector: 'app-analiza-datatable',
  templateUrl: './analiza-datatable.component.html',
  styleUrls: ['./analiza-datatable.component.scss'],
  providers: [AnalizaDataTableService]
})
export class AnalizaDatatableComponent implements OnInit{
  
  public analize: Array<Analiza>;
  public filtriraneAnalize: Array<Analiza>;
  public analizePrethodnaStrana: Array<Analiza> = [];
  public tekstPretrage: string;
  public paginationConfig: any;
  public validacionaPoruka: string = "";

  constructor(private analizaService: AnalizaDataTableService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.vratiAnalize();
    this.setPaginationConfig(0);
    this.tekstPretrage = "";
  }

  pageChanged(event: any){
    this.paginationConfig.currentPage = event;
  }

  vratiAnalize(){
    this.analizaService.vratiAnalize().subscribe(result => {
      if(result.success){
        this.analize = result.data;
        if(this.tekstPretrage){
          this.pretraziAnalize();
        }else{
          this.filtriraneAnalize = result.data;
          this.setPaginationConfig(this.filtriraneAnalize.length);
        }
      }else{
        this.processValidacionaPoruka(result.message);
      }
    });
  }

  obrisiAnalizu(analiza: Analiza){
    if(confirm("Da li ste sigurni da zelite da obrisete analizu " + analiza.nazivAnalize + " ?")) {
      this.analizaService.obrisiAnalizu(analiza.sifraAnalize).subscribe(result => {
        if(result.success){
          this.toastr.success("Uspesno obrisana analiza " + analiza.nazivAnalize);
          this.vratiAnalize();
        }else{
          this.processValidacionaPoruka(result.message);
        }
      });
    }
  }

  prikaziAnalizu(analiza: Analiza){
    this.router.navigate([`/analiza/analiza-add-edit/${analiza.sifraAnalize}`]);
  }

  pretraziAnalize(){
    this.filtriraneAnalize = this.analize.filter((analiza) => {
      return analiza.nazivAnalize.toLowerCase().includes(this.tekstPretrage.toLowerCase());
    });
    this.setPaginationConfig(this.filtriraneAnalize.length);

  }

  pretraziNaEnter(event: KeyboardEvent){
    if(event.key === 'Enter'){
      this.pretraziAnalize();
    }
  }

  setPaginationConfig(itemsCount: number){
    this.paginationConfig = {
      id: "analizePagination",
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
