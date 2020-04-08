using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class WedstrijdDAO
    {
        private readonly VoetbalContext _dbContext;

        public WedstrijdDAO()
        {
            _dbContext = new VoetbalContext();
        }

        public IEnumerable<Wedstrijd> GetAll()
        {
            return _dbContext.Wedstrijd.Include(w => w.Thuisploeg)
                .Include(w => w.Uitploeg)
                .Include(w => w.Stadion).ToList();
        }
    }
}
