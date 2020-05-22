using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class SeizoenService
    {
        private SeizoenDAO _seizoenDAO;

        public SeizoenService()
        {
            _seizoenDAO = new SeizoenDAO();
        }

        public Seizoen GetByDatum(DateTime datum)
        {
            return _seizoenDAO.GetByDatum(datum);
        }
    }
}
