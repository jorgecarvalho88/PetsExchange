using ApiExtension;
using AuthApi.Service.Auth;
using AuthApiContract;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
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
        public IActionResult Register(RegistrationApiContract user)
        {
            return Ok(_authService.Register(user));
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginApiContract user)
        {
            return Ok(_authService.Login(user));
        }

        [HttpPost]
        [Route("RefreshToken")]
        public IActionResult RefreshToken(AuthResultApiContract authResult)
        {
            return Ok(_authService.RefreshToken(authResult));
        }

    }
}
