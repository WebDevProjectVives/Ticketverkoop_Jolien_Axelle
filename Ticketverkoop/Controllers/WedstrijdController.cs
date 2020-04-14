using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticketverkoop.Controllers
{
    public class WedstrijdController : Controller
    {
        private WedstrijdService _wedstrijdService;
        private RingService _ringService;
        private VakService _vakService;
        private readonly IMapper _mapper;
        //private ClubService _clubService;
        //private StadionService _stadionService;


        public WedstrijdController(IMapper mapper)
        {
            _wedstrijdService = new WedstrijdService();
            _mapper = mapper;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = _wedstrijdService.GetAll();
            List<WedstrijdVM> listVM = _mapper.Map<List<WedstrijdVM>>(list);
            return View(listVM);
        }

        public ActionResult Koop(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.lstStadion = new SelectList(_wedstrijdService.GetAll(), "Stadion");
            //ViewBag.lstRingen = new SelectList(_ringService.GetAll(), "RingId", "Naam");
            //ViewBag.lstVakken = new SelectList(_vakService.GetAll(), "VakId", "Naam");
            return View();
        }
    }
}
