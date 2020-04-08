﻿using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Abonnement
    {
        public int AbonnementId { get; set; }
        public int ZitplaatsNr { get; set; }
        public int ClubId { get; set; }
        public int KlantId { get; set; }
        public int SeizoenId { get; set; }
        public int StadionRingVakId { get; set; }

        public virtual Club Club { get; set; }
        public virtual Seizoen Seizoen { get; set; }
        public virtual StadionRingVak StadionRingVak { get; set; }
    }
}
