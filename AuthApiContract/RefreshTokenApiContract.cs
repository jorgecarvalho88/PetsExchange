using Validations;

namespace AuthApiContract
{
    public class RefreshTokenApiContract : ValidationBase
    {
        public RefreshTokenApiContract()
        {

        }

        public RefreshTokenApiContract(string jwtId, string refreshToken, Guid userUniqueId, List<string> errors)
        {
            JwtId = jwtId;
            RefreshToken = refreshToken;
            Errors = errors;
            UserUniqueId = userUniqueId;
        }

        public string JwtId { get; set; }
        public string RefreshToken { get; set; }
        public Guid UserUniqueId { get; set; }
    }
}