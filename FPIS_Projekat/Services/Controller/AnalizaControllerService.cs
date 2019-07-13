using FPIS_Projekat.Common.Exceptions;
using FPIS_Projekat.DataAccess.DB;
using FPIS_Projekat.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Services.Controller
{
    public class AnalizaControllerService : IControllerService
    {
        private static AnalizaControllerService _instance;
        private Analiza _analiza;
        private IDBBroker _dbbroker;

        private AnalizaControllerService(IDBBroker dbbroker)
        {
            _dbbroker = dbbroker;
            _analiza = new Analiza();
        }

        public static AnalizaControllerService GetInstance(IDBBroker dbbroker)
        {
            if (_instance == null)
                _instance = new AnalizaControllerService(dbbroker);

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

        public List<Analiza> PrikaziSve()
        {
            var listaAnaliza = _dbbroker.VratiSveAnalize();

            return listaAnaliza;
        }

        public Analiza PronadjiAnalizu(long sifraAnalize)
        {
            try
            {
                var analiza = _dbbroker.VratiAnalizu(sifraAnalize);
                if (analiza == null)
                    throw new Exception("Ne postoji trazena kartica !!!");
                else
                    _analiza = analiza;

                return analiza;
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

        public long VratiSifruAnalize()
        {
            try
            {
                var sifra = _dbbroker.VratiSifruAnalize();

                if (_analiza != null)
                {
                    _analiza.SifraAnalize = sifra;
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

        public void ZapamtiAnalizu(Analiza analiza)
        {
            try
            {
                if (analiza.DonjaGranica > analiza.GornjaGranica)
                {
                    throw new Exception("Vrednost donje granice mora biti manja od vrednosti gornje granice analize !!!");
                }
                if (analiza.DonjaGranicaJedinicaMere != analiza.GornjaGranicaJedinicaMere)
                {
                    throw new Exception("Jedinice mera nisu iste za gornju i donju granicu analize !!!");
                }

                _analiza.NazivAnalize = analiza.NazivAnalize;
                _analiza.DonjaGranica = analiza.DonjaGranica;
                _analiza.GornjaGranica = analiza.GornjaGranica;
                _analiza.DonjaGranicaJedinicaMere = analiza.DonjaGranicaJedinicaMere;
                _analiza.GornjaGranicaJedinicaMere = analiza.GornjaGranicaJedinicaMere;

                _dbbroker.ZapamtiAnalizu(_analiza);
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void IzmeniAnalizu(Analiza analiza)
        {
            try
            {
                if (analiza.DonjaGranica > analiza.GornjaGranica)
                {
                    throw new Exception("Vrednost donje granice mora biti manja od vrednosti gornje granice analize !!!");
                }
                if (analiza.DonjaGranicaJedinicaMere != analiza.GornjaGranicaJedinicaMere)
                {
                    throw new Exception("Vrednost donje granice mora biti manja od vrednosti gornje granice analize !!!");
                }

                _analiza.NazivAnalize = analiza.NazivAnalize;
                _analiza.DonjaGranica = analiza.DonjaGranica;
                _analiza.GornjaGranica = analiza.GornjaGranica;
                _analiza.DonjaGranicaJedinicaMere = analiza.DonjaGranicaJedinicaMere;
                _analiza.GornjaGranicaJedinicaMere = analiza.GornjaGranicaJedinicaMere;

                _dbbroker.IzmeniAnalizu(_analiza);
            }
            catch (DatabaseException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ObrisiAnalizu(long sifra)
        {
            try
            {
                var analiza = _dbbroker.VratiAnalizu(sifra);
                if (analiza == null)
                    throw new Exception("Nije moguce obrisati analizu. Analiza ne postoji !!!");

                _dbbroker.ObrisiAnalizu(sifra);
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
    }
}
