using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AkuSuka.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ITokenService _tokenService;

        public TokenController(IRepositoryWrapper repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenApi tokenApi)
        {
            if (tokenApi is null)
            {
                return BadRequest("Invalid client request");
            }
            Console.WriteLine(tokenApi.RefreshToken, tokenApi.AccessToken);

            string accessToken = tokenApi.AccessToken;
            string refreshToken = tokenApi.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;

            var user = _repository.User.GetUserByUsername(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            _repository.User.UpdateUser(user);
            _repository.Save();

            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("rovoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var user = _repository.User.GetUserByUsername(username);

            if (user == null)
                return BadRequest();

            user.RefreshToken = null;

            _repository.Save();

            return NoContent();
        }



    }

}
