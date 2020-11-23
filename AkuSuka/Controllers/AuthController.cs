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
using Repository;
using Contracts;

namespace AkuSuka.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ITokenService _tokenService;

        public AuthController(IRepositoryWrapper repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User dbUser)
        {
            if (dbUser == null)
            {
                return BadRequest("Invalid client request");
            }
            User user = _repository.User.CheckUser(dbUser.Username, dbUser.Password);   
            if (user == null)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbUser.Username),
                new Claim(ClaimTypes.Role, "Owner")
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.Now.AddDays(7);

            _repository.User.UpdateUser(user);

            _repository.Save();

            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
