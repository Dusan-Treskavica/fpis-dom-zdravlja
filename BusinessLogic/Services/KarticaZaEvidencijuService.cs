using Common;
using Common.Exceptions;
using Common.Util;
using DataAccess.DB;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class KarticaZaEvidencijuService : IKarticaZaEvidencijuService
    {
        private readonly IDBBroker dbBroker;
        private readonly IHttpContextUtil httpContextUtil;

        public KarticaZaEvidencijuService(IDBBroker dbBroker, IHttpContextUtil httpContextUtil)
        {
            this.dbBroker = dbBroker;
            this.httpContextUtil = httpContextUtil;
        }

        public IList<KarticaZaEvidenciju> VratiSveKarticeZaEvidenciju()
        {
            IList<KarticaZaEvidenciju> sveKartice = this.dbBroker.VratiSveKarticeZaEvidenciju();
            return sveKartice;
        }

        public UputZaTerapiju VratiUputZaNovuKarticu(string brojUputa)
        {
            try
            {
                UputZaTerapiju uput = this.dbBroker.VratiUput(brojUputa);

                if (uput == null)
                {
                    throw new Exception("Prosledjeni broj uputa ne postoji.");
                }
                if (DateTime.Compare(uput.RokVazenja, DateTime.Now) == -1)
                {
                    throw new Exception("Uput je istekao !!!");
                }

                KarticaZaEvidenciju karticaZaEvidenciju = this.httpContextUtil.GetFromSession<KarticaZaEvidenciju>(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT);
                
                karticaZaEvidenciju.BrojUputa = uput.BrojUputa;
                karticaZaEvidenciju.UputZaTerapiju = uput;
                this.httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT, karticaZaEvidenciju);
                
                return uput;                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ObrisiKarticuZaEvidenciju(long sifraKarticeZaEvidenciju)
        {
            if(this.dbBroker.VratiKarticuAsTracking(sifraKarticeZaEvidenciju) == null)
            {
                throw new RequestProcessingException($"Nije moguce obrisati karticu. Izabrana kartica ne postoji.", HTTPResponseCodes.BAD_REQUEST);
            }
            this.dbBroker.ObrisiKarticuZaEvidencije(sifraKarticeZaEvidenciju);
        }

        public void DodajTerminTerapijeNaKarticu(EvidencijaTermina evidencijaTermina)
        {
            KarticaZaEvidenciju karticaZaEvidenciju = this.httpContextUtil.GetFromSession<KarticaZaEvidenciju>(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT);
            if(karticaZaEvidenciju.ListaTermina.Any(x => x.VremeDatumTerapije.ToShortDateString().Equals(evidencijaTermina.VremeDatumTerapije.ToShortDateString())))
            {
                throw new RequestProcessingException($"Vec postoji odabran termin za datum: {evidencijaTermina.VremeDatumTerapije.ToShortDateString()}", HTTPResponseCodes.BAD_REQUEST );
            }
            if(evidencijaTermina.Status != Status.Zakazan)
            {
                throw new RequestProcessingException($"Nije moguce dodati termin ciji status nije \"Zakazan\". ", HTTPResponseCodes.BAD_REQUEST);
            }
            karticaZaEvidenciju.DodajTerminNaKarticu(karticaZaEvidenciju.SifraKartice, evidencijaTermina.RadnikId, evidencijaTermina.VremeDatumTerapije);

            this.httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT, karticaZaEvidenciju);
        }

        public void UkloniTerminTerapijeSaKartice(EvidencijaTermina evidencijaTermina)
        {
            KarticaZaEvidenciju karticaZaEvidenciju = this.httpContextUtil.GetFromSession<KarticaZaEvidenciju>(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT);
            if (!karticaZaEvidenciju.ListaTermina.Any())
            {
                throw new RequestProcessingException($"Na kartici ne postoji ni jedan termin. ", HTTPResponseCodes.BAD_REQUEST);
            }
            if (karticaZaEvidenciju.ListaTermina.Count > 0 && !karticaZaEvidenciju.ListaTermina.Any(x => x.VremeDatumTerapije.Equals(evidencijaTermina.VremeDatumTerapije)))
            {
                throw new RequestProcessingException($"Ne postoji ni jedan termin za izabrani datum i vreme: {evidencijaTermina.VremeDatumTerapije}  da se ukloni sa kartice.", HTTPResponseCodes.BAD_REQUEST );
            }
            evidencijaTermina.Sifra = karticaZaEvidenciju.SifraKartice;
            karticaZaEvidenciju.ObrisiTerminSaKartice(evidencijaTermina.Sifra, evidencijaTermina.RadnikId, evidencijaTermina.VremeDatumTerapije);
            //karticaZaEvidenciju.ListaTermina.Remove(evidencijaTermina);

            this.httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT, karticaZaEvidenciju);
        }

        public void PromeniStatusTerminaTerapijeNaKartici(EvidencijaTermina evidencijaTermina)
        {
            //TODO
            KarticaZaEvidenciju karticaZaEvidenciju = this.httpContextUtil.GetFromSession<KarticaZaEvidenciju>(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT);
            if (!karticaZaEvidenciju.ListaTermina.Any())
            {
                throw new RequestProcessingException($"Na kartici ne postoji ni jedan termin. ", HTTPResponseCodes.BAD_REQUEST);
            }
            if (karticaZaEvidenciju.ListaTermina.Count > 0 && !karticaZaEvidenciju.ListaTermina.Any(x => x.VremeDatumTerapije.Equals(evidencijaTermina.VremeDatumTerapije)))
            {
                throw new RequestProcessingException($"Ne postoji ni jedan termin za izabrani datum i vreme: {evidencijaTermina.VremeDatumTerapije}  da se ukloni sa kartice.", HTTPResponseCodes.BAD_REQUEST);
            }

            evidencijaTermina.Sifra = karticaZaEvidenciju.SifraKartice;
            karticaZaEvidenciju.PromeniStatusTermina(evidencijaTermina);

            this.httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT, karticaZaEvidenciju);
        }

        public void KreirajKarticuZaEvidenciju(KarticaZaEvidenciju karticaZaEvidenciju)
        {
            
            KarticaZaEvidenciju karticaZaEvidencijuSession = this.httpContextUtil.GetFromSession<KarticaZaEvidenciju>(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT);

            foreach (EvidencijaTermina odabraniTermin in karticaZaEvidencijuSession.ListaTermina)
            {
                TerminTerapije termin = this.dbBroker.vratiTerminTerapije(odabraniTermin.RadnikId, odabraniTermin.VremeDatumTerapije);
                if (termin.SifraUsluge != karticaZaEvidencijuSession.SifraUsluge)
                {
                    throw new RequestProcessingException($"Svi izabrani termini moraju biti pripadati istoj usluzi. ", HTTPResponseCodes.BAD_REQUEST);
                }
            }

            KarticaZaEvidenciju karticaZaEvidencijuForInsert = new KarticaZaEvidenciju
            {
                SifraKartice = karticaZaEvidencijuSession.SifraKartice,
                DatumIzdavanja = karticaZaEvidencijuSession.DatumIzdavanja,
                BrojUputa = karticaZaEvidencijuSession.BrojUputa,
                SifraUsluge = karticaZaEvidencijuSession.SifraUsluge,
                ListaTermina = karticaZaEvidencijuSession.ListaTermina
            };
                
            List<EvidencijaTermina> terminiZaInsert = karticaZaEvidencijuForInsert.VratiTermineZaDodavanje();

            karticaZaEvidencijuForInsert.ListaTermina = terminiZaInsert;
            this.dbBroker.ZapamtiKarticu(karticaZaEvidencijuForInsert);
        }

        public void IzmeniKarticuZaEvidenciju(KarticaZaEvidenciju karticaZaEvidenciju)
        {
            try
            {
                KarticaZaEvidenciju karticaZaEvidencijuSession = this.httpContextUtil.GetFromSession<KarticaZaEvidenciju>(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT);

                foreach (EvidencijaTermina odabraniTermin in karticaZaEvidencijuSession.ListaTermina)
                {
                    TerminTerapije termin = this.dbBroker.vratiTerminTerapije(odabraniTermin.RadnikId, odabraniTermin.VremeDatumTerapije);
                    if (termin.SifraUsluge != karticaZaEvidencijuSession.SifraUsluge)
                    {
                        throw new RequestProcessingException($"Svi izabrani termini moraju biti pripadati istoj usluzi. ", HTTPResponseCodes.BAD_REQUEST);
                    }
                }

                KarticaZaEvidenciju karticaZaEvidencijuDB = this.dbBroker.VratiKarticuAsTracking(karticaZaEvidencijuSession.SifraKartice);
                karticaZaEvidencijuDB.SifraKartice = karticaZaEvidencijuSession.SifraKartice;
                karticaZaEvidencijuDB.DatumIzdavanja = karticaZaEvidencijuSession.DatumIzdavanja;
                karticaZaEvidencijuDB.BrojUputa = karticaZaEvidencijuSession.BrojUputa;
                karticaZaEvidencijuDB.SifraUsluge = karticaZaEvidencijuSession.SifraUsluge;

                List<EvidencijaTermina> evidencijeTerminaZaBrisanje = new List<EvidencijaTermina>();
                List<EvidencijaTermina> evidencijeTerminaZaDodavanje = new List<EvidencijaTermina>();
                this.SrediTermineZaAzuriranjeKartice(karticaZaEvidencijuSession, karticaZaEvidencijuDB, ref evidencijeTerminaZaDodavanje, ref evidencijeTerminaZaBrisanje);

                KarticaZaEvidenciju karticaZaEvidencijuForInsert = new KarticaZaEvidenciju
                {
                    SifraKartice = karticaZaEvidencijuSession.SifraKartice,
                    DatumIzdavanja = karticaZaEvidencijuSession.DatumIzdavanja,
                    BrojUputa = karticaZaEvidencijuSession.BrojUputa,
                    SifraUsluge = karticaZaEvidencijuSession.SifraUsluge,
                    ListaTermina = karticaZaEvidencijuSession.ListaTermina.Select(x =>
                    {
                        if(x.TerminTerapije != null)
                        {
                            x.TerminTerapije.Fizioterapeut = null;
                            x.TerminTerapije.Usluga = null;
                            x.KarticaZaEvidenciju = null;
                        }
                        return x;
                    }).ToList()
                };
                evidencijeTerminaZaBrisanje = evidencijeTerminaZaBrisanje.Select(x =>
                {
                    if (x.TerminTerapije != null)
                    {
                        x.TerminTerapije.Fizioterapeut = null;
                        x.TerminTerapije.Usluga = null;
                        x.KarticaZaEvidenciju = null;
                    }
                    return x;
                }).ToList();

                this.dbBroker.IzmeniKarticu_V2(karticaZaEvidencijuDB, evidencijeTerminaZaDodavanje, evidencijeTerminaZaBrisanje);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void SrediTermineZaAzuriranjeKartice(KarticaZaEvidenciju karticaZaEvidenciju, KarticaZaEvidenciju karticaZaEvidencijuDB, ref List<EvidencijaTermina> evidencijeTerminaZaDodavanje, ref List<EvidencijaTermina> evidencijeTerminaZaBrisanje)
        {
            List<EvidencijaTermina> sredjeniTermini = new List<EvidencijaTermina>();

            List<EvidencijaTermina> sviPrethodniTerminiKartice = (List<EvidencijaTermina>)karticaZaEvidencijuDB.ListaTermina;

            List<EvidencijaTermina> terminiZaInsert = karticaZaEvidenciju.VratiTermineZaDodavanje();
            List<EvidencijaTermina> terminiZaUpdate = karticaZaEvidenciju.VratiTermineZaUpdate();
            List<EvidencijaTermina> terminiZaDelete = karticaZaEvidenciju.VratiTermineZaBrisanje()
                                                .Where(t => sviPrethodniTerminiKartice
                                                .Any(tDB => tDB.RadnikId == t.RadnikId && tDB.VremeDatumTerapije == t.VremeDatumTerapije))
                                                .ToList();

            terminiZaInsert.ForEach(t =>
            {
                sviPrethodniTerminiKartice.Add(new EvidencijaTermina { Sifra = t.Sifra, RadnikId = t.RadnikId, VremeDatumTerapije = t.VremeDatumTerapije, DBStatus = DBStatus.Insert });
            });
            terminiZaUpdate.ForEach(t =>
            {
                EvidencijaTermina et = sviPrethodniTerminiKartice.Find(x => x.RadnikId == t.RadnikId && x.VremeDatumTerapije == t.VremeDatumTerapije);
                et.DBStatus = DBStatus.Update;
                et.Status = t.Status;
            });
            terminiZaDelete.ForEach(t =>
            {
                sviPrethodniTerminiKartice.Find(x => x.RadnikId == t.RadnikId && x.VremeDatumTerapije == t.VremeDatumTerapije).DBStatus = DBStatus.Delete;
                //sviPrethodniTerminiKartice.Add(new EvidencijaTermina { Sifra = t.Sifra, RadnikId = t.RadnikId, VremeDatumTerapije = t.VremeDatumTerapije, DBStatus = DBStatus.Delete });
                //sviPrethodniTerminiKartice.RemoveAll(x => x.RadnikId == t.RadnikId && x.VremeDatumTerapije == t.VremeDatumTerapije, DBStatus = DBStatus.Update);
            });

            //karticaZaEvidencijuDB.ListaTermina = sviPrethodniTerminiKartice;
            evidencijeTerminaZaDodavanje = terminiZaInsert;
            evidencijeTerminaZaBrisanje = terminiZaDelete;
            //return sredjeniTermini;
        }

        public KarticaZaEvidenciju VratiKarticuZaEvidenciju(long sifraKarticeZaEvidenciju)
        {
            KarticaZaEvidenciju karticaZaEvidenciju = this.dbBroker.VratiKarticu(sifraKarticeZaEvidenciju);
            if(karticaZaEvidenciju == null)
            {
                throw new RequestProcessingException($"Ne postoji trazena kartica za evidenciju.", HTTPResponseCodes.BAD_REQUEST);
            }
            karticaZaEvidenciju.DBStatus = DBStatus.Update;
            this.httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT, karticaZaEvidenciju);
            
            return karticaZaEvidenciju;
        }

        public long KreirajSifruNoveKartice()
        {
            try
            {
                long sifraKartice = this.dbBroker.VratiSifruKartice();

                KarticaZaEvidenciju novaKartica = new KarticaZaEvidenciju { SifraKartice = sifraKartice, DatumIzdavanja = DateTime.Now, ListaTermina = new List<EvidencijaTermina>(), DBStatus = DBStatus.Insert };
                this.httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT, novaKartica);

                return sifraKartice;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<Usluga> VratiUsluge()
        {
            return this.dbBroker.VratiUsluge();
        }

        public IList<TerminTerapije> VratiTermineIFizioterapeute(long sifraUsluge, DateTime datumTerapije)
        {
            KarticaZaEvidenciju karticaZaEvidenciju = this.httpContextUtil.GetFromSession<KarticaZaEvidenciju>(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT);
            
            if(karticaZaEvidenciju.DBStatus == DBStatus.Insert)
            {
                karticaZaEvidenciju.SifraUsluge = sifraUsluge;
            }
            else
            {
                if(karticaZaEvidenciju.SifraUsluge != sifraUsluge)
                {
                    Usluga usluga = this.dbBroker.VratiUslugu(karticaZaEvidenciju.SifraUsluge);
                    throw new RequestProcessingException($"Nije dozvoljeno menjati vrstu usluge za karticu. Mozete birati termine terapije samo za uslugu: {usluga.Naziv}. ", HTTPResponseCodes.BAD_REQUEST);
                }
                else
                {
                    karticaZaEvidenciju.SifraUsluge = sifraUsluge;
                }
            }

            this.httpContextUtil.SetToSession(ApplicationConsts.HTTP_CONTEXT_KARTICAZAEVIDENCIJU_OBJECT, karticaZaEvidenciju);
            return this.dbBroker.VratiTermineIFizioterapeute(datumTerapije, sifraUsluge);
        }
    }
}
