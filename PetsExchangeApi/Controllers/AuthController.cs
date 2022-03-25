using ApiExtension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsExchangeApi.DTO;
using PetsExchangeApi.Service.Auth;

namespace PetsExchangeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ApiControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserAuthDto user)
        {
            return Ok(await _authService.Register(user));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserAuthDto user)
        {
            return Ok(await _authService.Login(user));
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(AuthResultDto refreshToken)
        {
            return Ok(await _authService.RefreshToken(refreshToken));
        }
       
    }
}
