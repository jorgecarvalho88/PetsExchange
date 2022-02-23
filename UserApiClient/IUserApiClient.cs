using ClientExtension;
using UserApiContract;

namespace UserApiClient
{
    public interface IUserApiClient
    {
        Task<ResponseResult<UserContract>> GetUser(Guid userId);
    }
}