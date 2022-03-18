using ApiExtension;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserApi.Service;
using UserApiContract;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ApiControllerBase
    {
        private IUserService _userService;
        private UserManager<Model.User> _userManager;
        public UserController(IUserService userService, UserManager<Model.User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("{uniqueId}")]
        public IActionResult Get(Guid uniqueId)
        {
            return Ok(_userService.Get(uniqueId));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add(UserContract user)
        {
            return Ok(_userService.Add(user));
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update(UserContract user)
        {
            return Ok(_userService.Update(user));
        }

        [HttpDelete]
        [Route("")]
        public IActionResult Delete(Guid uniqueId)
        {
            return Ok(_userService.Delete(uniqueId));
        }
    }
}
