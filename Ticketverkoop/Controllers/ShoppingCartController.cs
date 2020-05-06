using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
//using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Extensions;
using Ticketverkoop.Service;
using Ticketverkoop.Util.Mail;
using Ticketverkoop.ViewModel;
using Microsoft.AspNetCore.Http;
using Ticketverkoop.Areas.Identity.Data;

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
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(IMapper mapper, IEmailSender emailSender, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _emailSender = emailSender;
            orderService = new OrderService();
            orderlijnService = new OrderlijnService();
            ticketService = new TicketService();
            ringService = new RingService();
            vakService = new VakService();
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.lstRingen = new SelectList(ringService.GetAll(), "Factor", "Naam");
            ViewBag.lstVakken = new SelectList(vakService.GetAll(), "Factor", "Naam");

            //vm.vakken = new selectlist(service.all,)
            //var cartVM = new CartVM();
            //CartVM cartVM = new CartVM();
            //cartVM.Vakken = new SelectList(vakService.GetAll(), "Factor", "Naam");
            //cartVM.Ringen = new SelectList(ringService.GetAll(), "Factor", "Naam");

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
                    ticket.VakId = /*cart.VakId*/ 1;
                    ticket.RingId = /*cart.RingId*/ 1;
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

                    Ticket ticket2 = ticketService.Get(ticket.TicketId);
                    _emailSender.SendEmailAsync(User.Identity.Name, "bevestiging van betaling", "Ticket id: " + ticket2.TicketId + "\r Datum: " + ticket2.Wedstrijd.Datum + "\r\n Ring: " + ticket2.Ring.Naam + "\n Vak: " + ticket2.Vak.Naam + "\n\r Zitplaats: " + ticket2.ZitplaatsNr);
                }
                /*foreach(AbonnementCartVM abonnementCart in carts.AbonnementCart)
                {
                    abonnement = new Abonnement();
                    abonnement.ClubId = abonnementCart.Order_ID;

                    orderlijn = new Orderlijn();
                    orderlijn.OrderId = order.OrderId;
                    orderlijn.AbonnementId = abonnement.AbonnementId;

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Order> orders = orderService.OrdersPerUser(userId);

            List<Orderlijn> orderlijnen = new List<Orderlijn>();

            foreach (Order order in orders)
            {
                orderlijnen = orderlijnService.OrderlijnPerOrder(order.OrderId);
                //orderlijnen.Add(orderlijnService.OrderlijnPerOrder(order.OrderId));
                //orderlijnen.ToList().Add(orderlijnService.OrderlijnPerOrder(order.OrderId));
                //orderlijnen.AddRange()
            }
            IEnumerable<Ticket> tickets = new List<Ticket>();

            foreach (Orderlijn orderlijn in orderlijnen)
            {
                var ticketId = Convert.ToInt32(orderlijn.TicketId);
                tickets = ticketService.GetTickets(ticketId);
            }
            /*var list = _wedstrijdService.GetAll();*/
            //List<HistoriekVM> listVM = _mapper.Map<List<HistoriekVM>>(tickets);

            return View(tickets);
        }
    }
}
