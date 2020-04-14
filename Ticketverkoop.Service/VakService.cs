using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class VakService
    {
        private VakDAO _vakDAO;

        public VakService()
        {
            _vakDAO = new VakDAO();
        }

        public IEnumerable<Vak> GetAll()
        {
            return _vakDAO.GetAll();
        }
    }
}
