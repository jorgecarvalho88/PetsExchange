using DataExtension;

namespace PetApi.Infrastructure.Pet
{
    public interface IPetRepository : IRepositoryBase
    {
        Model.Pet Get(Guid petId);
        List<Model.Pet> GetByOwner(Guid ownerId);
        Model.Breed GetBreed(Guid breedId);
    }
}
