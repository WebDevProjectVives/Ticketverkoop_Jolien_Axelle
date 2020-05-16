using System;
using System.Collections.Generic;
using System.Text;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Repository;

namespace Ticketverkoop.Service
{
    public class OrderlijnService
    {
        private OrderlijnDAO _orderlijnDAO;
        public OrderlijnService()
        {
            _orderlijnDAO = new OrderlijnDAO();
        }

        public Orderlijn GetOrderlijnVanTicket(int? id)
        {
            return _orderlijnDAO.GetOrderlijnVanTicket(id);
        }

        public List<Orderlijn> OrderlijnPerOrder(int orderId)
        {
            return _orderlijnDAO.OrderlijnPerOrder(orderId);
        }
        public void Insert(Orderlijn entity)
        {
            _orderlijnDAO.Insert(entity);
        }
        public void Delete(Orderlijn entity)
        {
            _orderlijnDAO.Delete(entity);
        }
    }
}
