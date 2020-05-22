using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class RingService
    {
        private RingDAO _ringDAO;

        public RingService()
        {
            _ringDAO = new RingDAO();
        }

        public Ring Get(int? id)
        {
            return _ringDAO.Get(id);
        }

        public IEnumerable<Ring> GetAll()
        {
            return _ringDAO.GetAll();
        }
    }
}
