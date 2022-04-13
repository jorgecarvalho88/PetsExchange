using PetApiContract;

namespace PetApi.Service.Type
{
    public interface ITypeService
    {
        TypeContract Get(Guid uniqueId);
        //TypeContract Add(TypeContract type);
        //TypeContract Update(TypeContract type);
        //TypeContract Delete(Guid uniqueId);
    }
}
