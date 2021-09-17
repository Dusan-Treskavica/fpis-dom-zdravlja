using DataAccess.Entities;
using DataAccess.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DB
{
    public interface IDBBroker
    {
        long VratiSifruKartice();
        long VratiSifruAnalize();

        IList<Usluga> VratiUsluge();
        Usluga VratiUslugu(long sifraUsluge);
        UputZaTerapiju VratiUput(string brojUputa);
        IList<TerminTerapije> VratiTermineIFizioterapeute(DateTime datumTermina, long sifraUsluge);
        IList<TerminTerapije> VratiTermineIFizioterapeute(long sifraUsluge);
        void ObrisiKarticuZaEvidencije(long sifraKarticeZaEvidenciju);
        Analiza ZapamtiAnalizu(Analiza analiza);
        List<Analiza> VratiSveAnalize();
        Analiza VratiAnalizu(long sifra);
        Analiza IzmeniAnalizu(Analiza analiza);
        List<KarticaZaEvidenciju> VratiSveKarticeZaEvidenciju();
        KarticaZaEvidenciju VratiKarticu(long sifraKartice);
        KarticaZaEvidenciju VratiKarticuAsTracking(long sifraKartice);
        void ZapamtiKarticu(KarticaZaEvidenciju karticaZaEvidenciju);
        void IzmeniKarticu(KarticaZaEvidenciju karticaZaEvidenciju);
        void IzmeniKarticu_V2(KarticaZaEvidenciju karticaZaEvidenciju, List<EvidencijaTermina> terminiZaDodavanje, List<EvidencijaTermina> terminiZaBrisanje);
        void ObrisiAnalizu(long sifra);
        TerminTerapije vratiTerminTerapije(long radnikId, DateTime vremeDatumTerapije);
        List<EvidencijaTermina> VratiSveEvidencijeTerminaZaKarticu(long sifraKartice);
        Task DodajNoviRefreshTokenIObrisiStareAsync(RefreshToken refreshToken);
        RefreshToken VratiRefreshToken(string refreshToken);
        Task ObrisiStariIDodajNoviToken(RefreshToken stariRefreshToken, RefreshToken noviRefreshToken);
    }
}
