using PetApi.Infrastructure.Pet;
using PetApiContract;

namespace PetApi.Service.Pet
{
    public class PetService : IPetService
    {
        private IPetRepository _petRepository;
        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public PetContract Get(Guid uniqueId)
        {
            return ConvertToApiContractCart(_petRepository.Get(uniqueId));
        }

        public List<PetContract> GetByOwner(Guid ownerId)
        {
            var petsContract = new List<PetContract>();
            var pets = _petRepository.GetByOwner(ownerId);

            foreach (var pet in pets)
            {
                petsContract.Add(ConvertToApiContractCart(pet));
            }

            return petsContract;
        }

        public PetContract Add(PetContract pet)
        {
            _petRepository.BeginTransaction();

            var existingPet = _petRepository.Get(pet.UniqueId);
            if(existingPet is not null)
            {
                _petRepository.RollBackTransaction();
                pet.Errors.Add("Pet already exists");
                return pet;
            }

            var breed = _petRepository.GetBreed(pet.BreedId);

            var newPet = new Model.Pet(breed, pet.Owner, pet.Name, pet.Sex, pet.Weight, pet.MicroChiped, pet.Neutered, pet.Trained, pet.FriendlyAroundDogs, pet.FriendlyAroundCats, pet.Description, pet.Observations);

            if(newPet.IsValid)
            {
                _petRepository.Create(newPet);
                _petRepository.Commit();
                _petRepository.CommitTransaction();

                pet.UniqueId = newPet.UniqueId;
            }
            else
            {
                _petRepository.RollBackTransaction();
                pet.Errors.AddRange(newPet.Errors);
            }
            return pet;
        }

        public PetContract Update(PetContract pet)
        {
            _petRepository.BeginTransaction();

            var existingPet = _petRepository.Get(pet.UniqueId);
            if(existingPet is null)
            {
                _petRepository.RollBackTransaction();
                pet.Errors.Add("Pet doesn't exists");
                return pet;
            }

            var breed = _petRepository.GetBreed(pet.BreedId);

            existingPet.SetBreed(breed);
            existingPet.SetOwner(pet.Owner);
            existingPet.SetName(pet.Name);
            existingPet.SetSex(pet.Sex);
            existingPet.SetWeight(pet.Weight);
            existingPet.SetMicroShiped(pet.MicroChiped);
            existingPet.SetNeutered(pet.Neutered);
            existingPet.SetTrained(pet.Trained);
            existingPet.SetFriendlyAroundDogs(pet.FriendlyAroundDogs);
            existingPet.SetFriendlyAroundCats(pet.FriendlyAroundCats);
            existingPet.SetDescription(pet.Description);
            existingPet.SetObservations(pet.Observations);

            if(!existingPet.IsValid)
            {
                _petRepository.RollBackTransaction();
                pet.Errors.AddRange(existingPet.Errors);
                return pet;
            }

            _petRepository.Update(existingPet);
            _petRepository.Commit();
            _petRepository.CommitTransaction();
            return pet;
        }

        public PetContract Delete(Guid uniqueId)
        {
            _petRepository.BeginTransaction();
            
            var existingPet = _petRepository.Get(uniqueId);
            if(existingPet is null)
            {
                _petRepository.RollBackTransaction();
                return new PetContract()
                {
                    Errors = new List<string>() { "Invalid UniqueId" }
                };
            }

            var pet = ConvertToApiContractCart(existingPet);

            _petRepository.Delete(existingPet);
            _petRepository.Commit();
            _petRepository.CommitTransaction();
            return pet;
        }

        #region Methods
        private PetContract ConvertToApiContractCart(Model.Pet pet)
        {
            if (pet == null)
            {
                return new PetContract() { Errors = new List<string>() { "Pet not found" } };
            }
            return new PetContract(
                pet.UniqueId,
                pet.Breed.UniqueId,
                pet.Owner,
                pet.Name,
                pet.Sex,
                pet.Weight,
                pet.MicroChiped,
                pet.Neutered,
                pet.Trained,
                pet.FriendlyAroundDogs,
                pet.FriendlyAroundDogs,
                pet.Description,
                pet.Observations,
                pet.Errors
                );
        }

        
        #endregion

    }
}
