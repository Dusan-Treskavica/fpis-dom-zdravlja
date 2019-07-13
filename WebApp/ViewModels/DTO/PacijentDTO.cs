using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Services.DTO
{
    public class PacijentDTO
    {
        public string JmbgPacijenta { get; set; }
        public string ImePrezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Telefon { get; set; }
        public string Pol { get; set; }
    }
}
