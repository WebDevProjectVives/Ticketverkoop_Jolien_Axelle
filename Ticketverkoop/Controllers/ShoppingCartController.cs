using System;
using System.Collections.Generic;
using System.Data;
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
        private OrderService orderService;
        private OrderlijnService orderlijnService;
        private TicketService ticketService;
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
            ViewBag.lstRingen = new SelectList(ringService.GetAll(), "Factor", "Naam");
            ViewBag.lstVakken = new SelectList(vakService.GetAll(), "Factor", "Naam");
           
            //vm.vakken = new selectlist(service.all,)

            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");


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
        public ActionResult Payment(ShoppingCartVM carts)
        {

            //  opvragen ID ingelogde User
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            
            try
            {
                Order order;
                Orderlijn orderlijn;
                Ticket ticket;
                //create order object
                order = new Order();
                order.UserId = userID;
                //order.OrderId = 0;
                order.DateCreated = DateTime.UtcNow;
                //order.Count = cart.Aantal;
                //call method to save
                orderService = new OrderService();
                orderService.Insert(order);

                foreach (CartVM cart in carts.Cart)
                {
                    ticket = new Ticket();
                    ticket.WedstrijdId = cart.Wedstrijd_ID;
                    ticket.ZitplaatsNr = 1;
                    //ticket.StadionRingVakId = 
                    ticket.vak = cart.Vak;
                    ticket.Ring = cart.Ring;
                    ticketService = new TicketService();
                    ticketService.Insert(ticket);
                    //create orderlijn object
                    orderlijn = new Orderlijn();
                    orderlijn.OrderId = order.OrderId;
                    orderlijn.TicketId = ticket.TicketId;
                    //order.Count = cart.Aantal;
                    //call method to save
                    
                    orderlijnService = new OrderlijnService();
                    orderlijnService.Insert(orderlijn);
                }
                /*foreach(AbonnementCartVM abonnementCart in abonnementCarts)
                {
                    order = new Order();

                }*/

                


            }
            catch (DataException ex)
            {
                ModelState.AddModelError("","Opslaan niet gelukt");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bel systeem administrator");
            }
            

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
