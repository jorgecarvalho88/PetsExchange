using ClientExtension;
using UserApiContract;

namespace UserApiClient
{
    public interface IUserApiClient
    {
        Task<ResponseResult<UserContract>> GetUser(Guid userId);
        Task<ResponseResult<UserContract>> GetUser(string email);
        Task<ResponseResult<UserContract>> CreateUser(UserContract user);
        Task<ResponseResult<UserContract>> UpdateUser(UserContract user);
        Task<ResponseResult<UserContract>> DeleteUser(Guid userId);
    }
}