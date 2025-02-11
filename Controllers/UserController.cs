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
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IUserService usersService, ILogger<UserController> logger)
        {
            _userService = usersService;
            _logger = logger;
        }

        [HttpGet(nameof(Users))]
        [SwaggerOperation(Summary = "Get all users", Description = "Get all users")]

        public async Task<IActionResult> Users()
        {
            try
            {
                _logger.LogInformation(nameof(Users));
                IEnumerable<User> listUsers =  await _userService.GetUsers();
                return Ok(listUsers);
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format($"{nameof(Users)} {e.Message}"));
                return BadRequest(e.Message);
            }

        }

        [HttpPost(nameof(CreateUser))]
        [SwaggerOperation(Summary = "Create a user", Description = "Create a user")]

        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                _logger.LogInformation(nameof(CreateUser));
                if (user is null)
                {
                    return BadRequest("Invalid user");
                }

                var response = await _userService.CreateUser(user);

                if (!response)
                {
                    return BadRequest("Error creating user");
                }

                return Ok(response);
            }
            catch (Exception e)
            {

                _logger.LogError(string.Format($"{nameof(CreateUser)} {e.Message}"));
                return BadRequest(e.Message);
            }
        }
    }
}
