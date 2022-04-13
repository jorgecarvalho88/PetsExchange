using PetApi.Infrastructure.Breed;
using PetApiContract;

namespace PetApi.Service.Breed
{
    public class BreedService : IBreedService
    {
        private IBreedRepository _breedRepository;
        public BreedService(IBreedRepository breedRepository)
        {
            _breedRepository = breedRepository;
        }

        public BreedContract Get(Guid uniqueId)
        {
            return ConvertToApiContractCart(_breedRepository.Get(uniqueId));
        }

        public BreedContract Get(string name)
        {
            return ConvertToApiContractCart(_breedRepository.Get(name));
        }

        public BreedContract Add(BreedContract breed)
        {
            _breedRepository.BeginTransaction();

            var existingBreed = _breedRepository.Get(breed.UniqueId);
            if(existingBreed is not null)
            {
                _breedRepository.RollBackTransaction();
                breed.Errors.Add("Breed already exists");
                return breed;
            }

            var type = _breedRepository.GetType(breed.TypeId);
            var newBreed = new Model.Breed(breed.Name, type);

            if (newBreed.IsValid)
            {
                _breedRepository.Create(newBreed);
                _breedRepository.Commit();
                _breedRepository.CommitTransaction();

                breed.UniqueId = newBreed.UniqueId;
            }
            else
            {
                _breedRepository.RollBackTransaction();
                breed.Errors.AddRange(newBreed.Errors);
            }
            return breed;
        }


        #region Methods
        private BreedContract ConvertToApiContractCart(Model.Breed breed)
        {
            if (breed == null)
            {
                return new BreedContract() { Errors = new List<string>() { "Breed not found" } };
            }
            return new BreedContract(
                breed.UniqueId, breed.Name, breed.PetType.UniqueId, breed.Errors
                );
        }
        #endregion

    }
}
