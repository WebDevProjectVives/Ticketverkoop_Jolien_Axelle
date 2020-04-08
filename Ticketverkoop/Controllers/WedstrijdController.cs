using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticketverkoop.Controllers
{
    public class WedstrijdController : Controller
    {
        private WedstrijdService _wedstrijdService;


        public WedstrijdController()
        {
            _wedstrijdService = new WedstrijdService();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = _wedstrijdService.GetAll();
            return View();
            //nog listVM
        }
    }
}
