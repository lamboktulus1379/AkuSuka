using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AkuSuka.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            if (user.Username == "lamboktulus1379" && user.Password == "gra0307")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("I was wandering what if the best I could do for may day to day"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken
                (
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { token = tokenString });
            } else
            {
                return Unauthorized();
            }
        }
    }
}
