using PetsExchangeApi.DTO;

namespace PetsExchangeApi.Service.User
{
    public interface IUserService
    {
        Task<UserDto> Get(Guid uniqueId);
    }
}
