using DataExtension;

namespace AuthApi.Infrastructure.RefreshToken
{
    public class RefreshTokenRepository : RepositoryBase, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AuthDbContext context) : base(context)
        { }

        public IQueryable<Model.RefreshToken> Get()
        {
            return base.GetQueryable<Model.RefreshToken>();
        }

        public Model.RefreshToken GetByRefreshToken(string refreshToken)
        {
            return Get().Where(t => t.Token == refreshToken.ToString()).FirstOrDefault();
        }
    }
}
