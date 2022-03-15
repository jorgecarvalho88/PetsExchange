using AuthApi.Infrastructure.RefreshToken;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Infrastructure
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Model.RefreshToken> RefreshTokens => Set<Model.RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RefreshTokenContext.SetModel(modelBuilder);
        }
    }
}
