using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModel;


namespace Ticketverkoop.Controllers
{
    public class StadionController : Controller
    {
        private StadionService _stadionService;
        private readonly IMapper _mapper;

        public StadionController(IMapper mapper)
        {
            _mapper = mapper;
            _stadionService = new StadionService();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = _stadionService.GetAll();
            List<StadionVM> listVM = _mapper.Map<List<StadionVM>>(list);
            return View(listVM);
        }

        public IActionResult Info()
        {
            var stadions = new List<StadionInfoVM> //generieke lijst
            {
                new StadionInfoVM {Naam = "Jan Breydelstadion" , Omschrijving = "Het Jan Breydelstadion, tot 1998 het Olympiastadion, is een voetbalstadion in de stad Brugge, gelegen in de deelgemeente Sint-Andries. Het stadion is eigendom van de stad Brugge en is de thuishaven van stadsrivalen Club Brugge en Cercle Brugge, die het stadion exploiteren.", 
                    AantalZitplaatsen = 20000 , ImagePath = "~/Images/Stadions/JanBreydelStadion.png" },
                new StadionInfoVM {Naam = "Constant Vanden Stock stadion" , Omschrijving = "Het Constant Vanden Stock stadion is een voetbalstadion in de Belgische gemeente Anderlecht, in het Brussels Hoofdstedelijk Gewest. Het is de thuisbasis van voetbalclub RSC Anderlecht. Het stadion bevindt zich in het Astridpark, verwijzend naar de Zweedse prinses en Koningin van België Astrid van Zweden, die in 1935 overleed.",
                    AantalZitplaatsen = 20000 , ImagePath = "~/Images/Stadions/ConstantVandenStock.png" },
                new StadionInfoVM {Naam = "Albertparkstadion" , Omschrijving = "Albertparkstadion is een voetbalstadion gelegen in Mariakerke, een deelgemeente van de West-Vlaamse badstad Oostende. Het is de thuisbasis van KV Oostende, dat sinds het seizoen 2013/14 opnieuw in de Jupiler Pro League aantreedt.",
                    AantalZitplaatsen = 12000 , ImagePath = "~/Images/Stadions/AlbertParkstadion.png" },
                new StadionInfoVM {Naam = "Regenboogstadion" , Omschrijving = "Het Regenboogstadion is een voetbalstadion in Waregem. Het stadion is eigendom van Stad Waregem, maar de voetbalploeg SV Zulte Waregem gebruikt het stadion in erfpacht.",
                    AantalZitplaatsen = 12000 , ImagePath = "~/Images/Stadions/Regenboogstadion.png" },
                new StadionInfoVM {Naam = "Ghelamco Arena" , Omschrijving = "De Ghelamco Arena of het Arteveldestadion is het huidig stadion van de Belgische voetbalclub KAA Gent, de grootste club uit de stad Gent. Het stadion werd op 17 juli 2013 geopend met een wedstrijd tegen het Duitse VfB Stuttgart.",
                    AantalZitplaatsen = 16000 , ImagePath = "~/Images/Stadions/GhelamcoArena.png" },
                new StadionInfoVM {Naam = "Cristal Arena" , Omschrijving = "De Cristal Arena is het stadion van voetbalclub KRC Genk. Het is met het op twee na grootste stadion van België. Alleen het Koning Boudewijnstadion en het Jan Breydelstadion van Cercle Brugge en Club Brugge zijn groter.",
                    AantalZitplaatsen = 16000 , ImagePath = "~/Images/Stadions/CristalArena.png" },
            };
            return View(stadions);
        }
    }
}
