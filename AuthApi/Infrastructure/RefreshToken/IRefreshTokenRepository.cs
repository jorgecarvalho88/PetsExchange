using DataExtension;

namespace AuthApi.Infrastructure.RefreshToken
{
    public interface IRefreshTokenRepository : IRepositoryBase
    {
        Model.RefreshToken GetByRefreshToken(string refreshToken);
    }
}
