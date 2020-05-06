﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class TicketDAO
    {
        private readonly VoetbalSQLContext _dbContext;

        public TicketDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        public Ticket Get(int id)
        {
            return _dbContext.Ticket
                .Where(t => t.TicketId == id).Include(t => t.Ring).Include(t => t.Vak).Include(t => t.Wedstrijd)
                .FirstOrDefault(); // -> null, zonder dit wordt er geen execute uitgevoerd!
        }


        public void Insert(Ticket entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }
    }
}
