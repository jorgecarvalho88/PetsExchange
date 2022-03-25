using PetsExchangeApi.DTO;

namespace PetsExchangeApi.Service.Auth
{
    public interface IAuthService
    {
        Task<AuthResultDto> Register(UserAuthDto userDto);
        Task<AuthResultDto> Login(UserAuthDto userDto);
        Task<AuthResultDto> RefreshToken(AuthResultDto user);

    }
}
