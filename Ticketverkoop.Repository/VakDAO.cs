using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class VakDAO
    {
        private readonly VoetbalSQLContext _dbContext;

        public VakDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        public IEnumerable<Vak> GetAll()
        {
            return _dbContext.Vak.ToList();
        }
    }
}
