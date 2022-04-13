using ApiExtension;
using Microsoft.AspNetCore.Mvc;
using PetApi.Model;
using PetApi.Service.Pet;
using PetApiContract;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ApiControllerBase
    {
        private IPetService _petService;
        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        [Route("id/{uniqueId}")]
        public IActionResult GetById(Guid uniqueId)
        {
            return Ok(_petService.Get(uniqueId));
        }

        [HttpGet]
        [Route("owner/{ownerId}")]
        public IActionResult GetByOwner(Guid ownerId)
        {
            return Ok(_petService.GetByOwner(ownerId));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add(PetContract pet)
        {
            return Ok(_petService.Add(pet));
        }

        [HttpPut]
        [Route("")]
        public IActionResult Update(PetContract pet)
        {
            return Ok(_petService.Update(pet));
        }

        [HttpDelete]
        [Route("{uniqueId}")]
        public IActionResult Delete(Guid uniqueId)
        {
            return Ok(_petService.Delete(uniqueId));
        }

    }
}
