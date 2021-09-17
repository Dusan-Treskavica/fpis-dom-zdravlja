using Common.Exceptions;
using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.Entities.Auth;
using Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.DB
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

        public IList<Usluga> VratiUsluge()
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

        public IList<TerminTerapije> VratiTermineIFizioterapeute(DateTime datumTermina, long sifraUsluge)
        {
            var termini = _dbContext.TerminiTerapije.Where(t => 
            t.VremeDatumTerapije.Date.Equals(datumTermina.Date) &&
                                                                t.SifraUsluge == sifraUsluge &&
                                                                t.Kapacitet > 0)
                                                    .Include("Fizioterapeut")
                                                    .Include("Usluga")
                                                    .AsNoTracking()
                                                    .ToList();
            return termini;
        }

        public TerminTerapije vratiTerminTerapije(long radnikId, DateTime vremeDatumTerapije)
        {
            var terminTerapije = _dbContext.TerminiTerapije
                .Where(t => t.VremeDatumTerapije.Equals(vremeDatumTerapije) && t.RadnikId == radnikId)
                .AsNoTracking()
                .FirstOrDefault();
            return terminTerapije;

        }

        public IList<TerminTerapije> VratiTermineIFizioterapeute(long sifraUsluge)
        {
            var termini = _dbContext.TerminiTerapije.Where(t => t.SifraUsluge == sifraUsluge)
                                                    .Include("Fizioterapeut")
                                                    .Include("Usluga")
                                                    .AsNoTracking()
                                                    .ToList();
            return termini;
        }

        public void ZapamtiKarticu(KarticaZaEvidenciju karticaZaEvidenciju)
        {
            _dbContext.KarticeZaEvidenciju.Add(karticaZaEvidenciju);
            this.SmanjiKapacitetIzabranimTerminima(karticaZaEvidenciju.ListaTermina);

            _dbContext.Database.OpenConnection();
            try
            {
                _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.KarticeZaEvidenciju ON");
                _dbContext.SaveChanges();
                _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.KarticeZaEvidenciju OFF");
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Greska sa bazom podataka. Nije moguce ucitati podatke iz baze !!!");
            }
            finally
            {
                _dbContext.Database.CloseConnection();
            }
        }

        public void IzmeniKarticu_V2(KarticaZaEvidenciju karticaZaEvidenciju, List<EvidencijaTermina> terminiZaDodavanje, List<EvidencijaTermina> terminiZaBrisanje)
        {
            try
            {
                _dbContext.Database.BeginTransaction();

                var entries = _dbContext.ChangeTracker.Entries();

                _dbContext.Entry(karticaZaEvidenciju).State = EntityState.Modified;

                foreach (EvidencijaTermina termin in karticaZaEvidenciju.ListaTermina)
                {
                    switch (termin.DBStatus)
                    {
                        case DBStatus.Insert: 
                            _dbContext.Entry<EvidencijaTermina>(termin).State = EntityState.Added;
                            break;
                        case DBStatus.Update:
                            _dbContext.Entry<EvidencijaTermina>(termin).State = EntityState.Modified;
                            break;
                        case DBStatus.Delete:
                            _dbContext.Entry<EvidencijaTermina>(termin).State = EntityState.Deleted;
                            break;
                        default:
                            _dbContext.Entry<EvidencijaTermina>(termin).State = EntityState.Unchanged;
                            break;
                    }
                }
                
                _dbContext.SaveChanges();

                this.SmanjiKapacitetIzabranimTerminima(terminiZaDodavanje);

                this.SmanjiKapacitetIzabranimTerminima(terminiZaBrisanje);

                _dbContext.SaveChanges();

                _dbContext.Database.CommitTransaction();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _dbContext.Database.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                throw new DatabaseException("Nije moguce izvrsiti izmene nad karticom !!!");
            }
        }
        public void IzmeniKarticu(KarticaZaEvidenciju karticaZaEvidenciju)
        {
            try
            {
                if (_dbContext.KarticeZaEvidenciju.Where(x => x.SifraKartice == karticaZaEvidenciju.SifraKartice).AsNoTracking().FirstOrDefault() == null)
                    throw new DatabaseException("Nije moguce izvrsiti izmene nad karticom !!!");
                var entries = _dbContext.ChangeTracker.Entries();

                //_dbContext.KarticeZaEvidenciju.Update(karticaZaEvidenciju);

                var terminiZaDodavanje = karticaZaEvidenciju.VratiTermineZaDodavanje();
                _dbContext.EvidencijaTermina.AddRange(terminiZaDodavanje);
                this.SmanjiKapacitetIzabranimTerminima(terminiZaDodavanje);

                var terminiZaUpdate = karticaZaEvidenciju.VratiTermineZaUpdate();
                _dbContext.EvidencijaTermina.UpdateRange(terminiZaDodavanje);

                var terminiZaBrisanje = karticaZaEvidenciju.VratiTermineZaBrisanje().Where(t => _dbContext.EvidencijaTermina.Any(
                                                                    tDB => tDB.Sifra == t.Sifra &&
                                                                           tDB.RadnikId == t.RadnikId &&
                                                                           tDB.VremeDatumTerapije == t.VremeDatumTerapije)).ToList();

                var terminiZaDetach = karticaZaEvidenciju.VratiTermineZaBrisanje().Where(t => !terminiZaBrisanje.Contains(t)).ToList();
                terminiZaDetach.ForEach(t => _dbContext.Entry<EvidencijaTermina>(
                    karticaZaEvidenciju.ListaTermina.Single(
                        el => el.Equals(t))).State = EntityState.Detached);

                _dbContext.EvidencijaTermina.RemoveRange(terminiZaBrisanje);
                this.PovecajKapacitetIzabranimTerminima(terminiZaBrisanje);

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

        private void SmanjiKapacitetIzabranimTerminima(IList<EvidencijaTermina> listaTermina)
        {
            List<TerminTerapije> izabraniTermini = new List<TerminTerapije>();

            //Azuriranje kapaciteta izabranih termina
            foreach (EvidencijaTermina terminSaKartice in listaTermina)
            {
                if(terminSaKartice.TerminTerapije != null)
                {
                    terminSaKartice.TerminTerapije.Kapacitet--;
                    izabraniTermini.Add(terminSaKartice.TerminTerapije);
                }
                else
                {
                    var termin = this._dbContext.TerminiTerapije.Where(t =>
                                            t.RadnikId == terminSaKartice.RadnikId && t.VremeDatumTerapije == terminSaKartice.VremeDatumTerapije)
                                    .AsNoTracking()
                                    .FirstOrDefault();
                    termin.Kapacitet--;
                    izabraniTermini.Add(termin);
                }
            }
            _dbContext.TerminiTerapije.UpdateRange(izabraniTermini);
        }

        private void PovecajKapacitetIzabranimTerminima(IList<EvidencijaTermina> listaTermina)
        {
            List<TerminTerapije> izabraniTermini = new List<TerminTerapije>();

            //Azuriranje kapaciteta izabranih termina
            foreach (EvidencijaTermina terminSaKartice in listaTermina)
            {
                if(terminSaKartice.TerminTerapije != null)
                {
                    terminSaKartice.TerminTerapije.Kapacitet++;
                    izabraniTermini.Add(terminSaKartice.TerminTerapije);
                }
                else
                {
                    var termin = this._dbContext.TerminiTerapije.Where(t =>
                                            t.RadnikId == terminSaKartice.RadnikId && t.VremeDatumTerapije == terminSaKartice.VremeDatumTerapije)
                                    .AsNoTracking()
                                    .FirstOrDefault();
                    termin.Kapacitet++;
                    izabraniTermini.Add(termin);
                }
            }
            _dbContext.TerminiTerapije.UpdateRange(izabraniTermini);
        }

        public List<EvidencijaTermina> VratiSveEvidencijeTerminaZaKarticu(long sifraKartice)
        {
            return this._dbContext.EvidencijaTermina.Where(et => et.Sifra == sifraKartice).AsNoTracking().ToList();
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
                _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Analize ON");
                _dbContext.SaveChanges();
                _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Analize OFF");
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
                KarticaZaEvidenciju karticaZaEvidenciju = _dbContext.KarticeZaEvidenciju.Where(kze => kze.SifraKartice == sifraKartice)
                                    .Include(kze => kze.Usluga)
                                    .Include(kze => kze.UputZaTerapiju)
                                        .ThenInclude(u => u.Pacijent)
                                    .Include(kze => kze.ListaTermina)
                                        .ThenInclude(lt => lt.TerminTerapije)
                                            .ThenInclude(t => t.Fizioterapeut)
                                     .Include(kze => kze.ListaTermina)
                                        .ThenInclude(lt => lt.TerminTerapije)
                                            .ThenInclude(t => t.Usluga)
                                    .AsNoTracking()
                                    .FirstOrDefault();

                return karticaZaEvidenciju;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Nije moguce ucitati karticu za evidenciju !!!");
            }
        }

        public KarticaZaEvidenciju VratiKarticuAsTracking(long sifraKartice)
        {
            try
            {
                KarticaZaEvidenciju karticaZaEvidenciju = _dbContext.KarticeZaEvidenciju.Where(kze => kze.SifraKartice == sifraKartice)
                                    .Include(kze => kze.ListaTermina)
                                        .ThenInclude(lt => lt.TerminTerapije)
                                    .FirstOrDefault();

                return karticaZaEvidenciju;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Nije moguce ucitati karticu za evidenciju !!!");
            }
        }

        public async Task DodajNoviRefreshTokenIObrisiStareAsync(RefreshToken refreshToken)
        {
            try
            {
                var stariRefreshTokeni = this._dbContext.RefreshTokens.Where(rt => rt.UserId == refreshToken.UserId);

                if (stariRefreshTokeni != null)
                {
                    this._dbContext.RefreshTokens.RemoveRange(stariRefreshTokeni);
                }

                this._dbContext.RefreshTokens.Add(refreshToken);

                await this._dbContext.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public RefreshToken VratiRefreshToken(string refreshToken)
        {
            try
            {
                return this._dbContext.RefreshTokens.Where(t => t.Value == refreshToken)
                                                        .AsNoTracking()
                                                        .FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task ObrisiStariIDodajNoviToken(RefreshToken stariRefreshToken, RefreshToken noviRefreshToken)
        {
            try
            {
                RefreshToken stariToken = this._dbContext.RefreshTokens.Where(rt => rt.UserId == stariRefreshToken.UserId && rt.Value == stariRefreshToken.Value).FirstOrDefault();
                if (stariToken != null)
                {
                    this._dbContext.RefreshTokens.Remove(stariToken);
                }
                this._dbContext.RefreshTokens.Add(noviRefreshToken);

                await this._dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
