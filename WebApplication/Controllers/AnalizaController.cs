using AutoMapper;
using BusinessLogic.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication.DTO;
using Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Common.Exceptions;
using Common.Models;
using Common;

namespace WebApplication.Controllers
{
    [Route("api/domzdravlja/v1/analiza")]
    [Authorize]
    public class AnalizaController : Controller
    {
        private readonly IAnalizaService analizaService;
        private readonly IMapper mapper;

        public AnalizaController(IAnalizaService analizaService, IMapper mapper)
        {
            this.analizaService = analizaService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult VratiSveAnalize()
        {
            try
            {
                List<AnalizaDTO> analize = mapper.Map<List<AnalizaDTO>>(this.analizaService.VratiSveAnalize());

                return Ok(new ApiResponse { HttpStatusCode = HTTPResponseCodes.OK, Success = true, Data = analize });
            }
            catch (RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }

        [HttpGet]
        [Route("{sifraAnalize}")]
        public IActionResult VratiAnalizu(long sifraAnalize)
        {
            try
            {
                Analiza analiza = this.analizaService.VratiAnalizu(sifraAnalize);

                return Ok(new ApiResponse { HttpStatusCode = HTTPResponseCodes.OK, Success = true, Data = mapper.Map<AnalizaDTO>(analiza) } );
            }
            catch(RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }

        [HttpPost]
        public IActionResult KreirajAnalizu([FromBody]AnalizaDTO analizaDTO)
        {
            try
            {
                Analiza analiza = this.mapper.Map<Analiza>(analizaDTO);
                this.analizaService.KreirajAnalizu(analiza);
                return Ok(new ApiResponse { Success = true, HttpStatusCode = HTTPResponseCodes.OK });
            }
            catch (RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }

        [HttpPut]
        [Route("{sifraAnalize}")]
        public IActionResult IzmeniAnalizu([FromBody]AnalizaDTO analizaDTO, long sifraAnalize)
        {
            if (analizaDTO.SifraAnalize != sifraAnalize)
            {
                return Conflict(new ApiResponse
                {
                    HttpStatusCode = HTTPResponseCodes.CONFLICT_RESOURCES,
                    Message = "Sifra analize iz body-ja mora biti ista kao sifra analize iz URI-ja",
                    Success = false
                });
            }
            try
            {
                Analiza analiza = this.mapper.Map<Analiza>(analizaDTO);
                this.analizaService.IzmeniAnalizu(analiza);
                return Ok(new ApiResponse { Success = true, HttpStatusCode = HTTPResponseCodes.OK });
            }
            catch (RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }

        [HttpGet]
        [Route("jedinicamere")]
        public IActionResult VratiJediniceMere()
        {
            return Ok(Extensions.EnumToDictionary<JedinicaMere>());
        }

        [HttpGet]
        [Route("sifra")]
        public IActionResult VratiSifruAnalize()
        {
            long sifraAnalize = this.analizaService.VratiSifruAnalize();
            return Ok(new ApiResponse { HttpStatusCode = HTTPResponseCodes.OK, Success = true, Data = sifraAnalize });
        }

        [HttpDelete]
        [Route("{sifraAnalize}")]
        public IActionResult ObrisiAnalizu(long sifraAnalize)
        {
            try
            {
                this.analizaService.ObrisiAnalizu(sifraAnalize);
                return Ok(sifraAnalize);
            }
            catch(RequestProcessingException ex)
            {
                return Json(new ApiResponse { HttpStatusCode = ex.HttpStatusCode, Message = ex.Message, Success = false });
            }
        }
    }
}
