using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
