using System;

namespace WebApplication.DTO
{
    public class EvidencijaTerminaDTO
    {
        public long SifraKartice { get; set; }

        public TerminTerapijeDTO TerminTerapije { get; set; }

        public int Status { get; set; }
    }
}
