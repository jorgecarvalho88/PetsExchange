using DataExtension;

namespace PetApi.Infrastructure.Breed
{
    public class BreedRepository : RepositoryBase, IBreedRepository
    {
        public BreedRepository(PetDbContext context) : base(context)
        {

        }

        public Model.Breed Get(Guid breedId)
        {
            throw new NotImplementedException();
        }

        public Model.Breed Get(string breedName)
        {
            throw new NotImplementedException();
        }
    }
}
