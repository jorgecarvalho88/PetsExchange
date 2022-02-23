using ApiExtension;
using Microsoft.AspNetCore.Mvc;
using PetsExchangeApi.Service.User;

namespace PetsExchangeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ApiControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("/{uniqueId}")]
        public async Task<IActionResult> Get(Guid uniqueId)
        {
            return Ok(await _userService.Get(uniqueId));
        }
    }
}
