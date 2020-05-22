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
    public class ClubController : Controller
    {
        private ClubService _clubService;
        private WedstrijdService _wedstrijdService;
        private SeizoenService _seizoenService; 
        private readonly IMapper _mapper;

        public ClubController(IMapper mapper)
        {
            _clubService = new ClubService();
            _seizoenService = new SeizoenService();
            _mapper = mapper;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = _clubService.GetAll();
            List<ClubVM> listVM = _mapper.Map<List<ClubVM>>(list);
            return View(listVM);
        }

        public IActionResult Info()
        {
            var clubs = new List<ClubInfoVM> //generieke lijst
            {
                new ClubInfoVM {Naam = "Club Brugge", 
                    Omschrijving = "Club Brugge KV is een Belgische voetbalclub, uitkomend in eerste klasse. Het is de populairste van de twee profclubs uit Brugge – de andere is Cercle Brugge.", 
                    Ontstaan = 1891, 
                    ImagePath = "~/Images/Clubs/club_logo.png" },
                new ClubInfoVM {Naam = "Oostende",
                    Omschrijving = "KV Oostende is een Belgische voetbalclub uit Oostende. De club is aangesloten bij de Voetbalbond met stamnummer 31 en heeft groen-rood-geel als kleuren.",
                    Ontstaan = 1904,
                    ImagePath = "~/Images/Clubs/oostende_logo.png" },
                new ClubInfoVM {Naam = "RSC Anderlecht",
                    Omschrijving = "Royal Sporting Club Anderlecht is een Belgische voetbalclub uit Brussel, meer bepaald uit de Brusselse gemeente Anderlecht.",
                    Ontstaan = 1908,
                    ImagePath = "~/Images/Clubs/anderlecht_logo.png" },
                new ClubInfoVM {Naam = "Zulte Waregem",
                    Omschrijving = "Sportvereniging Zulte Waregem is een Belgische voetbalclub uit Waregem en Zulte.",
                    Ontstaan = 2001,
                    ImagePath = "~/Images/Clubs/zulte_logo.png" },
                new ClubInfoVM {Naam = "Genk",
                    Omschrijving = "Koninklijke Racing Club Genk is een Belgische voetbalclub uit Genk, die bij de KBVB aangesloten is met stamnummer 322, voordien het nummer van KFC Winterslag.",
                    Ontstaan = 1988,
                    ImagePath = "~/Images/Clubs/genk_logo.png" },
                new ClubInfoVM {Naam = "AA Gent",
                    Omschrijving = "Koninklijke Atletiek Associatie Gent, afgekort KAA Gent of AA Gent is een Belgische voetbalclub uit Gent.",
                    Ontstaan = 1900,
                    ImagePath = "~/Images/Clubs/gent_logo.png" },
            };
            return View(clubs);
        }

        public IActionResult WedstrijdenPerClub()
        {
            ViewBag.lstClub = new SelectList(_clubService.GetAll(), "ClubId", "Naam");
            return View();
        }

        [HttpPost]
        public IActionResult WedstrijdenPerClub(int? clubId)
        {
            if (clubId == null)
            {
                return NotFound();
            }
            _wedstrijdService = new WedstrijdService();
            var wedstrijden = _wedstrijdService.WedstrijdenPerClub(Convert.ToInt16(clubId));

            ViewBag.lstClub = new SelectList(_clubService.GetAll(), "ClubId", "Naam", clubId);

            List<WedstrijdVM> listVM = _mapper.Map<List<WedstrijdVM>>(wedstrijden);
            return View(listVM);
        }

        public async Task<ActionResult> Select(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Club club = await _clubService.Get(Convert.ToInt32(id));
            Seizoen seizoen = _seizoenService.GetByDatum(DateTime.Now);

            AbonnementCartVM item = new AbonnementCartVM
            {
                Club_ID = club.ClubId,
                Naam = club.Naam,
                Startdatum = seizoen.Startdatum,
                Einddatum = seizoen.Einddatum,
                Prijs = club.Stadion.Basisprijs * 8,
                DateCreated = DateTime.Now
            };

            ShoppingCartVM shopping;

            if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
                if (shopping.AbonnementCart == null)
                {
                    shopping.AbonnementCart = new List<AbonnementCartVM>();
                }
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.AbonnementCart = new List<AbonnementCartVM>();
            }
            shopping.AbonnementCart.Add(item);


            HttpContext.Session.SetObject("ShoppingCart", shopping);

            return RedirectToAction("Index", "ShoppingCart");
        }

    }
}
