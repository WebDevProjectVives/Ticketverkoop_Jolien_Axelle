using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class OrderService
    {
        private OrderDAO _orderDAO;
        public OrderService()
        {
            _orderDAO = new OrderDAO();
        }

        public async Task<Order> Get(int id)
        {
            return await _orderDAO.Get(id);
        }

        public IEnumerable<Order> OrdersPerUser(string userId)
        {
            return _orderDAO.OrdersPerUser(userId);
        }

        public void Insert(Order entity)
        {
            _orderDAO.Insert(entity);
        }

    }
}
