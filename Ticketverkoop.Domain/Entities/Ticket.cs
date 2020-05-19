using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public int WedstrijdId { get; set; }
        public int ZitplaatsNr { get; set; }
        public int RingId { get; set; }
        public int VakId { get; set; }
        public string UserId { get; set; }

        public virtual Ring Ring { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual Vak Vak { get; set; }
        public virtual Wedstrijd Wedstrijd { get; set; }
    }
}
