using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class StadionRingVak
    {
        public StadionRingVak()
        {
            Abonnement = new HashSet<Abonnement>();
        }

        public int StadionRingVakId { get; set; }
        public int StadionId { get; set; }
        public int RingId { get; set; }
        public int VakId { get; set; }
        public int AantalZitplaatsen { get; set; }
        public decimal Prijs { get; set; }

        public virtual Ring Ring { get; set; }
        public virtual Stadion Stadion { get; set; }
        public virtual Vak Vak { get; set; }
        public virtual ICollection<Abonnement> Abonnement { get; set; }
    }
}
