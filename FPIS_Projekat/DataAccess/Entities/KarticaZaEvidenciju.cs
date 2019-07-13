using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.Entities
{
    public class KarticaZaEvidenciju
    {
        private long _sifra;
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        private DateTime _datumIzdavanja;
        private string _brojUputa;
        private UputZaTerapiju _uputZaTerapiju;
        private long _sifraUsluge;
        private Usluga _usluga;
        private IList<EvidencijaTermina> _listaTermina;

        public long SifraKartice { get => _sifra; set => _sifra = value; }
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime DatumIzdavanja { get => _datumIzdavanja; set => _datumIzdavanja = value; }
        public string BrojUputa { get => _brojUputa; set => _brojUputa = value; }
        public UputZaTerapiju UputZaTerapiju { get => _uputZaTerapiju; set => _uputZaTerapiju = value; }
        public long SifraUsluge { get => _sifraUsluge; set => _sifraUsluge = value; }
        public Usluga Usluga { get => _usluga; set => _usluga = value; }
        public IList<EvidencijaTermina> ListaTermina { get => _listaTermina; set => _listaTermina = value; }

        public void DodajTerminNaKarticu(long sifraKartice, long radnikId, DateTime datumVremeTermina)
        {
            EvidencijaTermina noviTermin = new EvidencijaTermina
            {
                Sifra = sifraKartice,
                RadnikId = radnikId,
                VremeDatumTerapije = datumVremeTermina
            };
            noviTermin.Status = Status.Zakazan;
            
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

        public void PromeniStatusTermina(long sifraKartice, long radnikId, DateTime datumVremeTermina, Status statusTermina)
        {
            var trazeniTermin = ListaTermina.Where(t => t.Sifra == sifraKartice && 
                                                        t.RadnikId == radnikId && 
                                                        t.VremeDatumTerapije == datumVremeTermina)
                                                    .FirstOrDefault();
            if (trazeniTermin != null)
            {
                trazeniTermin.Status = statusTermina;
                trazeniTermin.DBStatus = DBStatus.Update;
            }
            else
                throw new Exception("Nije moguce izvrsiti izmenu termina. Termin ne postoji !!!");
        }

        public List<EvidencijaTermina> VratiTermineZaBrisanje()
        {
            var terminiZaBrisanje = new List<EvidencijaTermina>();

            foreach (var termin in ListaTermina)
            {
                if (termin.DBStatus == DBStatus.Delete)
                    terminiZaBrisanje.Add(termin);
            }

            return terminiZaBrisanje;
        }

        public List<EvidencijaTermina> VratiTermineZaDodavanje()
        {
            var terminiZaDodavanje = new List<EvidencijaTermina>();

            foreach (var termin in ListaTermina)
            {
                if (termin.DBStatus == DBStatus.Insert)
                    terminiZaDodavanje.Add(termin);
            }

            return terminiZaDodavanje;
        }
    }
}
