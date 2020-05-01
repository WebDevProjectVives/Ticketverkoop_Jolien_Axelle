using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Ring
    {
        public Ring()
        {
            StadionRingVak = new HashSet<StadionRingVak>();
        }

        public int RingId { get; set; }
        public string Naam { get; set; }
        public int? Factor { get; set; }

        public virtual ICollection<StadionRingVak> StadionRingVak { get; set; }
    }
}
