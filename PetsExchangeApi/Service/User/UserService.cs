using PetsExchangeApi.DTO;
using PetsExchangeApi.Extensions;
using UserApiClient;

namespace PetsExchangeApi.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserApiClient _userApiClient;

        public UserService(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }
        public async Task<UserDto?> Get(Guid uniqueId)
        {
            var user = (await _userApiClient.GetUser(uniqueId)).Body;

            if(user is not null)
            {
                return user.ToDto();
            }

            return null;
        }
    }
}
