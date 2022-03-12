using Validations;

namespace PetsExchangeApi.DTO
{
    public class UserDto : ValidationBase
    {
        public UserDto()
        {
        }

        public UserDto(Guid uniqueId, string firstName, string lastName, string password, string email, List<string> errors)
        {
            FirstName = firstName;
            LastName = firstName;
            Password = password;
            Email = email;
            Errors = errors;
            UniqueId = uniqueId;
        }

        public Guid UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
