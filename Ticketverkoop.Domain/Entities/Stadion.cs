using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Stadion
    {
        public Stadion()
        {
            Club = new HashSet<Club>();
            StadionRingVak = new HashSet<StadionRingVak>();
            Wedstrijd = new HashSet<Wedstrijd>();
        }

        public int StadionId { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public decimal Basisprijs { get; set; }

        public virtual ICollection<Club> Club { get; set; }
        public virtual ICollection<StadionRingVak> StadionRingVak { get; set; }
        public virtual ICollection<Wedstrijd> Wedstrijd { get; set; }
    }
}
