using AuthApiContract;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Service.RefreshToken
{
    public interface IRefreshTokenService
    {
        AuthResultApiContract GenerateJwtToken(IdentityUser user);
        AuthResultApiContract VerifyRefreshToken(AuthResultApiContract authResult);
    }
}
