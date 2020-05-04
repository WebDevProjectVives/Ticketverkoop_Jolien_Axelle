using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticketverkoop.ViewModel
{
    public class OrderVM
    {
        /*public List<OrderlijnVM> Orderlijn { get; set; }*/
        public int User_ID { get; set; }
        public DateTime Date_Created { get; set; }

    }

    public class OrderlijnVM
    {
        public int Orderlijn_ID { get; set; }
        public int Order_ID { get; set; }
        public int? Ticket_ID { get; set; }
        public int? Abonnement_ID { get; set; }

    }
}
