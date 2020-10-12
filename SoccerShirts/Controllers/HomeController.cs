using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoccerShirts.Models;
using SoccerShirts.Repositories;
using SoccerShirts.ViewModels;

namespace SoccerShirts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICamisaRepository _camisaRepository;

        public HomeController(ICamisaRepository camisaRepository)
        {
            _camisaRepository = camisaRepository;

        }
       

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                camisasMaisVendidas = _camisaRepository.CamisaMaisVendidas
            };
            return View(homeViewModel);
        }

    }
}
