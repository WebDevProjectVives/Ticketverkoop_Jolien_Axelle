using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class SeizoenDAO
    {
        private readonly VoetbalSQLContext _dbContext;

        public SeizoenDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        public Seizoen GetByDatum(DateTime datum)
        {
            return _dbContext.Seizoen
                .Where(s => s.Startdatum > datum)
                .FirstOrDefault(); // -> null, zonder dit wordt er geen execute uitgevoerd!
        }
    }
}
