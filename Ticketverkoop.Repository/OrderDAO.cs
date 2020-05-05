using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Order> Get(int id)
        {
            try
            {

                return await _dbContext.Order
                         .Where(b => b.OrderId == id).Include(b => b.User)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public void Insert(Order entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

    }
}
