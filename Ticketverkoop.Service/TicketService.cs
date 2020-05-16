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

        public Ticket Get(int? id)
        {
            return _ticketDAO.Get(id);
        }
        public ICollection<Ticket> TicketsPerUserPerWedstrijd(string userId, int wedstrijdId)
        {
            return _ticketDAO.TicketsPerUserPerWedstrijd(userId, wedstrijdId);
        }

        public IEnumerable<Ticket> TicketsPerUser(string userId)
        {
            return _ticketDAO.TicketsPerUser(userId);
        }

        public IEnumerable<Ticket> TicketsPerUserAnnuleren(string userId)
        {
            return _ticketDAO.TicketsPerUserAnnuleren(userId);
        }

        public IEnumerable<Ticket> TicketsPerWedstrijd(int wedstrijdId, int ringId, int vakId)
        {
            return _ticketDAO.TicketsPerWedstrijd(wedstrijdId, ringId, vakId);
        }

        public List<Ticket> GetTickets(int id)
        {
            return _ticketDAO.GetTickets(id);
        }

        public void Insert(Ticket entity)
        {
            _ticketDAO.Insert(entity);
        }

        public void Delete(Ticket entity)
        {
            _ticketDAO.Delete(entity);
        }
    }
}
