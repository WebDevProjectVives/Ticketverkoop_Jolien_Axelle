using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Club
    {
        public Club()
        {
            Abonnement = new HashSet<Abonnement>();
            WedstrijdThuisploeg = new HashSet<Wedstrijd>();
            WedstrijdUitploeg = new HashSet<Wedstrijd>();
        }

        public int ClubId { get; set; }
        public string Naam { get; set; }
        public int StadionId { get; set; }
        public string Logo { get; set; }

        public virtual Stadion Stadion { get; set; }
        public virtual ICollection<Abonnement> Abonnement { get; set; }
        public virtual ICollection<Wedstrijd> WedstrijdThuisploeg { get; set; }
        public virtual ICollection<Wedstrijd> WedstrijdUitploeg { get; set; }
    }
}
