﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace Ticketverkoop.Controllers
{
    public class WeerController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

    }
}