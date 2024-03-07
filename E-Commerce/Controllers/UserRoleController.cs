using E_Commerce.Application.Service;
using E_Commerce.DTOs.Role;
using E_Commerce.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _UserRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _UserRoleService = userRoleService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(AddRoleForUserDto AddRoleForUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var UserRole = await _UserRoleService.CreateAsync(AddRoleForUserDto);
            return Ok(UserRole);
        }
    }
}
