using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorErpUserManagement.API.Interfaces;
using RazorErpUserManagement.API.Models;

namespace RazorErpUserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;

        public LoginController(IConfiguration configuration, ILoginService loginService)
        {
            _configuration = configuration;
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = _loginService.Authenticate(userLogin);

            if (user != null)
            {
                var token = _loginService.GenerateToken(user);
                return Ok(token);
            }

            return NotFound("User not found.");
        }
    }
}
