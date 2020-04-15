using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Domain.Entities;
using Ticketverkoop.Extensions;
using Ticketverkoop.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticketverkoop.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ShoppingCartVM cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            return View(cartList);
        }

        [Authorize]  // je moet ingelogd zijn om deze action aan te spreken
        [HttpPost]
        public ActionResult Payment(List<CartVM> carts)
        {

            //  opvragen ID ingelogde User
            string userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;


            try
            {
                Order order;
                foreach (CartVM cart in carts)
                {   //create order object
                    order = new Order();
                    order.UserId = userID;
                    order.OrderId = cart.Order_ID;
                    order.DateCreated = DateTime.UtcNow;
                    //order.Count = cart.Aantal;
                    //call method to save
                  }


            }
            catch (Exception ex)
            { }

            return View();
        }
    }
}
