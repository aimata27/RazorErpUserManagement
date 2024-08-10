using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RazorErpUserManagement.API.Interfaces;
using RazorErpUserManagement.API.Models;

namespace RazorErpUserManagement.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        public AdminController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userManagementService.GetAllUsersAsync();

            if (users != null)
                return Ok(users);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDetails userDetails)
        {
            var isCompanyExists = await _userManagementService.IsCompanyExistsAsync(userDetails.CompanyId);

            if (isCompanyExists)
            {
                _userManagementService.CreateUserAsync(userDetails);

                return Ok("User created.");
            }

            return BadRequest("Company does not exists.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDetails userDetails)
        {
            _userManagementService.UpdateUserAsync(userDetails, id);

            return Ok("User updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _userManagementService.DeleteUserAsync(id);

            return Ok("User deleted.");
        }
    }
}
