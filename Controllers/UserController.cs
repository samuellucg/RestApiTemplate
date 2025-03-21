using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Services.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiRestTemplate.Controllers
{
    /* 
     Responsável por fazer a comunicação entre a API e o serviço de usuário.

     Intermediador.

     */

    [Route("api/[controller]")] // Define a rota da controller
    [ApiController] // Define que a controller é uma API
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public UserController(IUserService usersService, ILogger<UserController> logger, IAccountService accountService)
        {
            _userService = usersService;
            _logger = logger;
            _accountService = accountService;
        }

        #region UserRequest
        [HttpGet(nameof(Users))]
        [SwaggerOperation(Summary = "Get all users", Description = "Get all users")]

        public async Task<IActionResult> Users()
        {
            try
            {
                _logger.LogInformation(nameof(Users));
                IEnumerable<User> listUsers = await _userService.GetUsers();
                return Ok(listUsers);
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format($"{nameof(Users)} {e.Message}"));
                return BadRequest(e.Message);
            }

        }

        [HttpGet(nameof(UserById))]
        [SwaggerOperation(Summary = "Get user by Id", Description = "Get user by id")]
        public async Task<IActionResult> UserById(int id)
        {
            try
            {
                _logger.LogInformation(nameof(UserById));
                IEnumerable<User> listUsers = await _userService.GetUsers();
                if (!listUsers.Any())
                {
                    return NoContent();
                }
                var response = listUsers.FirstOrDefault(x => x.Id == id);
                if (response is null)
                {
                    return NotFound();
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(UserById)} {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpGet(nameof(GetEmails))]
        [SwaggerOperation(Summary = "Get user emails from user.json", Description = "Get users emails from user.json")]
        public async Task<IActionResult> GetEmails()
        {
            try
            {
                var response = await _userService.GetEmails();
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetEmails)} {e.Message}");
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
                    return Conflict("Error creating user, maybe your e-mail has been registered before.");
                    //return BadRequest("Error creating user maybe your e-mail has been registered before.");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(CreateUser)} {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpPut(nameof(EditUserById))]
        [SwaggerOperation(Summary = "Edit user by id", Description = "Edit user by id")]

        public async Task<IActionResult> EditUserById(int id, User user)
        {
            try
            {
                var response = await _userService.EditUserById(id, user);
                return Ok(response);
            }

            catch (Exception e)
            {

                _logger.LogError($"{nameof(EditUserById)} {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpDelete(nameof(DeleteUserById))]
        [SwaggerOperation(Summary = "Delete user by id", Description = "Delete user by id")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            try
            {
                var response = await _userService.DeleteUserById(id);

                if (response == 204)
                {
                    return NoContent();
                }

                else if (response == 500)
                {
                    return StatusCode(500);
                }

                return Ok("User has been deleted");
            }
            catch (Exception e)
            {

                _logger.LogError($"{nameof(DeleteUserById)} {e.Message}");
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Account

        [HttpPost(nameof(LoginUser))]
        [SwaggerOperation(Summary = "Login user", Description = "To login user")]
        public async Task<IActionResult> LoginUser([FromBody] User user)
        {
            try
            {
                var response = await _accountService.ValidateUser(user);
                if (response)
                {
                    RedirectToPage("/Home/Dashboard");
                    return Ok(response);
                }

                return NotFound();

            }
            catch (Exception e)
            {
                _logger.LogError(string.Format($"{nameof(LoginUser)} {e.Message}"));
                return BadRequest(e.Message);
            }

        }

        #endregion
    }
}
