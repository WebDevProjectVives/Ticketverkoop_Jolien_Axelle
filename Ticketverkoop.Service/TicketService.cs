using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class TicketService
    {
        private TicketDAO _ticketDAO;
        public TicketService()
        {
            _ticketDAO = new TicketDAO();
        }

        public Ticket Get(int id)
        {
            return _ticketDAO.Get(id);
        }

        public void Insert(Ticket entity)
        {
            _ticketDAO.Insert(entity);
        }
    }
}
