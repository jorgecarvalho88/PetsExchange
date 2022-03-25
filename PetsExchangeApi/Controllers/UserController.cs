using ApiExtension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetsExchangeApi.DTO;
using PetsExchangeApi.Service.User;

namespace PetsExchangeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ApiControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("id/{uniqueId}")]
        public async Task<IActionResult> GetById(Guid uniqueId)
        {
            return Ok(await _userService.Get(uniqueId));
        }

        [HttpGet]
        [Route("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return Ok(await _userService.Get(email));
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add(UserDto user)
        {
            return Ok(await _userService.Add(user));
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(UserDto user)
        {
            return Ok(await _userService.Update(user));
        }

        [HttpDelete]
        [Route("{uniqueId}")]
        public async Task<IActionResult> Delete(Guid uniqueId)
        {
            return Ok(await _userService.Delete(uniqueId));
        }
    }
}
