using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Ring
    {
        public Ring()
        {
            StadionRingVak = new HashSet<StadionRingVak>();
            Ticket = new HashSet<Ticket>();
        }

        public int RingId { get; set; }
        public string Naam { get; set; }
        public string RingFactor { get; set; }

        public virtual ICollection<StadionRingVak> StadionRingVak { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
