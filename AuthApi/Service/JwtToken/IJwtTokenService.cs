using AuthApiContract;

namespace AuthApi.Service.JwtToken
{
    public interface IJwtTokenService
    {
        JwtUserContract GenerateJwtToken(JwtUserContract user);
    }
}
