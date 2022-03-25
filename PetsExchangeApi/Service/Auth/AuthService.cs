using AuthApiClient;
using AuthApiContract;
using PetsExchangeApi.DTO;
using PetsExchangeApi.Extensions;

namespace PetsExchangeApi.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthApiClient _authApiClient;

        public AuthService(IAuthApiClient authApiClient)
        {
            _authApiClient = authApiClient;
        }

        public async Task<AuthResultDto?> Login(UserAuthDto userDto)
        {
            if (userDto.IsValid)
            {
                var authResult = (await _authApiClient.Login(
                    new LoginApiContract(userDto.Email, userDto.Password)
                    )).Body;

                return authResult.ToDto();
            }

            return null;
        }

        public Task<AuthResultDto> RefreshToken(AuthResultDto user)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResultDto> Register(UserAuthDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
