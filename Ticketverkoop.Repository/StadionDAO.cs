using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class StadionDAO
    {
        private readonly VoetbalContext _dbContext;

        public StadionDAO ()
        {
            _dbContext = new VoetbalContext();
        }

        public IEnumerable<Stadion> GetAll()
        {
            return _dbContext.Stadion.ToList();
        }
    }
}
