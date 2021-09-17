using System;

namespace DataAccess.Entities
{
    public class TerminTerapije
    {
        public long RadnikId { get; set; }
        public Fizioterapeut Fizioterapeut { get; set; }
        public DateTime VremeDatumTerapije { get; set; }
        public int Kapacitet { get; set; }
        public long SifraUsluge { get; set; }
        public Usluga Usluga { get; set; }

        public override bool Equals(object obj)
        {
            var terapija = obj as TerminTerapije;
            return terapija != null &&
                   RadnikId == terapija.RadnikId &&
                   VremeDatumTerapije == terapija.VremeDatumTerapije;
        }
    }
}
