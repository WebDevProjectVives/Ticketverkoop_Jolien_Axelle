using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class TicketDAO
    {
        private readonly VoetbalSQLContext _dbContext;

        public TicketDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        public Ticket Get(int? id)
        {
            return _dbContext.Ticket
                .Where(t => t.TicketId == id).Include(t => t.Wedstrijd.Thuisploeg).Include(t => t.Wedstrijd.Uitploeg).Include(t => t.Ring).Include(t => t.Vak).Include(t => t.Wedstrijd)
                .FirstOrDefault(); // -> null, zonder dit wordt er geen execute uitgevoerd!
        }

        public ICollection<Ticket> TicketsPerUserPerWedstrijd(string userId, int wedstrijdId)
        {
            return _dbContext.Ticket
               .Where(t => t.UserId == userId && t.WedstrijdId == wedstrijdId)
               .Include(t => t.Wedstrijd.Thuisploeg)
               .Include(t => t.Wedstrijd.Uitploeg)
               .Include(t => t.Ring)
               .Include(t => t.Vak)
               .ToList();
        }

        public IEnumerable<Ticket> TicketsPerUser(string userId)
        {
            return _dbContext.Ticket
               .Where(t => t.UserId == userId)
               .Include(t => t.Wedstrijd.Thuisploeg)
               .Include(t => t.Wedstrijd.Uitploeg)
               .Include(t => t.Ring)
               .Include(t => t.Vak)
               .ToList();
        }

        public IEnumerable<Ticket> TicketsPerUserPerDatum(string userId, DateTime datum, int wedstrijdId)
        {
            return _dbContext.Ticket
               .Where(t => t.UserId == userId && t.Wedstrijd.Datum == datum && t.WedstrijdId != wedstrijdId)
               .Include(t => t.Wedstrijd.Thuisploeg)
               .Include(t => t.Wedstrijd.Uitploeg)
               .Include(t => t.Ring)
               .Include(t => t.Vak)
               .ToList();
        }

        public IEnumerable<Ticket> TicketsPerUserAnnuleren(string userId)
        {
            return _dbContext.Ticket
               .Where(t => t.UserId == userId && t.Wedstrijd.Datum >= DateTime.Today.AddDays(7))
               .Include(t => t.Wedstrijd.Thuisploeg)
               .Include(t => t.Wedstrijd.Uitploeg)
               .Include(t => t.Ring)
               .Include(t => t.Vak)
               .ToList();
        }

        public IEnumerable<Ticket> TicketsPerWedstrijd(int wedstrijdId, int ringId, int vakId)
        {
            return _dbContext.Ticket
               .Where(t => t.WedstrijdId == wedstrijdId && t.RingId == ringId && t.VakId == vakId)
               .ToList();
        }

        public List<Ticket> GetTickets(int? id)
        {
            return _dbContext.Ticket.Where(t => t.TicketId == id)
                .ToList();
        }

        public void Insert(Ticket entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        public void Delete(Ticket entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }
    }
}
