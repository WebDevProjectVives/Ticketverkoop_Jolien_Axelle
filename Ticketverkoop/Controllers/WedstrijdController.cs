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


namespace Ticketverkoop.Controllers
{
    public class WedstrijdController : Controller
    {
        private WedstrijdService _wedstrijdService;
        private RingService _ringService;
        private VakService _vakService;
        private StadionService _stadionService;
        private ClubService _clubService;
        private readonly IMapper _mapper;


        public WedstrijdController(IMapper mapper)
        {
            _wedstrijdService = new WedstrijdService();
            _stadionService = new StadionService();
            _mapper = mapper;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = _wedstrijdService.GetAllAankopen();
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


            CartVM item = new CartVM
            {
                Wedstrijd_ID = wedstrijd.WedstrijdId,
                Stadion_ID = wedstrijd.StadionId,
                Datum = wedstrijd.Datum,
                Aantal = 1,
                Prijs = wedstrijd.Thuisploeg.Stadion.Basisprijs,
                DateCreated = DateTime.Now,
                Thuisploeg = wedstrijd.Thuisploeg.Naam,
                Uitploeg = wedstrijd.Uitploeg.Naam,
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



            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
