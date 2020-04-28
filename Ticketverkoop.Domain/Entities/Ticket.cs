using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Ticket
    {
        public Ticket()
        {
            Orderlijn = new HashSet<Orderlijn>();
        }

        public int TicketId { get; set; }
        public int WedstrijdId { get; set; }
        public int ZitplaatsNr { get; set; }
        public int StadionRingVakId { get; set; }

        public virtual StadionRingVak StadionRingVak { get; set; }
        public virtual Wedstrijd Wedstrijd { get; set; }
        public virtual ICollection<Orderlijn> Orderlijn { get; set; }
    }
}
