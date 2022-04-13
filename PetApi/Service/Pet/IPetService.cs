using PetApiContract;

namespace PetApi.Service.Pet
{
    public interface IPetService
    {
        PetContract Get(Guid uniqueId);
        List<PetContract> GetByOwner(Guid ownerId);
        PetContract Add(PetContract pet);
        PetContract Update(PetContract pet);
        PetContract Delete(Guid uniqueId);
    }
}
