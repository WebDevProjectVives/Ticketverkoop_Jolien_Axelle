using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class StadionService
    {
        private StadionDAO _stadionDAO;

        public StadionService()
        {
            _stadionDAO = new StadionDAO();
        }

        public IEnumerable<Stadion> GetAll()
        {
            return _stadionDAO.GetAll();
        }
    }
}
