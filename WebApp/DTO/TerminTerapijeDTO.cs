using System;

namespace WebApp.DTO
{
    public class TerminTerapijeDTO
    {
        public long RadnikId { get; set; }
        public string Fizioterapeut { get; set; }
        public DateTime VremeDatumTerapije { get; set; }
        public int Kapacitet { get; set; }
        public long SifraUsluge { get; set; }
        public string Usluga { get; set; }
    }
}
