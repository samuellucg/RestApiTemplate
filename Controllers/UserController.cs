using Microsoft.AspNetCore.Mvc;

namespace ApiRestTemplate.Controllers
{
    [Route("api/[controller]")] // Define a rota da controller
    [ApiController] // Define que a controller é uma API
    public class UserController : Controller
    {
        [HttpGet(nameof(Users))]
        public IActionResult Users()
        {
            return View();
        }
    }
}
