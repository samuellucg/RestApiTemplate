using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Services.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using Domain.Model;

namespace ApiRestTemplate.Controllers
{
    [Route("api/[controller]")] // Define a rota da controller
    [ApiController] // Define que a controller é uma API
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService usersService)
        {
            _userService = usersService;
        }

        [HttpGet(nameof(Users))]
        [SwaggerOperation(Summary = "Get all users", Description = "Get all users")]

        public async Task<IActionResult> Users()
        {
            try
            {
                List<User> listUsers = await _userService.GetUsers();
                return View(listUsers);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
    }
}
