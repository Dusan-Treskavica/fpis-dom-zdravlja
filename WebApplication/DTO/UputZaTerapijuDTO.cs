using System;

namespace WebApplication.DTO
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
