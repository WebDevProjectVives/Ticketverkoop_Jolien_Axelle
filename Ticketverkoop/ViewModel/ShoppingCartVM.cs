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
        public int Stadion_Ring_Vak_ID { get; set; }
        public int Aantal { get; set; }
        public float Prijs { get; set; }
        public System.DateTime DateCreated { get; set; }

    }
}
