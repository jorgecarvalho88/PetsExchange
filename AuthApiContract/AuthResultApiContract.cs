using Validations;

namespace AuthApiContract
{
    public class AuthResultApiContract : ValidationBase
    {
        public AuthResultApiContract()
        {

        }

        public AuthResultApiContract(string token, string refreshToken, List<string> errors)
        {
            Token = token;
            RefreshToken = refreshToken;
            Errors = errors;
        }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}