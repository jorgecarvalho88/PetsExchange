using DataExtension;

namespace PetApi.Infrastructure.Type
{
    public class TypeRepository : RepositoryBase, ITypeRepository
    {
        public TypeRepository(PetDbContext context) : base(context)
        {

        }

        public Model.Type Get(Guid uniqueId)
        {
            throw new NotImplementedException();
        }
    }
}
