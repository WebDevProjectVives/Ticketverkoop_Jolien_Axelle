using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class OrderlijnDAO
    {
        private readonly VoetbalSQLContext _dbContext;

        public OrderlijnDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        public Orderlijn GetOrderlijnVanTicket(int? id)
        {
            return _dbContext.Orderlijn
                .Where(o => o.TicketId == id)
                .FirstOrDefault(); // -> null, zonder dit wordt er geen execute uitgevoerd!
        }

        public List<Orderlijn> OrderlijnPerOrder(int orderId)
        {
            return _dbContext.Orderlijn.Where(o => o.OrderId == orderId)
                .ToList();
        }

        /*
        public IEnumerable<Orderlijn> OrderlijnPerTicket(IEnumerable<Order> orders)
        {
                return _dbContext.Orderlijn
                   .Where(o => o.OrderId == order.OrderId)
                   orders.ToList().ForEach(o => o.OrderId);
            
        }*/

        public void Insert(Orderlijn entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        public void Delete(Orderlijn entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }
    }
}
