using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Services.Interface;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Application.Services.Token.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ApiRestTemplate.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        //private readonly ILogger _logger;

        private readonly IUserService _userService;

        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            //_logger = logger;
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost(nameof(LoginUser))]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(User userToLogin)
        {
            try
            {
                var response = await _userService.LoginUser(userToLogin);
                if (response is null)
                    return BadRequest();
                return Ok(new { token = response.AccessToken });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpPost(nameof(LoginUser))]
        //[AllowAnonymous]
        //public async Task<IActionResult> LoginUser([FromBody] User user)
        //{
        //    try
        //    {
        //        //_logger.LogInformation(nameof(LoginUser));
        //        var response = await _userService.GetUserByEmail(user);
        //        if (response)
        //        {
        //            var token = _tokenService.GenerateToken(user);

        //            //var claims = new List<Claim>
        //            //{
        //            //    new Claim(ClaimTypes.Name, user.Name),
        //            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //            //    new Claim(ClaimTypes.Email, user.Email)
        //            //};

        //            //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //            //var authProperties = new AuthenticationProperties
        //            //{
        //            //    IsPersistent = true,
        //            //    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(5)
        //            //};

                    
        //            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        //            //return Ok(response);
        //            return Ok(new
        //            {
        //                token,
        //                user.Name,
        //                user.Email,
        //                user.Id
        //            });
        //        }
        //        return BadRequest("Invalid email or password");
        //    }
        //    catch (Exception e)
        //    {
        //        //_logger.LogError(string.Format($"{nameof(LoginUser)} {e.Message}"));
        //        return BadRequest(e.Message);
        //    }
        //}

    }
}
