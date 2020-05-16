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
        private StadionRingVakService stadionRingVakService;
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

            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            if (cartList != null)
            {
                foreach (CartVM cart in cartList.Cart)
                {
                    cart.Ringen = new SelectList(ringService.GetAll(), "RingFactor", "Naam");
                    cart.Vakken = new SelectList(vakService.GetAll(), "VakFactor", "Naam");
                }
            }

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
                order.DateCreated = DateTime.UtcNow;
                orderService = new OrderService();
                orderService.Insert(order);

                foreach (CartVM cart in carts.Cart)
                {
                    ICollection<Ticket> ticketsUser = ticketService.TicketsPerUserPerWedstrijd(userID, cart.Wedstrijd_ID);
                    if (ticketsUser.Count + cart.Aantal <= 10)
                    {

                        for (var i = 0; i < cart.Aantal; i++)
                        {
                            ticket = new Ticket();
                            ticket.WedstrijdId = cart.Wedstrijd_ID;
                            ticket.VakId = Convert.ToInt32(cart.VakFactor.Substring(0, 1));
                            ticket.RingId = Convert.ToInt32(cart.RingFactor.Substring(0, 1));

                            stadionRingVakService = new StadionRingVakService();
                            _wedstrijdService = new WedstrijdService();
                            Wedstrijd wedstrijd = _wedstrijdService.GetWedstrijd(ticket.WedstrijdId);
                            var aantalZitplaatsen = stadionRingVakService.AantalZitplaatsenPerVak(wedstrijd.StadionId, ticket.RingId, ticket.VakId);
                            IEnumerable<Ticket> tickets = ticketService.TicketsPerWedstrijd(ticket.WedstrijdId, ticket.RingId, ticket.VakId);

                            if (tickets.Count() + cart.Aantal <= aantalZitplaatsen.AantalZitplaatsen)
                            {
                                int zitplaatsVrij = 1;
                                int j = 1;
                                foreach (Ticket ticket1 in tickets)
                                {
                                    if (ticket1.ZitplaatsNr != j)
                                    {
                                        break;
                                    }
                                    j++;
                                }
                                zitplaatsVrij = j;
                                ticket.ZitplaatsNr = zitplaatsVrij;
                                ticket.UserId = userID;
                                ticketService = new TicketService();
                                ticketService.Insert(ticket);
                                //create orderlijn object
                                orderlijn = new Orderlijn();
                                orderlijn.OrderId = order.OrderId;
                                orderlijn.TicketId = ticket.TicketId;

                                orderlijnService = new OrderlijnService();
                                orderlijnService.Insert(orderlijn);

                                Ticket ticket2 = ticketService.Get(ticket.TicketId);
                                _emailSender.SendEmailAsync(User.Identity.Name, "bevestiging van betaling", "Ticket id: " + ticket2.TicketId + "\r Datum: " + ticket2.Wedstrijd.Datum + "\r\n Ring: " + ticket2.Ring.Naam + "\n Vak: " + ticket2.Vak.Naam + "\n\r Zitplaats: " + ticket2.ZitplaatsNr);

                            }
                            else
                            {
                                ViewBag.Message = "Er zijn onvoldoende zitplaatsen beschikbaar in het geselecteerde vak. Uw betaling is dus niet gelukt.";
                            }
                        }

                    }
                    else
                    {
                        ViewBag.Message = "U kan maximum 10 tickets kopen per match. U heeft het maximum bereikt. Uw betaling is niet gelukt.";
                    }
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
                ModelState.AddModelError("", "Opslaan niet gelukt");
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
            var list = ticketService.TicketsPerUser(userId);
            List<HistoriekVM> listVM = _mapper.Map<List<HistoriekVM>>(list);

            return View(listVM);
        }

        [Authorize]
        public IActionResult Annulatie()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = ticketService.TicketsPerUserAnnuleren(userId);
            List<HistoriekVM> listVM = _mapper.Map<List<HistoriekVM>>(list);

            return View(listVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ticket ticket = ticketService.Get(Convert.ToInt32(id));
            if (ticket == null)
            {
                return NotFound();
            }
            var historiekVM = _mapper.Map<HistoriekVM>(ticket);

            return View(historiekVM);
        }

        //POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                Ticket ticket = ticketService.Get(Convert.ToInt32(id));
                Orderlijn orderlijn = orderlijnService.GetOrderlijnVanTicket(ticket.TicketId);
                if (ticket == null)
                {
                    return NotFound();
                }
                if (orderlijn == null)
                {
                    return NotFound();
                }
                orderlijnService.Delete(orderlijn);
                ticketService.Delete(ticket);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bel systeem administrator");
                return View();
            }
        }
    }
}
