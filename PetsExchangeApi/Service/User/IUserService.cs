using PetsExchangeApi.DTO;

namespace PetsExchangeApi.Service.User
{
    public interface IUserService
    {
        Task<UserDto> Get(Guid uniqueId);
        Task<UserDto> Get(string email);
        Task<UserDto> Add(UserDto user);
        Task<UserDto> Update(UserDto user);
        Task<UserDto> Delete(Guid uniqueId);
    }
}
