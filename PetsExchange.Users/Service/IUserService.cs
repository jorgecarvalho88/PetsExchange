using UserApiContract;

namespace UserApi.Service
{
    public interface IUserService
    {
        UserContract Get(Guid uniqueId);
        UserContract Get(string email);
        UserContract Add(UserContract user);
        UserContract Update(UserContract user);
        UserContract Delete(Guid uniqueId);
    }
}
