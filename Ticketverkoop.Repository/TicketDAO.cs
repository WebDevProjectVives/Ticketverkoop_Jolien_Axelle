using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class TicketDAO
    {
        private readonly VoetbalContext _dbContext;

        public TicketDAO()
        {
            _dbContext = new VoetbalContext();
        }


        public void Insert(Ticket entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }
    }
}
