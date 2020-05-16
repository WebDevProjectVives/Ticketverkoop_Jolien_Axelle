using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticketverkoop.Domain.Entities;

namespace Ticketverkoop.Repository
{
    public class StadionRingVakDAO
    {
        private readonly VoetbalSQLContext _dbContext;

        public StadionRingVakDAO()
        {
            _dbContext = new VoetbalSQLContext();
        }

        public StadionRingVak AantalZitplaatsenPerVak(int stadionId, int ringId, int vakId)
        {
            return _dbContext.StadionRingVak
                .Where(s => s.StadionId == stadionId && s.RingId == ringId && s.VakId == vakId)
                .FirstOrDefault();
        }
    }
}
