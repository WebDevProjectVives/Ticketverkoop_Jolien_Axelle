using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class VakDAO
    {
        private readonly VoetbalContext _dbContext;

        public VakDAO()
        {
            _dbContext = new VoetbalContext();
        }

        public IEnumerable<Vak> GetAll()
        {
            return _dbContext.Vak.ToList();
        }
    }
}
