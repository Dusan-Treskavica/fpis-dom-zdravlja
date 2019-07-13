using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.Entities
{
    public class UputZaTerapiju
    {
        private string _brojUputa;
        private DateTime _datumIzdavanja;
        private DateTime _rokVazenja;
        private int _redniBrojZahteva;
        private long _sifraUstanove;
        private Ustanova _ustanova;
        private long _sifraTerapije;
        private VrstaTerapije _vrstaTerapije;
        private string _jmbgPacijenta;
        private Pacijent _pacijent;

        public string BrojUputa { get => _brojUputa; set => _brojUputa = value; }
        public DateTime DatumIzdavanja { get => _datumIzdavanja; set => _datumIzdavanja = value; }
        public DateTime RokVazenja { get => _rokVazenja; set => _rokVazenja = value; }
        public long SifraUstanove { get => _sifraUstanove; set => _sifraUstanove = value; }
        public Ustanova Ustanova { get => _ustanova; set => _ustanova = value; }
        public int RedniBrojZahteva { get => _redniBrojZahteva; set => _redniBrojZahteva = value; }
        public long SifraTerapije { get => _sifraTerapije; set => _sifraTerapije = value; }
        public VrstaTerapije VrstaTerapije { get => _vrstaTerapije; set => _vrstaTerapije = value; }
        public string JmbgPacijenta { get => _jmbgPacijenta; set => _jmbgPacijenta = value; }
        public Pacijent Pacijent { get => _pacijent; set => _pacijent = value; }
    }
}
