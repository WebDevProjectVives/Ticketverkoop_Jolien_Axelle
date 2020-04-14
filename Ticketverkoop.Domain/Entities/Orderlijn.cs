using System;
using System.Collections.Generic;
using System.Text;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Orderlijn
    {
        public int OrderlijnId { get; set; }
        public int OrderId { get; set; }
        public int TicketId { get; set; }
        public int AbonnementId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Abonnement Abonnement { get; set; }
    }
}
