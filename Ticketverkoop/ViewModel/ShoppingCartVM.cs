using Microsoft.AspNetCore.Mvc.Rendering;
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
        public int Wedstrijd_ID { get; set; }
        public int Stadion_ID { get; set; }
        public DateTime Datum { get; set; }
        public string Thuisploeg { get; set; }
        public string Uitploeg { get; set; }
        public string Ring { get; set; }
        public SelectList Ringen { get; set; }
        public string RingFactor { get; set; }
        public string  Vak { get; set; }
        public SelectList Vakken { get; set; }
        public string VakFactor { get; set; }
        public int Aantal { get; set; }
        public decimal Prijs { get; set; }
        public System.DateTime DateCreated { get; set; }

    }
    
    public class AbonnementCartVM
    {
        public int Club_ID { get; set; }
        public string Naam { get; set; }
        public string Ring { get; set; }
        public SelectList Ringen { get; set; }
        public string RingFactor { get; set; }
        public string Vak { get; set; }
        public SelectList Vakken { get; set; }
        public string VakFactor { get; set; }
        public int Aantal { get; set; }
        public decimal Prijs { get; set; }
        public System.DateTime DateCreated { get; set; }

    }
    
}
