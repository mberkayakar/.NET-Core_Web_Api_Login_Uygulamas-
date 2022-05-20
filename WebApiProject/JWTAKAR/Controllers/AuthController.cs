using JWTAKAR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTAKAR.Controllers
{



    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;


        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
                _userManager = userManager;
                _signInManager = signInManager;
                _configuration = configuration;
        }


        [HttpPost("login")]
        
        public async Task<IActionResult> Login(UserLoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user == null)
                {
                    return BadRequest("Böyle bi aga bulunamadı bro tekrar dene");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                return result.Succeeded ? Ok(new { token = GenerateJWtToken(user) }) : Unauthorized();

            }
        }

        private string GenerateJWtToken(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("AKARAKARAKARAKAR123");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)

                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }



}
}
