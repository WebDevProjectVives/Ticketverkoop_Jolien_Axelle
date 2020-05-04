using System;
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

        public IEnumerable<Ring> GetAll()
        {
            return _dbContext.Ring.ToList();
        }
    }
}
