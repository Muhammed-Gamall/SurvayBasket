using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurvayBasket2026.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService , IOptions<JwtOptions> jwtOptions) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        [HttpPost]
        public async Task<IActionResult> AuthenticationAsync(LoginRequest Request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetTokenAsync(Request.Email, Request.Password, cancellationToken);

            return result is null ? BadRequest("Email or Password are Wrong") : Ok(result);
        }


        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest Request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetRefreshTokenAsync(Request.Token, Request.RefreshToken, cancellationToken);

            return result is null ? BadRequest("Invalid token") : Ok(result);
        }

        
        //[HttpGet]
        //public IActionResult test() { 
           
        //    return Ok(_jwtOptions.issuer);
        //}

    }
}
