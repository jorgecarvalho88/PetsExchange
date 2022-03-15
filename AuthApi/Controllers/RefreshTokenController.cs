using ApiExtension;
using AuthApi.Service.RefreshToken;
using AuthApiContract;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RefreshTokenController : ApiControllerBase
    {
        private IRefreshTokenService _refreshTokenService;
        public RefreshTokenController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        [HttpGet]
        [Route("{jwtToken}")]
        public IActionResult Get(string jwtToken)
        {
            return Ok(_refreshTokenService.Get(jwtToken));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add(RefreshTokenApiContract refreshToken)
        {
            return Ok(_refreshTokenService.Add(refreshToken));
        }
    }
}
