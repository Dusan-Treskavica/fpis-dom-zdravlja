using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Services.DTO
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
