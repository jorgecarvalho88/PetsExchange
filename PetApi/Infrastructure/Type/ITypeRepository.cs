using DataExtension;

namespace PetApi.Infrastructure.Type
{
    public interface ITypeRepository : IRepositoryBase
    {
        Model.Type Get(Guid uniqueId);
    }
}
