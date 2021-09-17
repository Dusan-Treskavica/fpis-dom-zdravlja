using System;

namespace WebApplication.DTO
{
    public class TerminTerapijeDTO
    {
        public long RadnikId { get; set; }
        public string Fizioterapeut { get; set; }
        public DateTime VremeDatumTerapije { get; set; }
        public int Kapacitet { get; set; }
        public long SifraUsluge { get; set; }
        public string NazivUsluge { get; set; }
    }
}
