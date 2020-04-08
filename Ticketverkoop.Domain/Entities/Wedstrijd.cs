using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Wedstrijd
    {
        public Wedstrijd()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int WedstrijdId { get; set; }
        public DateTime Datum { get; set; }
        public int ThuisploegId { get; set; }
        public int UitploegId { get; set; }
        public int StadionId { get; set; }

        public virtual Stadion Stadion { get; set; }
        public virtual Club Thuisploeg { get; set; }
        public virtual Club Uitploeg { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
