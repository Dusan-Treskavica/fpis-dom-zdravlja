using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Services.DTO
{
    public class UputZaTerapijuDTO
    {
        public string BrojUputa { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public DateTime RokVazenja { get; set; }
        public string JmbgPacijenta { get; set; }
        public PacijentDTO Pacijent { get; set; }
    }
}
