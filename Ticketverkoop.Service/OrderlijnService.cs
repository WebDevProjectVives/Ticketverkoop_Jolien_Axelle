﻿using System;
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

        public void Insert(Orderlijn entity)
        {
            _orderlijnDAO.Insert(entity);
        }
    }
}