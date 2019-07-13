using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FPIS_Projekat.Common.Exceptions;
using FPIS_Projekat.DataAccess.DB;
using FPIS_Projekat.DataAccess.Entities;
using FPIS_Projekat.Extensions;
using FPIS_Projekat.Services;
using FPIS_Projekat.Services.Controller;
using FPIS_Projekat.Services.DTO;
using FPIS_Projekat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace FPIS_Projekat.Controllers
{
    public class KarticaEvidencijeController : Controller
    {
        private IDBBroker _dbbroker;
        private readonly IMapper _mapper;

        public KarticaEvidencijeController(IDBBroker dbbroker, IMapper mapper)
        {
            _dbbroker = dbbroker;
            _mapper = mapper;
        }

        [BindProperty]
        public KarticaEvidencijeViewModel ViewModel { get; set; } = new KarticaEvidencijeViewModel();

        [HttpGet]
        public IActionResult Kreiraj()
        {

            ViewModel.KarticaZaEvidencijuDTO.DatumIzdavanja = DateTime.Now;
            KarticaEvidencijeControllerService.Remove();

            return View("Create", ViewModel);
        }

        [HttpGet]
        public IActionResult Izmeni()
        {
            KarticaEvidencijeControllerService.Remove();

            return View("Update", ViewModel);
        }

        [HttpGet]
        public IActionResult PrikaziSve()
        {
            try
            {
                var listaKarticaZaEvidencijuDTO = _mapper.Map<IEnumerable<KarticaZaEvidencijuDTO>>
                                                    (KarticaEvidencijeControllerService.GetInstance(_dbbroker)
                                                    .PrikaziSve());
                ViewModel.KarticeZaEvidencijuDTO = listaKarticaZaEvidencijuDTO;

                bool? redirection = TempData.Peek("Redirection") as bool?;
                if (redirection != null && redirection == true)
                {
                    ViewModel.ErrorMessage = TempData.Peek("Message") as string;
                    TempData.Remove("Redirection");
                    TempData.Remove("Message");
                }

                return View("ShowAll", ViewModel);

            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { message = ex.Message});
            }
        }

        [HttpGet]
        public IActionResult Prikazi(long sifra)
        {
            try
            {
                KarticaEvidencijeControllerService.Remove();

                var kartica = KarticaEvidencijeControllerService.GetInstance(_dbbroker).PronadjiKarticu(sifra);
                var usluge = KarticaEvidencijeControllerService.GetInstance(_dbbroker).VratiUsluge();

                ViewModel.KarticaZaEvidencijuDTO = _mapper.Map<KarticaZaEvidencijuDTO>(kartica);
                ViewModel.Usluge = _mapper.Map<List<UslugaDTO>>(usluge);
                ViewModel.StatusiTermina = EnumSelecListExtension.ToSelectList<Status>(Status.Zakazan);

                return View("Update", ViewModel);
            }
            catch (DatabaseException ex)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { message = ex.Message });
            }
            catch (Exception ex)
            {
                TempData["Redirection"] = true;
                TempData["Message"] = ex.Message;
                return RedirectToAction("PrikaziSve");
            }
            
        }

        [HttpPost]
        public IActionResult NovaKartica(string brojUputa)
        {
            try
            {
                var uput = KarticaEvidencijeControllerService.GetInstance(_dbbroker).NovaKartica(brojUputa);

                ViewModel.KarticaZaEvidencijuDTO.UputZaTerapiju = _mapper.Map<UputZaTerapijuDTO>(uput);

                return Json(ViewModel);

            }
            catch (DatabaseException ex)
            {
                ViewModel.Redirection = true;
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
        }

        [HttpPost]
        public IActionResult VratiSifruKartice()
        {
            try
            {
                var sifra = KarticaEvidencijeControllerService.GetInstance(_dbbroker).VratiSifruKartice();

                ViewModel = new KarticaEvidencijeViewModel
                {
                    KarticaZaEvidencijuDTO = new KarticaZaEvidencijuDTO
                    {
                        SifraKartice = sifra,
                        DatumIzdavanja = DateTime.Now.Date
                    }
                };

                return Json(ViewModel);
            }
            catch (DatabaseException ex)
            {
                ViewModel.Redirection = true;
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
            catch (Exception ex)
            {
                ViewModel.Redirection = true;
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
        }

        [HttpPost]
        public IActionResult VratiUsluge()
        {
            try
            {
                var usluge = KarticaEvidencijeControllerService.GetInstance(_dbbroker).VratiUsluge();

                ViewModel.Usluge = _mapper.Map<List<UslugaDTO>>(usluge);

                return Json(ViewModel);
            }
            catch (DatabaseException ex)
            {
                ViewModel.Redirection = true;
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
            catch (Exception ex)
            {
                ViewModel.Redirection = true;
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
        }

        [HttpPost]
        public IActionResult PronadjiKarticu(long sifraKartice)
        {
            try
            {
                var kartica = KarticaEvidencijeControllerService.GetInstance(_dbbroker).PronadjiKarticu(sifraKartice);
                var usluge = KarticaEvidencijeControllerService.GetInstance(_dbbroker).VratiUsluge();

                ViewModel.KarticaZaEvidencijuDTO = _mapper.Map<KarticaZaEvidencijuDTO>(kartica);
                ViewModel.Usluge = _mapper.Map<List<UslugaDTO>>(usluge);
                ViewModel.StatusiTermina = EnumSelecListExtension.ToSelectList<Status>(Status.Zakazan);

                return Json(ViewModel);
            }
            catch (DatabaseException ex)
            {
                ViewModel.Redirection = true;
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
            catch (Exception ex)
            {
                //ViewModel.Redirection = true;
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
        }

        [HttpPost]
        public IActionResult IzaberiUslugu(long sifraUsluge)
        {
            try
            {
                KarticaEvidencijeControllerService.GetInstance(_dbbroker).IzaberiUslugu(sifraUsluge);
                ViewModel.ErrorMessage = "";

                return Json(ViewModel);
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
        }

        [HttpPost]
        public IActionResult VratiTermineIFizioterapeute(DateTime datumTermina, long sifraUsluge)
        {
            try
            {
                var terminiTerapije = KarticaEvidencijeControllerService.GetInstance(_dbbroker).VratiTermineIFizioterapeute(datumTermina, sifraUsluge);
                ViewModel = new KarticaEvidencijeViewModel();

                var terminiDTO = _mapper.Map<List<TerminTerapijeDTO>>(terminiTerapije);
                ViewModel.TerminiTerapije = terminiDTO;

                return Json(terminiDTO);
            }
            catch(DatabaseException ex)
            {
                ViewModel.Redirection = true;
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
            catch(Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
        }

        [HttpPost]
        public IActionResult DodajTerminNaKarticu(long radnikId, DateTime datumVremeTermina)
        {
            try
            {
                KarticaEvidencijeControllerService.GetInstance(_dbbroker).DodajTerminNaKarticu(radnikId, datumVremeTermina);
                return Json("");
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
        }

        [HttpPost]
        public IActionResult ObrisiTerminSaKartice(long radnikId, DateTime datumVremeTermina)
        {
            try
            {
                KarticaEvidencijeControllerService.GetInstance(_dbbroker).ObrisiTerminSaKartice(radnikId, datumVremeTermina);
                return Json("");
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
        }

        [HttpPost]
        public IActionResult PromeniStatusTermina(long radnikId, DateTime datumVremeTermina, Status statusTermina)
        {
            try
            {
                KarticaEvidencijeControllerService.GetInstance(_dbbroker).PromeniStatusTermina(radnikId, datumVremeTermina, statusTermina);
                return Json("");
            }
            catch(Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                return Json(ViewModel);
            }
        }

        [HttpGet]
        public IActionResult Obrisi(long sifra)
        {
            try
            {
                KarticaEvidencijeControllerService.GetInstance(_dbbroker).ObrisiKarticu(sifra);

                return RedirectToAction("PrikaziSve");
            }
            catch (DatabaseException ex)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { message = ex.Message });
            }
            catch (Exception ex)
            {
                TempData["Redirection"] = true;
                TempData["Message"] = ex.Message;
                return RedirectToAction("PrikaziSve");
            }
        }

        [HttpPost, ActionName("Kreiraj")]
        public IActionResult KreirajPOST()
        {
            if (!ModelState.IsValid)
            {
                //ViewModel.JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty);
                ViewModel.ErrorMessage = "Nisu ispravno uneti podaci !!!";
                return View("Create", ViewModel);
            }

            try
            {
                KarticaEvidencijeControllerService.GetInstance(_dbbroker).ZapamtiKarticu();

                return RedirectToAction("PrikaziSve");
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                return View(ViewModel);
            }
        }

        [HttpPost, ActionName("Izmeni")]
        public IActionResult IzmeniPOST()
        {
            if (!ModelState.IsValid)
            {
                ViewModel.StatusiTermina = EnumSelecListExtension.ToSelectList<Status>(Status.Zakazan);
                ViewModel.ErrorMessage = "Nisu ispravno uneti podaci !!!";
                return View("Update", ViewModel);
            }

            try
            {
                KarticaEvidencijeControllerService.GetInstance(_dbbroker).IzmeniKarticu();

                return RedirectToAction("PrikaziSve");
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                return View(ViewModel);
            }

        }
    }
}