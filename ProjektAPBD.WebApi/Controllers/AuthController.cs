using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektAPBD.WebApi.Interfaces;

namespace ProjektAPBD.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var (accessToken, refreshToken) = await _authRepository.Login(username, password);
            return Ok(new { 
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            var (accessToken, newRefreshToken) = await _authRepository.Refresh(refreshToken);
            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
