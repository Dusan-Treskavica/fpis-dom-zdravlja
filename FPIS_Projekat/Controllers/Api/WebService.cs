using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FPIS_Projekat.Common.Exceptions;
using FPIS_Projekat.DataAccess.DB;
using FPIS_Projekat.Services.Controller;
using FPIS_Projekat.Services.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FPIS_Projekat.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/webservice")]
    public class WebService : Controller
    {
        private IDBBroker _dbbroker;
        private readonly IMapper _mapper;

        public WebService(IDBBroker dbbroker, IMapper mapper)
        {
            _dbbroker = dbbroker;
            _mapper = mapper;
        }


        [HttpGet("sifra")]
        public IActionResult VratiSifruKartice()
        {
            try
            {
                long sifra = _dbbroker.VratiSifruKartice();

                return new ObjectResult(sifra);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{sifra}")]
        public IActionResult VratiKarticuEvidencije(long sifra)
        {
            try
            {
                var kartica = _mapper.Map<KarticaZaEvidencijuDTO>(_dbbroker.VratiKarticu(sifra));

                return new ObjectResult(kartica);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("usluge")]
        public IActionResult VratiUsluge()
        {
            try
            {
                var usluge =_dbbroker.VratiUsluge();

                return new ObjectResult(_mapper.Map<List<UslugaDTO>>(usluge));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}