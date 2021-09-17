using System;

namespace WebApp.DTO
{
    public class EvidencijaTerminaDTO
    {
        public long SifraKartice { get; set; }

        public long RadnikId { get; set; }

        public string ImeRadnika { get; set; }

        public DateTime VremeDatumTerapije { get; set; }

        public int Status { get; set; }
    }
}
