using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Seizoen
    {
        public Seizoen()
        {
            Abonnement = new HashSet<Abonnement>();
        }

        public int SeizoenId { get; set; }
        public DateTime Startdatum { get; set; }
        public DateTime Einddatum { get; set; }

        public virtual ICollection<Abonnement> Abonnement { get; set; }
    }
}
