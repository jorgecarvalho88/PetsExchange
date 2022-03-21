﻿using ApiExtension;
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
        public UserController(IUserService userService)
        {
            _userService = userService;
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
