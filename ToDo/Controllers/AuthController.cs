using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Interfaces;
using ToDo.ViewModels;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]UserViewModel userParam)
        {
            var token = _userService.Authenticate(userParam.Username, userParam.Password);

            if (token == null)
                return BadRequest("Username or password is incorrect");

            return Ok(token);
        }
    }
}