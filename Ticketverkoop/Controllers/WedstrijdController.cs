using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Extensions;
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
        private ClubService _clubService;
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

        public async Task<ActionResult> Select(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Wedstrijd wedstrijd = await _wedstrijdService.Get(Convert.ToInt32(id));
            Club club = await _clubService.Get(wedstrijd.ThuisploegId);
            Club club2 = await _clubService.Get(wedstrijd.UitploegId);


            CartVM item = new CartVM
            {
                Wedstrijd_ID = wedstrijd.WedstrijdId,
                Datum = wedstrijd.Datum,
                Aantal = 1,
                Prijs = 20, // moet prijs zijn uit de database : RingVakSTadion
                DateCreated = DateTime.Now,
                Thuisploeg = club.Naam,
                Uitploeg = club2.Naam
            };

            ShoppingCartVM shopping;

            if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.Cart = new List<CartVM>();
            }
            shopping.Cart.Add(item);


            HttpContext.Session.SetObject("ShoppingCart", shopping);


            //  Session["ShoppingCart"] = shopping;

            
            /*
            var lijstTickets = new List<SelectListItem>();
            for (var i = 1; i < 11; i++)
                lijstTickets.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            ViewBag.lstTickets = lijstTickets;
            

            ViewBag.lstStadion = new SelectList(_wedstrijdService.GetAll(), "Stadion");
            //ViewBag.lstRingen = new SelectList(_ringService.GetAll(), "RingId", "Naam");
            //ViewBag.lstVakken = new SelectList(_vakService.GetAll(), "VakId", "Naam");
            //return View();
            */

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
