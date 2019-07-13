using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FPIS_Projekat.Common.Exceptions;
using FPIS_Projekat.DataAccess.DB;
using FPIS_Projekat.DataAccess.Entities;
using FPIS_Projekat.Extensions;
using FPIS_Projekat.Services.Controller;
using FPIS_Projekat.Services.DTO;
using FPIS_Projekat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPIS_Projekat.Controllers
{
    public class AnalizaController : Controller
    {
        private IDBBroker _dbbroker;
        private readonly IMapper _mapper;

        [BindProperty]
        public AnalizaViewModel ViewModel { get; set; } = new AnalizaViewModel();

        public AnalizaController(IDBBroker dbbroker, IMapper mapper)
        {
            _dbbroker = dbbroker;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Kreiraj()
        {
            ViewModel.JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty);
            AnalizaControllerService.Remove();

            return View("Create", ViewModel);
        }

        [HttpGet]
        public IActionResult Izmeni()
        {
            ViewModel.JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty);
            AnalizaControllerService.Remove();

            return View("Update", ViewModel);
        }

        [HttpGet]
        public IActionResult PrikaziSve()
        {
            try
            {
                var listaAnalizaDTO = _mapper.Map<IEnumerable<AnalizaDTO>>
                                        (AnalizaControllerService.GetInstance(_dbbroker)
                                        .PrikaziSve());

                ViewModel = new AnalizaViewModel()
                {
                    Analize = listaAnalizaDTO
                };

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
                return RedirectToAction("ErrorWithMessage", "Home", new { message = ex.Message });
            }

        }

        [HttpGet]
        public IActionResult Prikazi(long sifra)
        {
            try
            {
                var analiza = AnalizaControllerService.GetInstance(_dbbroker).PronadjiAnalizu(sifra);
                
                ViewModel.AnalizaDTO = _mapper.Map<AnalizaDTO>(_dbbroker.VratiAnalizu(sifra));
                ViewModel.JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty);

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
        public IActionResult VratiSifruAnalize()
        {
            try
            {
                var sifra = AnalizaControllerService.GetInstance(_dbbroker).VratiSifruAnalize();

                ViewModel = new AnalizaViewModel
                {
                    AnalizaDTO = new AnalizaDTO()
                    {
                        SifraAnalize = sifra
                    },
                    JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty)
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
        public IActionResult PronadjiAnalizu(long sifraAnalize)
        {
            try
            {
                var analizaDTO = _mapper.Map<AnalizaDTO>
                                    (AnalizaControllerService.GetInstance(_dbbroker)
                                    .PronadjiAnalizu(sifraAnalize));

                ViewModel.AnalizaDTO = analizaDTO;
                ViewModel.JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty);
                

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
        
        [HttpGet]
        public IActionResult Obrisi(long sifra)
        {
            try
            {
                AnalizaControllerService.GetInstance(_dbbroker).ObrisiAnalizu(sifra);

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
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewModel.JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty);
                    ViewModel.ErrorMessage = "Nisu ispravno uneti podaci !!!";
                    return View("Create", ViewModel);
                }

                var analiza = _mapper.Map<Analiza>(ViewModel.AnalizaDTO);
                AnalizaControllerService.GetInstance(_dbbroker).ZapamtiAnalizu(analiza);
            
                ViewModel.JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty);

                return RedirectToAction("PrikaziSve");
            }
            catch(DatabaseException ex)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { message = ex.Message});
            }
            catch(Exception ex)
            {
                ViewModel.JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty);
                ViewModel.ErrorMessage = ex.Message;
                return View("Create", ViewModel);
            }
        }

        [HttpPost, ActionName("Izmeni")]
        public IActionResult IzmeniPOST()
        {
            try { 
                if (!ModelState.IsValid)
                {
                    ViewModel.JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty);
                    ViewModel.ErrorMessage = "Nisu ispravno uneti podaci !!!";
                    return View("Update", ViewModel);
                }

                var analiza = _mapper.Map<Analiza>(ViewModel.AnalizaDTO);
                AnalizaControllerService.GetInstance(_dbbroker).IzmeniAnalizu(analiza);
                
                return RedirectToAction("PrikaziSve");
            }
             catch (DatabaseException ex)
            {
                return RedirectToAction("ErrorWithMessage", "Home", new { message = ex.Message });
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = ex.Message;
                ViewModel.JedinicaMereLista = EnumSelecListExtension.ToSelectList<JedinicaMere>(JedinicaMere.Empty);
                return View("Update", ViewModel);
            }
        }
    }
}