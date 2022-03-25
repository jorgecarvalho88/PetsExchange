using Validations;

namespace PetsExchangeApi.DTO
{
    public class AuthResultDto : ValidationBase
    {
        public AuthResultDto()
        {}

        public AuthResultDto(string token, string refreshToken, List<string> errors)
        {
            Token = token;
            RefreshToken = refreshToken;
            Errors = errors;
        }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
