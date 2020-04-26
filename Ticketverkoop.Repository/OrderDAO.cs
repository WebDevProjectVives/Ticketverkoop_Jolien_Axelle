using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class OrderDAO
    {
        private readonly VoetbalContext _dbContext;

        public OrderDAO()
        {
            _dbContext = new VoetbalContext();
        }

        public IEnumerable<Order> GetAll()
        {
            return _dbContext.Order.ToList();
        }

    }
}
