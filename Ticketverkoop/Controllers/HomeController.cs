using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Models;
using Ticketverkoop.ViewModel;
using Ticketverkoop.Util.Mail;

namespace Ticketverkoop.Controllers
{
    public class HomeController : Controller
    {
        private readonly Util.Mail.IEmailSender _emailSender;

        public HomeController(Util.Mail.IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(SendMailVM sendMailVM)
        {

            _emailSender.SendEmailAsync(sendMailVM.FromEmail, "contact pagina", sendMailVM.Message);
            ModelState.Clear();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
