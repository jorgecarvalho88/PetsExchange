using Microsoft.AspNetCore.Mvc;
using PetApi.Service.Type;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypesController : Controller
    {
        private ITypeService _typeService;
        public TypesController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get(Guid uniqueId)
        {
            return Ok(_typeService.Get(uniqueId));
        }
    }
}
