using Validations;

namespace UserApiContract
{
    public class UserContract : ValidationBase
    {
        public UserContract()
        {
        }

        public UserContract(Guid uniqueId, string firstName, string lastName, string password, string email, List<string> errors)
        {
            FirstName = firstName;
            LastName = lastName;
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