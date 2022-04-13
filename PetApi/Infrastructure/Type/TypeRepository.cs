using DataExtension;

namespace PetApi.Infrastructure.Type
{
    public class TypeRepository : RepositoryBase, ITypeRepository
    {
        public TypeRepository(PetDbContext context) : base(context)
        {

        }

        public IQueryable<Model.Type> Get()
        {
            return base.GetQueryable<Model.Type>();
        }

        public Model.Type Get(Guid uniqueId)
        {
            return Get().Where(t => t.UniqueId == uniqueId).FirstOrDefault();
        }
    }
}
