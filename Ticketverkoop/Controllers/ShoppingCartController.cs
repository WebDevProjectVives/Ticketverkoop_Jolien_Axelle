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
        private TicketService ticketService;
        private StadionRingVakService stadionRingVakService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(IMapper mapper, IEmailSender emailSender, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _emailSender = emailSender;
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
                Ticket ticket;


                foreach (CartVM cart in carts.Cart)
                {
                    IEnumerable<Ticket> ticketsUser = ticketService.TicketsPerUserPerDatum(userID, cart.Datum, cart.Wedstrijd_ID);
                    ICollection<Ticket> ticketsUserWedstrijd = ticketService.TicketsPerUserPerWedstrijd(userID, cart.Wedstrijd_ID);

                    if (ticketsUser.Count() == 0)
                    {


                        if (ticketsUserWedstrijd.Count + cart.Aantal <= 10)
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

                                    Ticket ticket2 = ticketService.Get(ticket.TicketId);
                                    _emailSender.SendEmailAsync(User.Identity.Name, "bevestiging van betaling", "Ticket id: " + ticket2.TicketId + Environment.NewLine + " Datum: " + ticket2.Wedstrijd.Datum + Environment.NewLine + " Thuisploeg: " + ticket2.Wedstrijd.Thuisploeg.Naam + Environment.NewLine + " Uitploeg: " + ticket2.Wedstrijd.Uitploeg.Naam + Environment.NewLine + "\r\n Ring: " + ticket2.Ring.Naam + Environment.NewLine + "\n Vak: " + ticket2.Vak.Naam + Environment.NewLine + " Zitplaats: " + ticket2.ZitplaatsNr);;

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
                    else
                    {
                        ViewBag.Message = "U kan op eenzelfde dag geen twee verschillende wedstrijden gaan bekijken! Enkel de tickets voor de eerst geselecteerde wedstrijd werden aangekocht.";
                    }
                }

            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Opslaan niet gelukt");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bel systeem administrator");
            }

            HttpContext.Session.Clear();
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
                if (ticket == null)
                {
                    return NotFound();
                }
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
