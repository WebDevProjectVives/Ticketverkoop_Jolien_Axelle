using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModel
{
    public class ShoppingCartVM
    {
        public List<CartVM> Cart { get; set; }
    }

    public class CartVM
    {
        //public int Orderlijn_ID { get; set; }
        public int Order_ID { get; set; }
        public string Naam { get; set; }
        //public int Ticket_ID { get; set; }
        //public int Abonnement_ID { get; set; }
        //public int Aantal { get; set; }
        //public float Prijs { get; set; }
        public System.DateTime DateCreated { get; set; }

    }
}
