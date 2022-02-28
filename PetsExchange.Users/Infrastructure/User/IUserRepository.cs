using DataExtension;

namespace UserApi.Infrastructure.User
{
    public interface IUserRepository : IRepositoryBase
    {
        Model.User Get(Guid userId);
        Model.User Get(string email);
    }
}
