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
    public class StadionController : Controller
    {
        private StadionService _stadionService;
        private readonly IMapper _mapper;

        public StadionController(IMapper mapper)
        {
            _mapper = mapper;
            _stadionService = new StadionService();
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = _stadionService.GetAll();
            List<StadionVM> listVM = _mapper.Map<List<StadionVM>>(list);
            return View(listVM);
        }
    }
}
