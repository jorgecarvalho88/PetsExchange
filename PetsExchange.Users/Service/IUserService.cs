using UserApiDto;

namespace UserApi.Service
{
    public interface IUserService
    {
        UserDto Get(Guid uniqueId);
        UserDto Add(UserDto user);
        UserDto Update(UserDto user);
        UserDto Delete(Guid uniqueId);
    }
}
