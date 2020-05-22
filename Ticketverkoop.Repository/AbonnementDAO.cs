using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class AbonnementDAO
    {
        private readonly VoetbalSQLContext _dbContext;

        public AbonnementDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        public Abonnement Get(int? id)
        {
            return _dbContext.Abonnement
                .Where(a => a.AbonnementId == id).Include(a => a.Seizoen).Include(a => a.Club)
                .FirstOrDefault(); // -> null, zonder dit wordt er geen execute uitgevoerd!
        }

        public Abonnement GetAbonnementPerUser(string userId, DateTime startdatum)
        {
            return _dbContext.Abonnement
                .Where(a => a.UserId == userId && a.Seizoen.Startdatum == startdatum).Include(a => a.Seizoen).Include(a => a.Club)
                .FirstOrDefault(); // -> null, zonder dit wordt er geen execute uitgevoerd!
        }

        public IEnumerable<Abonnement> AbonnementPerClub(int clubId)
        {
            return _dbContext.Abonnement
                .Where(a => a.ClubId == clubId).ToList();
        }

        public void Insert(Abonnement entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }
    }
}
