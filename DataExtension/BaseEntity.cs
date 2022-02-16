using Validations;

namespace DataExtension
{
    public class BaseEntity : ValidationBase
    {
        public int Id { get; protected set; }
        public Guid UniqueId { get; protected set; }

        public BaseEntity()
        {
            UniqueId = Guid.NewGuid();
        }
    }
}