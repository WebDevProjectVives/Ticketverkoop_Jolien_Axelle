using System;
using System.Collections.Generic;
using System.Text;

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

        public virtual AspNetUsers Id { get; set; }

        public virtual ICollection<Orderlijn> Orderlijn { get; set; }
    }
}
