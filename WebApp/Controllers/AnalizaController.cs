using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace WebApp.Controllers
{
    [RoutePrefix("api/domzdravlja/v1/analiza")]
    public class AnalizaController : Controller
    {
        public AnalizaController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
