using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Services.DTO
{
    public class TerminTerapijeDTO
    {
        private long _radnikId;
        private string _fizioterapeut;
        private DateTime _vremeDatumTerapije;
        private int _kapacitet;
        private long _sifraUsluge;
        private string _usluga;

        public long RadnikId { get => _radnikId; set => _radnikId = value; }
        public string Fizioterapeut { get => _fizioterapeut; set => _fizioterapeut = value; }
        public DateTime VremeDatumTerapije { get => _vremeDatumTerapije; set => _vremeDatumTerapije = value; }
        public int Kapacitet { get => _kapacitet; set => _kapacitet = value; }
        public long SifraUsluge { get => _sifraUsluge; set => _sifraUsluge = value; }
        public string Usluga { get => _usluga; set => _usluga = value; }
    }
}
