using Microsoft.EntityFrameworkCore;
using UserApi.Infrastructure.User;

namespace UserApi.Infrastructure
{
    public class UserDbContext : DbContext
    {
        private readonly string _connectionString;

        public UserDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserContext.SetModel(modelBuilder);
        }
    }
}
