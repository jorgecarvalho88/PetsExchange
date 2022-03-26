using DataExtension;
using Validations;

namespace PetApi.Model
{
    public class Pet : BaseEntity
    {
        //private List<string> _petTypes = new List<string>() { "dog", "cat"};

        public Pet()
        {}

        public Pet(
            //string petType,
            Breed breed,
            string name,
            string sex,
            decimal weight,
            bool microshiped,
            bool neutered,
            bool trained,
            bool friendlyAroundDogs,
            bool friendlyAroundCats,
            string description,
            string? observations)
        {
            //SetPetType(petType);
            SetBreed(breed);
            SetName(name);
            SetSex(sex);
            SetWeight(weight);
            SetMicroShiped(microshiped);
            SetNeutered(neutered);
            SetTrained(trained);
            SetFriendlyAroundDogs(friendlyAroundDogs);
            SetFriendlyAroundCats(friendlyAroundCats);
            SetDescription(description);
            SetObservations(observations);
        }

        //private void SetPetType(string petType)
        //{
        //    ValidateIsNullOrWhiteSpace(petType, "pet type");
        //    ValidatePetType(petType);

        //    PetType = petType;
        //}

        private void SetBreed(Breed breed)
        {
            Breed = breed;
        }

        private void SetName(string name)
        {
            ValidateIsNullOrWhiteSpace(name, "name");
            Name = name;
        }

        private void SetSex(string sex)
        {
            ValidateIsNullOrWhiteSpace(sex, "sex");
            ValidateSexConstraint(sex);
            Sex = sex;
        }

        private void SetWeight(decimal weight)
        {
            if (weight < 0) this.Errors.Add("Weigth cant be negative");
            Weight = weight;
        }

        private void SetMicroShiped(bool microshiped)
        {
            MicroChiped = microshiped;
        }

        private void SetNeutered(bool neutered)
        {
            Neutered = neutered;
        }

        private void SetTrained(bool trained)
        {
            Trained = trained;
        }

        private void SetFriendlyAroundDogs(bool friendlyAroundDogs)
        {
            FriendlyAroundDogs = friendlyAroundDogs;
        }

        private void SetFriendlyAroundCats(bool friendlyAroundCats)
        {
            FriendlyAroundCats = friendlyAroundCats;
        }

        private void SetDescription(string description)
        {
            ValidateIsNullOrWhiteSpace(description, "description");
            ValidateLength(description, "description", 500);
            Description = description;
        }

        private void SetObservations(string? observations)
        {
            if(observations is not null) ValidateLength(observations, "observations", 500);
            Observations = observations;
        }


        //public string PetType { get; protected set; }
        public virtual Breed Breed { get; protected set; }
        public string Name { get; protected set; }
        public string Sex { get; protected set; }
        public decimal Weight { get; protected set; }
        public bool MicroChiped { get; protected set; }
        public bool Neutered { get; protected set; }
        public bool Trained { get; protected set; }
        public bool FriendlyAroundDogs { get; protected set; }
        public bool FriendlyAroundCats { get; protected set; }
        public string Description { get; protected set; }
        public string? Observations { get; protected set; }


        #region Methods
        private void ValidateIsNullOrWhiteSpace(string value, string property)
        {
            var error = StringValidator.ValidateIsNullOrWhiteSpace(value, property);
            if (error != null)
            {
                this.Errors.Add(error);
            }
        }

        private void ValidateLength(string value, string property, int length)
        {
            var error = StringValidator.ValidateLength(value, property, length);
            if (error != null)
            {
                this.Errors.Add(error);
            }
        }

        //private void ValidatePetType(string petType)
        //{
        //    if(!_petTypes.Contains(petType))
        //    {
        //        this.Errors.Add($"Pet type can only be {String.Join(",", _petTypes)}");
        //    }
        //}

        private void ValidateSexConstraint(string sex)
        {
            if(!new List<string> { "M", "F"}.Contains(sex.ToUpper()))
            {
                this.Errors.Add($"Sex must be Male(M) or Female(F)");
            }
        }
        #endregion
    }
}
