using Validations;

namespace PetsExchangeApi.DTO
{
    public class UserDto : ValidationBase
    {
        public UserDto()
        {
        }

        public UserDto(Guid uniqueId, string name, string email, List<string> errors)
        {
            Name = name;
            Email = email;
            Errors = errors;
            UniqueId = uniqueId;
        }

        public Guid UniqueId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
