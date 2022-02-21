using UserApiDto;

namespace UserApi.Service
{
    public interface IUserService
    {
        UserContract Get(Guid uniqueId);
        UserContract Add(UserContract user);
        UserContract Update(UserContract user);
        UserContract Delete(Guid uniqueId);
    }
}
