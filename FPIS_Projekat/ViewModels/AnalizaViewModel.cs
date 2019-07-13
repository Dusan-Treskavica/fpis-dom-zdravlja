using FPIS_Projekat.Services.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FPIS_Projekat.ViewModels
{
    public class AnalizaViewModel
    {
        public bool Redirection { get; set; } = false;

        public string ErrorMessage { get; set; } = "";

        public AnalizaDTO AnalizaDTO { get; set; } = new AnalizaDTO();

        public IEnumerable<SelectListItem> JedinicaMereLista { get; set; } = new List<SelectListItem>();

        public IEnumerable<AnalizaDTO> Analize { get; set; } = new List<AnalizaDTO>();
    }
}
