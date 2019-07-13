using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.DataAccess.Entities
{
    public class TerminTerapije
    {
        private long _radnikId;
        private Fizioterapeut _fizioterapeut;
        private DateTime _vremeDatumTerapije;
        private int _kapacitet;
        private long _sifraUsluge;
        private Usluga _usluga;

        public long RadnikId { get => _radnikId; set => _radnikId = value; }
        public Fizioterapeut Fizioterapeut { get => _fizioterapeut; set => _fizioterapeut = value; }
        public DateTime VremeDatumTerapije { get => _vremeDatumTerapije; set => _vremeDatumTerapije = value; }
        public int Kapacitet { get => _kapacitet; set => _kapacitet = value; }
        public long SifraUsluge { get => _sifraUsluge; set => _sifraUsluge = value; }
        public Usluga Usluga { get => _usluga; set => _usluga = value; }

        public override bool Equals(object obj)
        {
            var terapija = obj as TerminTerapije;
            return terapija != null &&
                   _radnikId == terapija._radnikId &&
                   _vremeDatumTerapije == terapija._vremeDatumTerapije;
        }
    }
}
