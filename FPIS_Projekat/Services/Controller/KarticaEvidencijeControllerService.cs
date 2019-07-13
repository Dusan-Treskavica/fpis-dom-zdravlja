using FPIS_Projekat.Common.Exceptions;
using FPIS_Projekat.DataAccess.DB;
using FPIS_Projekat.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Services.Controller
{
    public class KarticaEvidencijeControllerService : IControllerService
    {
        private static KarticaEvidencijeControllerService _instance;
        private KarticaZaEvidenciju _karticaZaEvidenciju;
        private IDBBroker _dbbroker;

        private KarticaEvidencijeControllerService(IDBBroker dbbroker)
        {
            _dbbroker = dbbroker;
            _karticaZaEvidenciju = new KarticaZaEvidenciju
            {
                ListaTermina = new List<EvidencijaTermina>()
            };
        }

        public static KarticaEvidencijeControllerService GetInstance(IDBBroker dbbroker)
        {
            if (_instance == null)
                _instance = new KarticaEvidencijeControllerService(dbbroker);

            _instance._dbbroker = dbbroker;

            return _instance;
        }

        public static void Remove()
        {
            _instance = null;
        }

        public void Kreiraj()
        {
        }

        public void Izmeni()
        {
        }

        public List<KarticaZaEvidenciju> PrikaziSve()
        {
            var listaKarticaZaEvidenciju = _dbbroker.VratiSveKarticeZaEvidenciju();

            return listaKarticaZaEvidenciju;
        }

        public UputZaTerapiju NovaKartica(string brojUputa)
        {
            try
            {
                var uput = _dbbroker.VratiUput(brojUputa);

                if (uput != null)
                {
                    if (DateTime.Compare(uput.RokVazenja, DateTime.Now) != -1)
                    {
                        _karticaZaEvidenciju.BrojUputa = uput.BrojUputa;
                        //_karticaZaEvidenciju.UputZaTerapiju = uput;
                    }
                    else
                    {
                        throw new Exception("Uput je istekao !!!");
                    }
                }

                return uput;
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long VratiSifruKartice()
        {
            try
            {
                var sifra = _dbbroker.VratiSifruKartice();

                if(_karticaZaEvidenciju != null)
                {
                    _karticaZaEvidenciju.SifraKartice = sifra;
                    _karticaZaEvidenciju.DatumIzdavanja = DateTime.Now;
                }


                return sifra;
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public KarticaZaEvidenciju PronadjiKarticu(long sifraKartice)
        {
            try
            {
                var kartica = _dbbroker.VratiKarticu(sifraKartice);
                if (kartica == null)
                    throw new Exception("Ne postoji trazena kartica !!!");
                else
                    _karticaZaEvidenciju = kartica;

                return kartica;
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Usluga> VratiUsluge()
        {
            try
            {
                var listaUsluga = _dbbroker.VratiUsluge();

                return listaUsluga;
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void IzaberiUslugu(long sifraUsluge)
        {
            try
            {
                var usluga = _dbbroker.VratiUslugu(sifraUsluge);
                if (usluga != null)
                {
                    _karticaZaEvidenciju.SifraUsluge = usluga.Sifra;
                    //_karticaZaEvidenciju.Usluga = usluga;
                }
                else
                {
                    throw new Exception("Ne postoji izabrana usluga !!!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TerminTerapije> VratiTermineIFizioterapeute(DateTime datumTermina, long sifraUsluge)
        {
            try
            {
                if (DateTime.Compare(datumTermina, DateTime.Now) == -1)
                {
                    throw new Exception("Izabrani datum mora biti u buducnosti !!!");
                }

                var usluga = _dbbroker.VratiUslugu(sifraUsluge);
                if (usluga == null)
                {
                    throw new Exception("Ne postoji izabrana usluga !!!");
                }

                var termini = _dbbroker.VratiTermineIFizioterapeute(datumTermina, sifraUsluge);
                if (termini.Count == 0)
                {
                    throw new Exception("Nema termina za uslugu " + usluga.Naziv + " za datum " + datumTermina.GetDateTimeFormats()[0] + " !!!");
                }

                var dozvoljeniTermini = VratiDozvoljeneTermine(termini);

                return dozvoljeniTermini;
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DodajTerminNaKarticu(long radnikId, DateTime datumVremeTermina)
        {
            try
            {
                //TODO: provera ispravnosti podataka
                _karticaZaEvidenciju.DodajTerminNaKarticu(_karticaZaEvidenciju.SifraKartice, radnikId, datumVremeTermina);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ObrisiTerminSaKartice(long radnikId, DateTime datumVremeTermina)
        {
            try
            {
                //TODO: provera ispravnosti podataka
                _karticaZaEvidenciju.ObrisiTerminSaKartice(_karticaZaEvidenciju.SifraKartice, radnikId, datumVremeTermina);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void PromeniStatusTermina(long radnikId, DateTime datumVremeTermina, Status statusTermina)
        {
            _karticaZaEvidenciju.PromeniStatusTermina(_karticaZaEvidenciju.SifraKartice, radnikId, datumVremeTermina, statusTermina);
        }

        public void ZapamtiKarticu()
        {
            try
            {
                _dbbroker.ZapamtiKarticu(_karticaZaEvidenciju);
            }
            catch (Exception ex)
            {
                throw new Exception("Nije moguce sacuvati novu karticu za evidenciju !!!");
            }
        }

        public void IzmeniKarticu()
        {
            try
            {
                _dbbroker.IzmeniKarticu(_karticaZaEvidenciju);
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Nije moguce sacuvati novu karticu za evidenciju !!!");
            }
        }

        public void ObrisiKarticu(long sifra)
        {
            try
            {
                var kartica = _dbbroker.VratiKarticu(sifra);
                if (kartica == null)
                    throw new Exception("Nije moguce obrisati karticu evidencije. Kartica ne postoji !!!");

                _dbbroker.ObrisiKarticuZaEvidencije(sifra);
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Pomocne Metode
        private List<TerminTerapije> VratiDozvoljeneTermine(List<TerminTerapije> termini)
        {
            var dozvoljeniTermini = new List<TerminTerapije>();

            foreach (var ter in termini)
            {
                bool postoji = false;
                foreach (var odabranTermin in _karticaZaEvidenciju.ListaTermina)
                {
                    if (odabranTermin.DaLiJeIstiTermin(ter))
                    {
                        postoji = true;
                        break;
                    }
                }
                if (!postoji)
                    dozvoljeniTermini.Add(ter);
            }

            return dozvoljeniTermini;
        }


        #endregion
    }
}
