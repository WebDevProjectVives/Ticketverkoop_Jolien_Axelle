using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
