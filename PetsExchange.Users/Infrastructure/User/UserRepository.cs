using DataExtension;

namespace UserApi.Infrastructure.User
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        { }

        public IQueryable<Model.User> Get()
        {
            return base.GetQueryable<Model.User>();
        }

        public Model.User? Get(string email)
        {
            return Get().Where(s => s.Email == email).FirstOrDefault();
        }

        public Model.User Get(Guid userId)
        {
            return Get().Where(s => s.UniqueId == userId).FirstOrDefault();
        }
    }
}
