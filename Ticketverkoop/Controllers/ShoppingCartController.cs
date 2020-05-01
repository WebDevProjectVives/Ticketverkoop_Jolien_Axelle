using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Extensions;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticketverkoop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private RingService ringService;
        private VakService vakService;
        private WedstrijdService _wedstrijdService;
        private readonly IMapper _mapper; 
        public ShoppingCartController(IMapper mapper)
        {
            _mapper = mapper;
            ringService = new RingService();
            vakService = new VakService();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.lstRingen = new SelectList(ringService.GetAll(), "RingId", "Naam");
            ViewBag.lstVakken = new SelectList(vakService.GetAll(), "VakId", "Naam");
            ViewBag.lstRingFactor = new SelectList(ringService.GetAll(), "RingId", "Factor");
            ViewBag.lstVakFactor = new SelectList(vakService.GetAll(), "VakId", "Factor");



            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            /*var list = _wedstrijdService.Get(id);
            List<WedstrijdVM> listVM = _mapper.Map<List<WedstrijdVM>>(list);*/

            //var ploeg1 = _wedstrijdService.Get(Convert.ToInt32(Wedstrijd_ID));
            //CartVM cartVM = _mapper.Map<CartVM>(ploeg1);

            return View(cartList);
        }

        public IActionResult DeleteTicket(int? wedstrijd_ID)
        {

            if (wedstrijd_ID == null)
            {
                return NotFound();
            }
            ShoppingCartVM cartList
              = HttpContext.Session
              .GetObject<ShoppingCartVM>("ShoppingCart");

            var itemToRemove =
                cartList.Cart.FirstOrDefault(r => r.Wedstrijd_ID == wedstrijd_ID);
            // db.bieren.FirstOrDefault (r => 

            if (itemToRemove != null)
            {
                cartList.Cart.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);

            }

            return View("index", cartList);

        }

        public IActionResult DeleteAbonnement(int? club_ID)
        {

            if (club_ID == null)
            {
                return NotFound();
            }
            ShoppingCartVM cartList
              = HttpContext.Session
              .GetObject<ShoppingCartVM>("ShoppingCart");

            var itemToRemove =
                cartList.AbonnementCart.FirstOrDefault(r => r.Club_ID == club_ID);
            // db.bieren.FirstOrDefault (r => 

            if (itemToRemove != null)
            {
                cartList.AbonnementCart.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);

            }

            return View("index", cartList);

        }

        [Authorize]  // je moet ingelogd zijn om deze action aan te spreken
        [HttpPost]
        public ActionResult Payment(List<CartVM> carts)
        {

            //  opvragen ID ingelogde User
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            
            try
            {
                Order order;
                foreach (CartVM cart in carts)
                {   //create order object
                    order = new Order();
                    order.UserId = userID;
                    order.OrderId = cart.Wedstrijd_ID;
                    order.DateCreated = DateTime.UtcNow;
                    //order.Count = cart.Aantal;
                    //call method to save
                  }


            }
            catch (Exception ex)
            { }
            

            return View();
        }

        [Authorize]
        public IActionResult Historiek()
        {
            var list = _wedstrijdService.GetAll();
            List<WedstrijdVM> listVM = _mapper.Map<List<WedstrijdVM>>(list);
            return View(listVM);
        }
    }
}
