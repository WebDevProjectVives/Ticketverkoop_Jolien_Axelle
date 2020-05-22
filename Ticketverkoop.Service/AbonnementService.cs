using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class AbonnementService
    {
        private AbonnementDAO _abonnementDAO;
        public AbonnementService()
        {
            _abonnementDAO = new AbonnementDAO();
        }

        public Abonnement Get(int? id)
        {
            return _abonnementDAO.Get(id);
        }

        public Abonnement GetAbonnementPerUser(string userId, DateTime startdatum)
        {
            return _abonnementDAO.GetAbonnementPerUser(userId, startdatum);
        }

        public IEnumerable<Abonnement> AbonnementPerClub(int clubId)
        {
            return _abonnementDAO.AbonnementPerClub(clubId);
        }

        public void Insert(Abonnement entity)
        {
            _abonnementDAO.Insert(entity);
        }
    }
}
