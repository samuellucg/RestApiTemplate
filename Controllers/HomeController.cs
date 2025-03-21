using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Privacy() // obsoleto talvez
        {
            return View();
        }



    }
}
