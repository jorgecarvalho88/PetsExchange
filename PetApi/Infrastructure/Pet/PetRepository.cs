using DataExtension;

namespace PetApi.Infrastructure.Pet
{
    public class PetRepository : RepositoryBase, IPetRepository
    {
        public PetRepository(PetDbContext context) : base(context)
        {

        }

        public Model.Pet Get(Guid petId)
        {
            throw new NotImplementedException();
        }
    }
}
