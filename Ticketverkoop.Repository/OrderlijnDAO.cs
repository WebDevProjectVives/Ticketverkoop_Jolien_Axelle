using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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


        public void Insert(Orderlijn entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }
    }
}
