using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public interface IKarticaZaEvidencijuService
    {
        IList<KarticaZaEvidenciju> VratiSveKarticeZaEvidenciju();
        long KreirajSifruNoveKartice();
        KarticaZaEvidenciju VratiKarticuZaEvidenciju(long sifraKarticeZaEvidenciju);
        UputZaTerapiju VratiUputZaNovuKarticu(string brojUputa);
        void KreirajKarticuZaEvidenciju(KarticaZaEvidenciju karticaZaEvidenciju);
        void IzmeniKarticuZaEvidenciju(KarticaZaEvidenciju karticaZaEvidenciju);
        void ObrisiKarticuZaEvidenciju(long sifraKarticeZaEvidenciju);
        void DodajTerminTerapijeNaKarticu(EvidencijaTermina terminTerapije);
        void UkloniTerminTerapijeSaKartice(EvidencijaTermina terminTerapije);
        void PromeniStatusTerminaTerapijeNaKartici(EvidencijaTermina terminTerapije);

        IList<Usluga> VratiUsluge();
        IList<TerminTerapije> VratiTermineIFizioterapeute(long sifraUsluge, DateTime datumTerapije);
    }
}
