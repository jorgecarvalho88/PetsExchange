using Validations;

namespace PetsExchangeApi.DTO
{
    public class UserAuthDto : ValidationBase
    {
        public UserAuthDto()
        {}

        public UserAuthDto(string email, string password, List<string> errors)
        {
            Email = email;
            Password = password;
            Errors = errors;
        }

        public string Email { get; set; }
        public string Password { get; set; }


    }
}
