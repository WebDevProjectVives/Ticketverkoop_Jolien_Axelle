﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class RingDAO
    {
        private readonly VoetbalSQLContext _dbContext;

        public RingDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        public Ring Get(int? id)
        {
            return _dbContext.Ring
                .Where(r => r.RingId == id)
                .FirstOrDefault(); // -> null, zonder dit wordt er geen execute uitgevoerd!
        }

        public IEnumerable<Ring> GetAll()
        {
            return _dbContext.Ring.ToList();
        }
    }
}
