using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspWebApi.Dto;
using AspWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AspWebApi.Controllers
{
    // token route 
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public TokenController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        // get token route 
        [HttpPost]
        public IActionResult TokenKey(TokenEmail model)
        {
            // verify user 
           var user = _userManager.Users.FirstOrDefault(x=>x.Email == model.Email);
            if (user == null)
            {
                return NotFound();
            }
            // claims 
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email , user.Email)
            };
            // key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor

            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,

            };
            // token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(securityTokenDescriptor);
            // return token
            return Ok(new {token = tokenHandler.WriteToken(token) });
          
        }
    }
}
