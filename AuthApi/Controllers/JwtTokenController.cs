using ApiExtension;
using AuthApi.Service.JwtToken;
using AuthApiContract;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JwtTokenController : ApiControllerBase
    {
        private IJwtTokenService _jwtTokenService;
        public JwtTokenController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Generate(JwtUserContract user)
        {
            return Ok(_jwtTokenService.GenerateJwtToken(user));
        }

    }
}
