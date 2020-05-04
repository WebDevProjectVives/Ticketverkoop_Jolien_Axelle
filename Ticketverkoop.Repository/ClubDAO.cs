using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class ClubDAO
    {
        private readonly VoetbalSQLContext _dbContext;

        public ClubDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        public IEnumerable<Club> GetAll()
        {
            return _dbContext.Club.Include(c => c.Stadion).ToList();
        }

        public async Task<Club> Get(int id)
        {
            try
            {

                return await _dbContext.Club
                         .Where(c => c.ClubId == id).Include(c => c.Stadion)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}

