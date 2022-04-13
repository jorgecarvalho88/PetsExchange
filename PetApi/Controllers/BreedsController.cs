using ApiExtension;
using Microsoft.AspNetCore.Mvc;
using PetApiContract;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreedsController : ApiControllerBase
    {
        private IBreedService _breedService;
        public BreedsController(IBreedService breedService)
        {
            _breedService = breedService;
        }

        [HttpGet]
        [Route("id/{uniqueId}")]
        public IActionResult Get(Guid uniqueId)
        {
            return Ok(_breedService.Get(uniqueId));
        }

        [HttpGet]
        [Route("name/{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(_breedService.Get(name));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add(BreedContract breed)
        {
            return Ok(_breedService.Add(breed));
        }
    }
}
