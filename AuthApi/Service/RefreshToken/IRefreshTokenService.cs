using AuthApiContract;

namespace AuthApi.Service.RefreshToken
{
    public interface IRefreshTokenService
    {
        RefreshTokenApiContract Get(string refreshToken);
        RefreshTokenApiContract Add(RefreshTokenApiContract refreshToken);
    }
}
