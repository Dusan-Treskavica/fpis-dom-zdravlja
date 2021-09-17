using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataAccess.Entities
{
    public class KarticaZaEvidenciju
    { 
        public long SifraKartice { get; set; }
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime DatumIzdavanja { get; set; }
        public string BrojUputa { get; set; }
        public UputZaTerapiju UputZaTerapiju { get; set; }
        public long SifraUsluge { get; set; }
        public Usluga Usluga { get; set; }
        public IList<EvidencijaTermina> ListaTermina { get; set; }

        public DBStatus DBStatus { get; set; }

        #region Methods

        public void DodajTerminNaKarticu(long sifraKartice, long radnikId, DateTime datumVremeTermina)
        {
            EvidencijaTermina noviTermin = new EvidencijaTermina
            {
                Sifra = sifraKartice,
                RadnikId = radnikId,
                VremeDatumTerapije = datumVremeTermina,
                Status = Status.Zakazan
            };
            
            if (ListaTermina == null)
                ListaTermina = new List<EvidencijaTermina>();

            bool pronadjen = false;
            foreach(var ter in ListaTermina)
            {
                if (ter.Equals(noviTermin))
                {
                    ter.DBStatus = DBStatus.Update;
                    pronadjen = true;
                    break;
                }
            }

            if (!pronadjen)
            { 
                noviTermin.DBStatus = DBStatus.Insert;
                ListaTermina.Add(noviTermin);
            }

        }

        public void ObrisiTerminSaKartice(long sifraKartice, long radnikId, DateTime datumVremeTermina)
        {
            var termin = ListaTermina.Where(t => t.Sifra == sifraKartice &&
                                                 t.RadnikId == radnikId && 
                                                 t.VremeDatumTerapije == datumVremeTermina)
                                            .FirstOrDefault();

            termin.DBStatus = DBStatus.Delete;
        }

        public void PromeniStatusTermina(EvidencijaTermina evidencijaTermina)
        {
            var trazeniTermin = ListaTermina.Where(t => t.Sifra == evidencijaTermina.Sifra && 
                                                        t.RadnikId == evidencijaTermina.RadnikId && 
                                                        t.VremeDatumTerapije == evidencijaTermina.VremeDatumTerapije)
                                                    .FirstOrDefault();
            if (trazeniTermin != null)
            {
                trazeniTermin.Status = evidencijaTermina.Status;
                trazeniTermin.DBStatus = this.SifraKartice != 0 ? DBStatus.Update: DBStatus.Insert;
            }
            else
                throw new RequestProcessingException("Nije moguce izvrsiti izmenu termina. Termin ne postoji !!!");
        }

        public List<EvidencijaTermina> VratiTermineZaBrisanje()
        {
            var terminiZaBrisanje = new List<EvidencijaTermina>();

            foreach (var termin in ListaTermina)
            {
                if (termin.DBStatus == DBStatus.Delete || termin.Status == Status.Otkazan)
                    terminiZaBrisanje.Add(termin);
            }

            return terminiZaBrisanje;
        }

        public List<EvidencijaTermina> VratiTermineZaDodavanje()
        {
            var terminiZaDodavanje = new List<EvidencijaTermina>();

            foreach (var termin in ListaTermina)
            {
                if ((termin.DBStatus == DBStatus.Insert || termin.DBStatus == DBStatus.Update) && termin.Status == Status.Zakazan)
                {
                    terminiZaDodavanje.Add(termin);
                }
            }

            return terminiZaDodavanje;
        }

        public List<EvidencijaTermina> VratiTermineZaUpdate()
        {
            var terminiZaUpdate = new List<EvidencijaTermina>();

            foreach (var termin in ListaTermina)
            {
                if (termin.DBStatus == DBStatus.Update && termin.Status == Status.Izvrsen)
                    terminiZaUpdate.Add(termin);
            }

            return terminiZaUpdate;
        }

        public void SrediTermineZaAzuriranjeKartice()
        {
            List<EvidencijaTermina> sredjeniTermini = new List<EvidencijaTermina>();
            
            List<EvidencijaTermina> terminiZaInsert = this.VratiTermineZaDodavanje();
            List<EvidencijaTermina> terminiZaUpdate = this.VratiTermineZaUpdate();
            List<EvidencijaTermina> terminiZaDelete = this.VratiTermineZaBrisanje();

            foreach (EvidencijaTermina termin in this.ListaTermina)
            {
                if (terminiZaInsert.Contains(termin))
                {
                    sredjeniTermini.Add(termin);
                }
                else if (terminiZaUpdate.Contains(termin))
                {
                    sredjeniTermini.Add(termin);
                }
                else if (terminiZaDelete.Contains(termin))
                {
                    continue;
                }
                else
                {
                    sredjeniTermini.Add(termin);
                }
            }
            this.ListaTermina = sredjeniTermini;
        }

        #endregion
    }
}
