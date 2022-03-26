using DataExtension;

namespace PetApi.Infrastructure.Breed
{
    public interface IBreedRepository : IRepositoryBase
    {
        Model.Breed Get(Guid breedId);
        Model.Breed Get(string breedName);
    }
}
