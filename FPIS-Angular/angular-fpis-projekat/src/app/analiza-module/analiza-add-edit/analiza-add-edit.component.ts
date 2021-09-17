import { Location } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Analiza } from '../analiza.model';
import { AnalizaAddEditService } from './analiza-add-edit.service';

@Component({
  selector: 'app-analiza-add-edit',
  templateUrl: './analiza-add-edit.component.html',
  styleUrls: ['./analiza-add-edit.component.scss'],
  providers: [AnalizaAddEditService]
})
export class AnalizaAddEditComponent implements OnInit {
  @ViewChild('form', { static: true }) form: NgForm;
  model: Analiza = new Analiza();
  public sifraAnalize: number;
  public jediniceMereLookup: Array<any>;
  public isSifraAnalizeDisabled: boolean;
  //public isSifraAnalizeDisabled: boolean;
  public validacionaPoruka : string = "";

  constructor(private analizaService: AnalizaAddEditService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private location: Location) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.sifraAnalize = +params['sifraAnalize'];
      if(this.sifraAnalize) {
        this.analizaService.vratiAnalizu(this.sifraAnalize).subscribe(response => {
          if(response.success){
            this.model = response.data;
            this.isSifraAnalizeDisabled = true;
          }else{
            this.model = new Analiza();
            this.processValidacionaPoruka(response.message);
          }
        });
      } else {
        this.model = new Analiza();
        this.isSifraAnalizeDisabled = false;
      }

      this.analizaService.vratiJediniceMere().subscribe(result => {
        this.jediniceMereLookup = Object.entries(result).map(([key, value]) => ({key,value}));
      });
    });
  }

  sacuvaj() {
    if (this.sifraAnalize){
      this.izmeniAnalizu();
    }else{
      this.kreirajAnalizu();
    }
  }

  kreirajAnalizu(){
    this.analizaService.kreirajAnalizu(this.model).subscribe(response => {
      if(response.success){
        this.processValidacionaPoruka("");
        this.toastr.success('Uspesno sacuvana analiza');
        this.router.navigate([`/analiza/analiza-add-edit/${this.model.sifraAnalize}`]);
      }else{
        this.processValidacionaPoruka(response.message);
      }
    });
  }

  izmeniAnalizu(){
    this.analizaService.izmeniAnalizu(this.model).subscribe(response => {
      if(response.success){
        this.processValidacionaPoruka("");
        this.toastr.success('Uspesno sacuvana analiza');
        this.router.navigate([`/analiza/analiza-add-edit/${this.model.sifraAnalize}`]);
      }else{
        this.processValidacionaPoruka(response.message);
      }
    });
  }

  vratiSifruZaNovuAnalizu(){
    this.analizaService.vratiSifruAnalize().subscribe(result => {
      if(result.success){
        this.model = new Analiza();
        this.model.sifraAnalize = result.data;
        this.isSifraAnalizeDisabled = true;
        
        this.sifraAnalize = 0;
        this.location.go(`analiza/analiza-add-edit/${this.model.sifraAnalize}`);

      }else{
        this.processValidacionaPoruka(result.message);
      }
    })
  }

  pronadjiAnalizu(){
    this.router.navigate([`/analiza/analiza-add-edit/${this.model.sifraAnalize}`]);
  }

  prikaziSveAnalize(){
    this.router.navigate([`/analiza`]);
  }

  processValidacionaPoruka(validacionaPoruka: string){
    this.validacionaPoruka = validacionaPoruka;
    if(validacionaPoruka){
      window.scroll(0,0);
    }
  }

}
