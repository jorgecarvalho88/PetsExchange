using AuthApiContract;
using PetsExchangeApi.DTO;

namespace PetsExchangeApi.Extensions
{
    public static class AuthExtension
    {
        public static AuthResultDto? ToDto(this AuthResultApiContract authResult)
        {
            if (authResult is null)
            {
                return null;
            }

            return new AuthResultDto(authResult.Token, authResult.RefreshToken, authResult.Errors);
        }
    }
}
