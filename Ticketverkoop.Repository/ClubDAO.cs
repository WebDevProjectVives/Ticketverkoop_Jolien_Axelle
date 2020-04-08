using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class ClubDAO
    {
        private readonly VoetbalContext _dbContext;

        public ClubDAO()
        {
            _dbContext = new VoetbalContext();
        }

        public IEnumerable<Club> GetAll()
        {
            return _dbContext.Club.Include(c => c.StadionNavigatie).ToList();
        }
    }
}

