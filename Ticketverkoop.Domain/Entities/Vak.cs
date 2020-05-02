using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Vak
    {
        public Vak()
        {
            StadionRingVak = new HashSet<StadionRingVak>();
        }

        public int VakId { get; set; }
        public string Naam { get; set; }
        public decimal Factor { get; set; }

        public virtual ICollection<StadionRingVak> StadionRingVak { get; set; }
    }
}
