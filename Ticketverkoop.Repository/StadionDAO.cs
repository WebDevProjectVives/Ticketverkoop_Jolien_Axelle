using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<Stadion> Get(int id)
        {
            try
            {
                return await _dbContext.Stadion
                         .Where(b => b.StadionId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public IEnumerable<Stadion> GetPrijs(int id)
        {
            return _dbContext.Stadion.Where(b => b.StadionId == id).Include(b => b.Basisprijs);
        }
    }
}
