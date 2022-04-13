using DataExtension;
using Validations;

namespace PetApi.Model
{
    public class Pet : BaseEntity
    {
        public Pet()
        {}

        public Pet(
            Breed breed,
            Guid owner,
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
            SetBreed(breed);
            SetOwner(owner);
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

        public void SetBreed(Breed breed)
        {
            if(breed is null)
            {
                this.Errors.Add("Breed is not valid");
            }
            Breed = breed;
        }
        public void SetOwner(Guid owner)
        {
            if (owner == new Guid())
            {
                this.Errors.Add("Owner must be valid");
            }

            Owner = owner;
        }
        
        public void SetName(string name)
        {
            ValidateIsNullOrWhiteSpace(name, "name");
            Name = name;
        }
        
        public void SetSex(string sex)
        {
            ValidateIsNullOrWhiteSpace(sex, "sex");
            ValidateSexConstraint(sex);
            Sex = sex;
        }
        
        public void SetWeight(decimal weight)
        {
            if (weight < 0) this.Errors.Add("Weigth cant be negative");
            Weight = weight;
        }
        
        public void SetMicroShiped(bool microshiped)
        {
            MicroChiped = microshiped;
        }
        
        public void SetNeutered(bool neutered)
        {
            Neutered = neutered;
        }
        
        public void SetTrained(bool trained)
        {
            Trained = trained;
        }
        
        public void SetFriendlyAroundDogs(bool friendlyAroundDogs)
        {
            FriendlyAroundDogs = friendlyAroundDogs;
        }
        
        public void SetFriendlyAroundCats(bool friendlyAroundCats)
        {
            FriendlyAroundCats = friendlyAroundCats;
        }
        
        public void SetDescription(string description)
        {
            ValidateIsNullOrWhiteSpace(description, "description");
            ValidateLength(description, "description", 500);
            Description = description;
        }
        
        public void SetObservations(string? observations)
        {
            if(observations is not null) ValidateLength(observations, "observations", 500);
            Observations = observations;
        }

        public virtual Breed Breed { get; protected set; }
        public Guid Owner { get; protected set; }
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
