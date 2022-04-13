using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validations;

namespace PetApiContract
{
    public class PetContract : ValidationBase
    {
        public PetContract()
        {}

        public PetContract(Guid uniqueId, Guid breedId, Guid ownerId, string name, string sex, decimal weight, bool microChiped,
            bool neutered, bool trained, bool friendlyAroundDogs, bool friendlyAroudCats, string description, string? observations,
            List<string> erros)
        {
            UniqueId = uniqueId;
            BreedId = breedId;
            Owner = ownerId;
            Name = name;
            Sex = sex;
            Weight = weight;
            MicroChiped = microChiped;
            Neutered = neutered;
            Trained = trained;
            FriendlyAroundDogs = friendlyAroundDogs;
            FriendlyAroundCats = friendlyAroudCats;
            Description = description;
            Observations = observations;
            Errors = erros;
        }

        public Guid UniqueId { get; set; }
        public Guid BreedId { get; set; }
        public Guid Owner { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public decimal Weight { get; set; }
        public bool MicroChiped { get; set; }
        public bool Neutered { get; set; }
        public bool Trained { get; set; }
        public bool FriendlyAroundDogs { get; set; }
        public bool FriendlyAroundCats { get; set; }
        public string Description { get; set; }
        public string? Observations { get; set; }
    }
}
