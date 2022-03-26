using DataExtension;

namespace PetApi.Model
{
    public class Breed : BaseEntity
    {
        public Breed()
        {

        }

        public Breed(string name, Type petType)
        {
            SetName(name);
            SetPetType(petType);
        }


        private void SetName(string name)
        {
            Name = name;
        }

        private void SetPetType(Type petType)
        {
            PetType = petType;
        }

        public string Name { get; protected set; }
        public Type PetType { get; protected set; }
        public virtual ICollection<Pet> Pets { get; protected set; }
    }
}
