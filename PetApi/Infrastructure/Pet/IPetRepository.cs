using DataExtension;

namespace PetApi.Infrastructure.Pet
{
    public interface IPetRepository : IRepositoryBase
    {
        Model.Pet Get(Guid petId);
    }
}
