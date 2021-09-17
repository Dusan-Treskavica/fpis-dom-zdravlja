using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO
{
    public class AnalizaDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Niste uneli(kreirali) sifru analize !!!")]
        [Range(1, long.MaxValue, ErrorMessage = "Niste uneli(kreirali) sifru analize !!!")]
        [Display(Name = "Sifra Analize")]
        public long SifraAnalize { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Niste uneli naziv analize !!!")]
        [Display(Name = "Naziv Analize")]
        public string NazivAnalize { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Niste uneli donju granicu analize !!!")]
        [Range(0.000001, 999999, ErrorMessage = "Uneta vrednost mora biti pozitivna")]
        [Display(Name = "Donja Granica")]
        public double DonjaGranica { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Niste uneli gornju granicu analize !!!")]
        [Range(0.000001, 999999, ErrorMessage = "Uneta vrednost mora biti pozitivna")]
        [Display(Name = "Gornja Granica")]
        public double GornjaGranica { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Niste izabrali jedinicu mere za donju granicu !!!")]
        [Range(1, 10, ErrorMessage = "Niste izabrali jedinicu mere za donju granicu !!!")]
        [Display(Name = "Jedinica Mere")]
        public int DonjaGranicaJedinicaMere { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Niste izabrali jedinicu mere za gornju granicu !!!")]
        [Range(1, 10, ErrorMessage = "Niste izabrali jedinicu mere za gornju granicu !!!")]
        [Display(Name = "Jedinica Mere")]
        public int GornjaGranicaJedinicaMere { get; set; }
    }
}
