using System;
using System.Collections.Generic;
using System.Text;
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

        public void Insert(Order entity)
        {
            _orderDAO.Insert(entity);
        }

    }
}
