using FPIS_Projekat.Common.Exceptions;
using FPIS_Projekat.DataAccess;
using FPIS_Projekat.DataAccess.DB;
using FPIS_Projekat.DataAccess.Entities;
using FPIS_Projekat.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.DB
{
    public class DBBroker : IDBBroker
    {
        private FPISDBContext _dbContext;

        public DBBroker(FPISDBContext dbContext)
        {
            _dbContext = dbContext;
            DatabaseFiller.PopulateDatabase(dbContext);
        }

        public long VratiSifruKartice()
        {
            var poslednjaKartica = _dbContext.KarticeZaEvidenciju.OrderByDescending(kze => kze.SifraKartice).FirstOrDefault();

            return poslednjaKartica == null ? 1 : poslednjaKartica.SifraKartice + 1;
        }

        public long VratiSifruAnalize()
        {
            var poslednjaAnaliza = _dbContext.Analize.OrderByDescending(a => a.SifraAnalize).FirstOrDefault();

            return poslednjaAnaliza == null ? 1 : poslednjaAnaliza.SifraAnalize + 1;
        }

        public List<Usluga> VratiUsluge()
        {
            return _dbContext.Usluge.ToList();
        }

        public Usluga VratiUslugu(long sifraUsluge)
        {
            return _dbContext.Usluge
                                .Where(u => u.Sifra == sifraUsluge)
                                .FirstOrDefault();
        }

        public UputZaTerapiju VratiUput(string brojUputa)
        {
            try
            {
                var uputZaTerapiju = _dbContext.UputiZaTerapiju
                                                .Where(ut => ut.BrojUputa == brojUputa)
                                                    .Include("Pacijent")
                                                    .Include("VrstaTerapije")
                                                    .Include("Ustanova")
                                                    .FirstOrDefault();

                return uputZaTerapiju;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Greska sa bazom podataka. Nije moguce ucitati podatke iz baze !!!");
            }
        }

        public List<TerminTerapije> VratiTermineIFizioterapeute(DateTime datumTermina, long sifraUsluge)
        {
            var termini = _dbContext.TerminiTerapije.Where(t => t.VremeDatumTerapije.ToShortDateString().Equals(datumTermina.ToShortDateString()) &&
                                                                t.SifraUsluge == sifraUsluge)
                                                    .Include("Fizioterapeut")
                                                    .ToList();
            return termini;
        }

        public void ZapamtiKarticu(KarticaZaEvidenciju karticaZaEvidenciju)
        {
            _dbContext.KarticeZaEvidenciju.Add(karticaZaEvidenciju);

            _dbContext.Database.OpenConnection();
            try
            {
                _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.KarticeZaEvidenciju ON");
                _dbContext.SaveChanges();
                _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.KarticeZaEvidenciju OFF");
            }
            finally
            {
                _dbContext.Database.CloseConnection();
            }
        }

        public void IzmeniKarticu(KarticaZaEvidenciju karticaZaEvidenciju)
        {
            try
            {
                var karticaDB = VratiKarticu(karticaZaEvidenciju.SifraKartice);

                if (karticaDB == null)
                    throw new DatabaseException("Nije moguce izvrsiti izmene nad karticom !!!");

                _dbContext.Update<KarticaZaEvidenciju>(karticaZaEvidenciju);

                var terminiZaDodavanje = karticaZaEvidenciju.VratiTermineZaDodavanje();
                _dbContext.EvidencijaTermina.AddRange(terminiZaDodavanje);

                var terminiZaBrisanje = karticaZaEvidenciju.VratiTermineZaBrisanje().Where(t => _dbContext.EvidencijaTermina.Any(
                                                                    tDB => tDB.Sifra == t.Sifra &&
                                                                           tDB.RadnikId == t.RadnikId &&
                                                                           tDB.VremeDatumTerapije == t.VremeDatumTerapije)).ToList();

                var terminiZaDetach = karticaZaEvidenciju.VratiTermineZaBrisanje().Where(t => !terminiZaBrisanje.Contains(t)).ToList();
                terminiZaDetach.ForEach(t => _dbContext.Entry<EvidencijaTermina>(
                    karticaZaEvidenciju.ListaTermina.Single(
                        el => el.Equals(t))).State = EntityState.Detached);

                _dbContext.EvidencijaTermina.RemoveRange(terminiZaBrisanje);
                
                _dbContext.SaveChanges();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Nije moguce izvrsiti izmene nad karticom !!!");
            }
        }
        
        public void ObrisiKarticuZaEvidencije(long sifraKarticeZaEvidenciju)
        {
            try
            {
                var karticaZaEvidenciju = _dbContext.KarticeZaEvidenciju.Find(sifraKarticeZaEvidenciju);
                _dbContext.KarticeZaEvidenciju.Remove(karticaZaEvidenciju);

                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new DatabaseException("Nije moguce obrisati karticu za evidenciju !!!");
            }
        }

        public void ObrisiAnalizu(long sifraAnalize)
        {
            try
            {
                var analiza = _dbContext.Analize.Find(sifraAnalize);
                _dbContext.Analize.Remove(analiza);

                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw new DatabaseException("Nije moguce obrisati analizu !!!");
            }
        }

        public Analiza ZapamtiAnalizu(Analiza analiza)
        {
            _dbContext.Analize.Add(analiza);

            _dbContext.Database.OpenConnection();
            try
            {
                _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Analize ON");
                _dbContext.SaveChanges();
                _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Analize OFF");
            }
            finally
            {
                _dbContext.Database.CloseConnection();
            }


            return _dbContext.Analize.Where(a => a.SifraAnalize == analiza.SifraAnalize).SingleOrDefault();
        }

        public Analiza IzmeniAnalizu(Analiza analiza)
        {
            try
            {
                var analizaDB = VratiAnalizu(analiza.SifraAnalize);
                if (analizaDB == null)
                    throw new DatabaseException("Nije moguce izvrsiti izmene nad karticom !!!");

                _dbContext.Update<Analiza>(analiza);
                _dbContext.SaveChanges();

                return _dbContext.Analize.Where(a => a.SifraAnalize == analiza.SifraAnalize).SingleOrDefault();

            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Nije moguce izvrsiti izmene nad karticom !!!");
            }
        }

        public List<Analiza> VratiSveAnalize()
        {
            return _dbContext.Analize.ToList();
        }

        public Analiza VratiAnalizu(long sifra)
        {
            return _dbContext.Analize.Where(a => a.SifraAnalize == sifra).AsNoTracking().SingleOrDefault();
        }

        public List<KarticaZaEvidenciju> VratiSveKarticeZaEvidenciju()
        {
            return _dbContext.KarticeZaEvidenciju
                                    .Include(kze => kze.Usluga)
                                    .Include(kze => kze.UputZaTerapiju)
                                        .ThenInclude(u => u.Pacijent)
                                    .Include(kze => kze.ListaTermina)
                                    .AsNoTracking()
                                    .ToList();
        }

        public KarticaZaEvidenciju VratiKarticu(long sifraKartice)
        {
            try
            {
                return _dbContext.KarticeZaEvidenciju.Where(kze => kze.SifraKartice == sifraKartice)
                                    .Include(kze => kze.Usluga)
                                    .Include(kze => kze.UputZaTerapiju)
                                        .ThenInclude(u => u.Pacijent)
                                    .Include(kze => kze.ListaTermina)
                                        .ThenInclude(lt => lt.TerminTerapije)
                                            .ThenInclude(t => t.Fizioterapeut)
                                    .AsNoTracking()
                                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Nije moguce ucitati karticu za evidenciju !!!");
            }
        }
    }
}
