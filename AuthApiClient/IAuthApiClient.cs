using AuthApiContract;
using ClientExtension;

namespace AuthApiClient
{
    public interface IAuthApiClient
    {
        Task<ResponseResult<AuthResultApiContract>> Register(RegistrationApiContract userRegistrationDto);
        Task<ResponseResult<AuthResultApiContract>> Login(LoginApiContract userLoginDto);
        Task<ResponseResult<AuthResultApiContract>> RefreshToken(AuthResultApiContract authResultDto);
    }
}
