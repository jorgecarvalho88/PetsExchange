using AuthApi.Infrastructure.RefreshToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Infrastructure
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Model.RefreshToken> RefreshTokens => Set<Model.RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            RefreshTokenContext.SetModel(modelBuilder);
        }
    }
}
