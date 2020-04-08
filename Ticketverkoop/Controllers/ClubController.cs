using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticketverkoop.Service;
using Ticketverkoop.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ticketverkoop.Controllers
{
    public class ClubController : Controller
    {
        private ClubService _clubService;
        private StadionService _stadionService;
        private readonly IMapper _mapper;

        public ClubController(IMapper mapper)
        {
            _clubService = new ClubService();
            _mapper = mapper;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = _clubService.GetAll();
            List<ClubVM> listVM = _mapper.Map<List<ClubVM>>(list);
            return View(listVM);
        }
    }
}
