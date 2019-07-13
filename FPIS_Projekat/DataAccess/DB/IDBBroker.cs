using FPIS_Projekat.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.DB
{
    public interface IDBBroker
    {
        long VratiSifruKartice();
        long VratiSifruAnalize();

        List<Usluga> VratiUsluge();
        Usluga VratiUslugu(long sifraUsluge);
        UputZaTerapiju VratiUput(string brojUputa);
        List<TerminTerapije> VratiTermineIFizioterapeute(DateTime datumTermina, long sifraUsluge);
        void ObrisiKarticuZaEvidencije(long sifraKarticeZaEvidenciju);
        Analiza ZapamtiAnalizu(Analiza analiza);
        List<Analiza> VratiSveAnalize();
        Analiza VratiAnalizu(long sifra);
        Analiza IzmeniAnalizu(Analiza analiza);
        List<KarticaZaEvidenciju> VratiSveKarticeZaEvidenciju();
        KarticaZaEvidenciju VratiKarticu(long sifraKartice);
        void ZapamtiKarticu(KarticaZaEvidenciju karticaZaEvidenciju);
        void IzmeniKarticu(KarticaZaEvidenciju karticaZaEvidenciju);
        void ObrisiAnalizu(long sifra);
    }
}
