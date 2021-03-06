﻿using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Vak
    {
        public Vak()
        {
            Abonnement = new HashSet<Abonnement>();
            StadionRingVak = new HashSet<StadionRingVak>();
            Ticket = new HashSet<Ticket>();
        }

        public int VakId { get; set; }
        public string Naam { get; set; }
        public string VakFactor { get; set; }

        public virtual ICollection<Abonnement> Abonnement { get; set; }
        public virtual ICollection<StadionRingVak> StadionRingVak { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
