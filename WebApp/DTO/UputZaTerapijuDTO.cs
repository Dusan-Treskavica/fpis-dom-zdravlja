using System;

namespace WebApp.DTO
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
