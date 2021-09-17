using AutoMapper;
using BusinessLogic.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication.DTO;
using Common.Extensions;
using Common.Exceptions;
using Common.Models;
using Common;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Controllers
{
    [Route("api/domzdravlja/v1/karticazaevidenciju")]
    [Authorize]
    public class KarticaZaEvidencijuController : Controller
    {
        private readonly IKarticaZaEvidencijuService karticaZaEvidencijuService;
        private readonly IMapper mapper;

        public KarticaZaEvidencijuController(IKarticaZaEvidencijuService karticaZaEvidencijuService, IMapper mapper)
        {
            this.karticaZaEvidencijuService = karticaZaEvidencijuService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VratiSveKarticeZaEvidenciju()
        {
            List<KarticaZaEvidencijuDTO> karticeZaEvidenciju = this.mapper.Map<List<KarticaZaEvidencijuDTO>>(this.karticaZaEvidencijuService.VratiSveKarticeZaEvidenciju());
            return Ok(new ApiResponse { HttpStatusCode = HTTPResponseCodes.OK, Success = true, Data = karticeZaEvidenciju });
        }

        [HttpGet]
        [Route("{sifraKarticeZaEvidenciju}")]
        public ActionResult<KarticaZaEvidencijuDTO> VratiKarticuZaEvidenciju(long sifraKarticeZaEvidenciju)
        {
            KarticaZaEvidenciju karticaZaEvidenciju = this.karticaZaEvidencijuService.VratiKarticuZaEvidenciju(sifraKarticeZaEvidenciju);

            return Ok(mapper.Map<KarticaZaEvidencijuDTO>(karticaZaEvidenciju));
        }

        [HttpPost]
        [Route("sifra")]
        public ActionResult kreirajSifruNoveKarticu()
        {
            return Ok(this.karticaZaEvidencijuService.KreirajSifruNoveKartice());
        }

        [HttpGet]
        [Route("uput/{brojUputa}")]
        public IActionResult VratiPodatkeUputaZaNovuKarticu(string brojUputa)
        {
            UputZaTerapijuDTO uputZaTerapijuDTO = this.mapper.Map<UputZaTerapijuDTO>(this.karticaZaEvidencijuService.VratiUputZaNovuKarticu(brojUputa));

            return Ok(uputZaTerapijuDTO);
        }

        [HttpGet]
        [Route("usluga")]
        public ActionResult<List<UslugaDTO>> VratiUsluge()
        {
            IList<Usluga> usluge = this.karticaZaEvidencijuService.VratiUsluge();

            return Ok(mapper.Map<List<UslugaDTO>>(usluge));
        }

        [HttpGet]
        [Route("statusitermina")]
        public ActionResult<IDictionary<string, string>> VratiStatuseTermina()
        {
            IDictionary<string, string> statusiTermina = Extensions.EnumToDictionary<Status>();

            return Ok(statusiTermina);
        }

        [HttpPost]
        [Route("slobodniTermin")]
        public ActionResult<List<TerminTerapijeDTO>> VratiSlobodneTermineILekare([FromBody] TerminTerapijeDTO terminTerapije)
        {
            try
            {
                List<TerminTerapijeDTO> terminiDTO = this.mapper.Map<List<TerminTerapijeDTO>>(
                    this.karticaZaEvidencijuService.VratiTermineIFizioterapeute(terminTerapije.SifraUsluge, terminTerapije.VremeDatumTerapije));
                return Ok(new ApiResponse { HttpStatusCode = HTTPResponseCodes.OK, Success = true, Data = terminiDTO });
            }
            catch (RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }

        [HttpPost]
        [Route("evidencijatermina/add")]
        public IActionResult IzaberiTerminTerapije([FromBody] EvidencijaTerminaDTO evidencijaTermina)
        {
            try
            {
                this.karticaZaEvidencijuService.DodajTerminTerapijeNaKarticu(this.mapper.Map<EvidencijaTermina>(evidencijaTermina));
                return Ok(new ApiResponse { Success = true });
            }
            catch (RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }

        [HttpPost]
        [Route("evidencijatermina/remove")]
        public IActionResult UkloniTerminTerapije([FromBody] EvidencijaTerminaDTO evidencijaTermina)
        {
            try
            {
                this.karticaZaEvidencijuService.UkloniTerminTerapijeSaKartice(this.mapper.Map<EvidencijaTermina>(evidencijaTermina));
                return Ok(new ApiResponse { Success = true });
            }
            catch (RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }

        [HttpPost]
        [Route("evidencijatermina/status")]
        public IActionResult PromeniStatusTermina([FromBody] EvidencijaTerminaDTO evidencijaTermina)
        {
            try
            {
                this.karticaZaEvidencijuService.PromeniStatusTerminaTerapijeNaKartici(this.mapper.Map<EvidencijaTermina>(evidencijaTermina));
                return Ok(new ApiResponse { Success = true });
            }
            catch (RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }

        [HttpPost]
        public IActionResult SacuvajKarticuZaEvidenciju([FromBody] KarticaZaEvidencijuDTO karticaZaEvidenciju)
        {
            try
            {
                this.karticaZaEvidencijuService.KreirajKarticuZaEvidenciju(this.mapper.Map<KarticaZaEvidenciju>(karticaZaEvidenciju));
                return Ok(new ApiResponse { Success = true });
            }
            catch (RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }


        [HttpPut]
        [Route("{sifraKartice}")]
        public IActionResult IzmeniKarticuZaEvidenciju([FromBody] KarticaZaEvidencijuDTO karticaZaEvidencijuDTO, long sifraKartice)
        {
            if (karticaZaEvidencijuDTO.SifraKartice != sifraKartice)
            {
                return Json(new ApiResponse { HttpStatusCode = HTTPResponseCodes.CONFLICT_RESOURCES,
                    Message = "Sifra kartice iz body-ja mora biti ista kao sifra kartice iz URI-ja", Success = false });
            }

            try
            {
                KarticaZaEvidenciju karticaZaEvidenciju = this.mapper.Map<KarticaZaEvidenciju>(karticaZaEvidencijuDTO);
                this.karticaZaEvidencijuService.IzmeniKarticuZaEvidenciju(karticaZaEvidenciju);

                return Ok(new ApiResponse { Success = true, HttpStatusCode = HTTPResponseCodes.OK });
            }
            catch (RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }

        [HttpDelete]
        [Route("{sifraKartice}")]
        public IActionResult ObrisiKarticuZaEvidenciju(long sifraKartice) 
        {
            try
            {
                this.karticaZaEvidencijuService.ObrisiKarticuZaEvidenciju(sifraKartice);

                return Ok(new ApiResponse { Success = true, HttpStatusCode = HTTPResponseCodes.OK });
            }
            catch (RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }
    }
}
