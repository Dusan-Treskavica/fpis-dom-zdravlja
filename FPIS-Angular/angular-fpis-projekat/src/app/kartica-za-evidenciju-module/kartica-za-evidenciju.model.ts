export class KarticaZaEvidenciju {

    public sifraKartice: number;
    public brojUputa: string;
    public uputZaTerapiju: UputZaTerapiju;
    public datumIzdavanja: Date;
    public sifraUsluge: number;
    public nazivUsluge: string;
    public odabraniTermini: Array<EvidencijaTermina>;

    constructor(){
        this.uputZaTerapiju = new UputZaTerapiju();
     }
}

export class UputZaTerapiju {
    public brojUputa: string;
    public datumIzdavanja: Date;
    public rokVazenja: Date;
    public jmbgPacijenta: string;
    public pacijent: Pacijent;

    constructor(){
        this.pacijent = new Pacijent();
    }
}

export class Pacijent {
    public jmbgPacijenta: string;
    public imePrezime: string;
    public datumRodjenja: string;
    public telefon: string;
    public pol: string;
}

export class EvidencijaTermina {

    public sifraKartice: number;
    public terminTerapije: TerminTerapije;
    public status: number;

    constructor(){ }
}

export class Usluga {
    public sifra: string;
    public naziv: string ;
}

export class TerminTerapije {
    public radnikId: number;
    public fizioterapeut: string;
    public vremeDatumTerapije : Date;
    public datumTerapije: string;
    public vremeTerapije: string;
    public kapacitet: number;
    public sifraUsluge: number;
    public nazivUsluge: string;

    constructor(){ }
}

export enum Status
{
    Zakazan = 1,
    Otkazan = 2,
    Izvrsen = 3
}