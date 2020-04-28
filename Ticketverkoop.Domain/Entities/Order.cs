using System;
using System.Collections.Generic;

namespace Ticketverkoop.Domain.Entities
{
    public partial class Order
    {
        public Order()
        {
            Orderlijn = new HashSet<Orderlijn>();
        }

        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Orderlijn> Orderlijn { get; set; }
    }
}
