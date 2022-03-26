using DataExtension;

namespace PetApi.Model
{
    public class Type : BaseEntity
    {
        public Type()
        {

        }

        public Type(string name)
        {
            SetType(name);
        }

        private void SetType(string type)
        {
            Name = type;
        }

        public string Name { get; protected set; }
        public virtual ICollection<Breed> Breeds { get; protected set; }
    }
}
