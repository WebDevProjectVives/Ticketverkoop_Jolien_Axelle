using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class StadionService
    {
        private StadionDAO _stadionDAO;

        public StadionService()
        {
            _stadionDAO = new StadionDAO();
        }

        public IEnumerable<Stadion> GetAll()
        {
            return _stadionDAO.GetAll();
        }

        public async Task<Stadion> Get(int id)
        {
            return await _stadionDAO.Get(id);
        }

        public IEnumerable<Stadion> GetPrijs(int id)
        {
            return _stadionDAO.GetPrijs(id);
        }
    }
}
