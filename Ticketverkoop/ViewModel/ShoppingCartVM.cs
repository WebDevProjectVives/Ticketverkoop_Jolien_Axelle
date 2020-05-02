using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModel
{
    public class ShoppingCartVM
    {
        public List<CartVM> Cart { get; set; }
        public List<AbonnementCartVM> AbonnementCart { get; set; }
    }

    public class CartVM
    {
        //public int Orderlijn_ID { get; set; }
        public int Wedstrijd_ID { get; set; }
        public int Stadion_ID { get; set; }
        public DateTime Datum { get; set; }
        public string Thuisploeg { get; set; }
        public string Uitploeg { get; set; }
        public string Ring { get; set; }
        public decimal RingFactor { get; set; }
        public string  Vak { get; set; }
        public decimal VakFactor { get; set; }
        //public int Ticket_ID { get; set; }
        //public int Abonnement_ID { get; set; }
        public int Aantal { get; set; }
        public decimal Prijs { get; set; }
        public System.DateTime DateCreated { get; set; }

    }

    public class AbonnementCartVM
    {
        public int Club_ID { get; set; }
        public string Naam { get; set; }
        public string Ring { get; set; }
        public string Vak { get; set; }
        public int Aantal { get; set; }
        public decimal Prijs { get; set; }
        public System.DateTime DateCreated { get; set; }

    }
}
