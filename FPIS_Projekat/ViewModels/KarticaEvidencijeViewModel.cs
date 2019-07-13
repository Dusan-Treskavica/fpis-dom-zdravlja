using FPIS_Projekat.DataAccess.Entities;
using FPIS_Projekat.Services.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPIS_Projekat.ViewModels
{
    public class KarticaEvidencijeViewModel
    {
        public string ErrorMessage { get; set; }

        public bool Redirection { get; set; } = false;

        public KarticaZaEvidencijuDTO KarticaZaEvidencijuDTO { get; set; } = new KarticaZaEvidencijuDTO();

        public IEnumerable<UslugaDTO> Usluge { get; set; } = new List<UslugaDTO>();
        
        public IEnumerable<TerminTerapijeDTO> TerminiTerapije { get; set; } = new List<TerminTerapijeDTO>();

        public IEnumerable<KarticaZaEvidencijuDTO> KarticeZaEvidencijuDTO { get; set; } = new List<KarticaZaEvidencijuDTO>();

        public IEnumerable<SelectListItem> StatusiTermina { get; set; } = new List<SelectListItem>();

    }
}
