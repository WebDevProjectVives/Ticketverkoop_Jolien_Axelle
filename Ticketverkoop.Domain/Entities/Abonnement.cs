using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Abonnement
    {
        public int AbonnementId { get; set; }
        public int ZitplaatsNr { get; set; }
        public int ClubId { get; set; }
        public int SeizoenId { get; set; }
        public int RingId { get; set; }
        public int VakId { get; set; }
        public string UserId { get; set; }

        public virtual Club Club { get; set; }
        public virtual Ring Ring { get; set; }
        public virtual Seizoen Seizoen { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual Vak Vak { get; set; }
    }
}
