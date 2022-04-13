using PetApi.Infrastructure.Type;
using PetApiContract;

namespace PetApi.Service.Type
{
    public class TypeService : ITypeService
    {
        private ITypeRepository _typeRepository;
        public TypeService(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        public TypeContract Get(Guid uniqueId)
        {
            return ConvertToApiContractCart(_typeRepository.Get(uniqueId));
        }

        #region Methods
        private TypeContract ConvertToApiContractCart(Model.Type type)
        {
            if (type == null)
            {
                return new TypeContract() { Errors = new List<string>() { "Type of pet not found" } };
            }
            return new TypeContract(type.UniqueId, type.Name, type.Errors);
        }
        #endregion
    }
}
