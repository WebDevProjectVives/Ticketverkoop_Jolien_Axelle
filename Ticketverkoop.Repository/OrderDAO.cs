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
        private readonly VoetbalSQLContext _dbContext;

        public OrderDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        /*public IEnumerable<Order> GetAll()
        {
            return _dbContext.Order.ToList();
        }*/

        public void Insert(Order entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

    }
}
