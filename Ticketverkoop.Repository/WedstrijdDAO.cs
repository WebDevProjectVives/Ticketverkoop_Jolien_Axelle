using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class WedstrijdDAO
    {
        private readonly VoetbalSQLContext _dbContext;

        public WedstrijdDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        public IEnumerable<Wedstrijd> GetAll()
        {
            return _dbContext.Wedstrijd.Include(w => w.Thuisploeg)
                .Include(w => w.Uitploeg)
                .Include(w => w.Stadion).ToList();
        }

        public IEnumerable<Wedstrijd> GetAllAankopen()
        {
            return _dbContext.Wedstrijd.Where(w => w.Datum <= DateTime.Today.AddMonths(1))
                .Include(w => w.Thuisploeg)
                .Include(w => w.Uitploeg)
                .Include(w => w.Stadion).ToList();
        }

        public async Task<Wedstrijd> Get(int id)
        {
            try
            {

                return await _dbContext.Wedstrijd
                         .Where(w => w.WedstrijdId == id).Include(w => w.Thuisploeg.Stadion)
                         .Include(w => w.Thuisploeg).Include(w => w.Uitploeg)
                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public Wedstrijd GetWedstrijd(int id)
        {
            return _dbContext.Wedstrijd
                .Where(w => w.WedstrijdId == id).Include(w => w.Stadion)
                .FirstOrDefault(); // -> null, zonder dit wordt er geen execute uitgevoerd!
        }

        public IEnumerable<Wedstrijd> WedstrijdenPerClub(int ploegId)
        {
            return _dbContext.Wedstrijd
                .Where(w => w.ThuisploegId == ploegId || w.UitploegId == ploegId)
                .Include(w => w.Thuisploeg)
                .Include(w => w.Uitploeg)
                .Include(w => w.Stadion).ToList();
        }
    }
}
