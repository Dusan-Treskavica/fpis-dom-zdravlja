using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.DTO
{
    public class KarticaZaEvidencijuDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Niste uneli(kreirali) sifru kartice !!!")]
        [Range(1, long.MaxValue, ErrorMessage = "Niste uneli(kreirali) sifru kartice !!!")]
        [Display(Name = "Sifra Kartice")]
        public long SifraKartice { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Niste uneli broj uputa !!!")]
        [Display(Name = "Broj Uputa")]
        public string BrojUputa { get; set; }

        public UputZaTerapijuDTO UputZaTerapiju { get; set; }

        [Display(Name = "Datum Izdavanja")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumIzdavanja { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Niste izabrali vrstu usluge !!!")]
        [Range(1, 10, ErrorMessage = "Niste izabrali vrstu usluge !!!")]
        [Display(Name = "Usluga")]
        public long SifraUsluge { get; set; }

        public string NazivUsluge { get; set; }

        public List<EvidencijaTerminaDTO> OdabraniTermini { get; set; } = new List<EvidencijaTerminaDTO>();
    }
}
