using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class WedstrijdService
    {
        private WedstrijdDAO _wedstrijdDAO;

        public WedstrijdService()
        {
            _wedstrijdDAO = new WedstrijdDAO();
        }

        public IEnumerable<Wedstrijd> GetAll()
        {
            return _wedstrijdDAO.GetAll();
        }

        public IEnumerable<Wedstrijd> GetAllAankopen()
        {
            return _wedstrijdDAO.GetAllAankopen();
        }

        public async Task<Wedstrijd> Get(int id)
        {
            return await _wedstrijdDAO.Get(id);
        }

        public Wedstrijd GetWedstrijd(int id)
        {
            return _wedstrijdDAO.GetWedstrijd(id);
        }

        public IEnumerable<Wedstrijd> WedstrijdenPerClub(int ploegId)
        {
            return _wedstrijdDAO.WedstrijdenPerClub(ploegId);
        }

    }
}
