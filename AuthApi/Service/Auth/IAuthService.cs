using AuthApiContract;

namespace AuthApi.Service.Auth
{
    public interface IAuthService
    {
        AuthResultApiContract Register(RegistrationApiContract user);
        AuthResultApiContract Login(LoginApiContract user);
        AuthResultApiContract RefreshToken(AuthResultApiContract authResult);
    }
}
