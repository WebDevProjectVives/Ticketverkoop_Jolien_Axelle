using System.Collections.Generic;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class ClubService
    {
        private ClubDAO _clubDAO;

        public ClubService()
        {
            _clubDAO = new ClubDAO();
        }

        public IEnumerable<Club> GetAll()
        {
            return _clubDAO.GetAll();
        }
    }
}
