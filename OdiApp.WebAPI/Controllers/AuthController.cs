
using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Services.Token;
using OdiApp.DTOs.IdentityDTOs;
using OdiApp.DTOs.TokenDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("auth")]
    [ApiController]
    [SwaggerControllerOrder(1)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("user-token")]
        public async Task<IActionResult> GetUserToken(LoginDTO login)
        {

            return Ok(await _authenticationService.CreateUserTokenAsync(login));
        }
        [HttpPost("client-token")]
        public IActionResult GetClientToken(ClientLoginDTO login)
        {

            return Ok(_authenticationService.CreateClientTokenAsync(login));
        }

        [HttpPost("refresh-token-sil")]
        public IActionResult RefreshTokenSil(RefreshTokenDTO refreshToken)
        {

            return Ok(_authenticationService.RevokeRefreshTokenAsync(refreshToken.ToString()));
        }

        [HttpPost("token-by-refresh-token")]
        public IActionResult GetTokenByRefreshToken(RefreshTokenDTO refreshToken)
        {
            return Ok(_authenticationService.CreateTokenByRefreshTokenAsync(refreshToken.ToString()));
        }
    }
}
