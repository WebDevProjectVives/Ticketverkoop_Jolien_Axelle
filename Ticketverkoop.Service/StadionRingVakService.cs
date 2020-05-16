using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class StadionRingVakService
    {
        private StadionRingVakDAO _stadionRingVakDAO;

        public StadionRingVakService()
        {
            _stadionRingVakDAO = new StadionRingVakDAO();
        }

        public StadionRingVak AantalZitplaatsenPerVak(int stadionId, int ringId, int vakId)
        {
            return _stadionRingVakDAO.AantalZitplaatsenPerVak(stadionId, ringId, vakId);
        }
    }
}
