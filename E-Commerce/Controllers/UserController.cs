using E_Commerce.Application.Service;
using E_Commerce.DTOs.User;
using E_Commerce.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _UserService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            if (ModelState.IsValid)
            {
                var usr = await _UserService.LoginAsync(userDto);

                if (usr.IsSuccess == true)
                {
                    var Claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Email, userDto.Email)
                    };


                    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"]));

                    JwtSecurityToken token = new JwtSecurityToken(
                        issuer: _configuration["jwt:Issuer"],
                        audience: _configuration["jwt:Audiences"],
                        expires: DateTime.Now.AddDays(5),
                        claims: Claims,
                        signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.Aes128CbcHmacSha256)
                        );

                    var StringToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new { StringToken, Expire = token.ValidTo });
                }
                return Unauthorized(ModelState);

            }
            return Unauthorized(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddOrEditUserDto UserDto)
        {
            if(UserDto is not null && !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user =  await  _UserService.CreateAsync(UserDto);
            return Ok(user);
        }

       
    }
}
