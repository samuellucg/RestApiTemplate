using System.Diagnostics;
using Application.Services.Interface;
using Application.Services.Token.Interface;
using Application.ViewModel.User;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ILoginService _loginService;

        private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger, ILoginService loginService, ITokenService tokenService)
        {
            _logger = logger;
            _loginService = loginService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new VmRegisterUser());
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(VmRegisterUser user)
        {
            try
            {
                CookieOptions cookieOptions = new CookieOptions();
                var response = await _loginService.RegisterAsync(user);               
                if (response)
                {
                    return RedirectToAction("Login");
                }

                return View(new VmRegisterUser());

            }
            catch (Exception)
            {

                throw;
            }
        }
        //public IActionResult Dashboard()
        //{
        //    return View();
        //}


        public IActionResult Privacy() // obsoleto talvez
        {
            return View();
        }



    }
}
