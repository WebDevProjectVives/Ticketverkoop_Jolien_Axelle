using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Ticketverkoop.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/CustomErrorPages/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [Route("/CustomErrorPages/405")]
        public IActionResult MethodNotAllowed()
        {
            return View();
        }


    }
}
