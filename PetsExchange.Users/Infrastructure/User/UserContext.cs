using Microsoft.EntityFrameworkCore;

namespace UserApi.Infrastructure.User
{
    public class UserContext
    {
        public static void SetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.User>().ToTable("Users");
            modelBuilder.Entity<Model.User>().HasKey(s => s.Id);
            modelBuilder.Entity<Model.User>().Ignore(c => c.Errors);

            modelBuilder.Entity<Model.User>(entity =>
            {
                entity.Property(o => o.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(o => o.LastName).IsRequired().HasMaxLength(50);
                entity.Property(o => o.Email).IsRequired().HasMaxLength(50);
                entity.Property(o => o.Id).IsRequired();
                entity.Property(o => o.UniqueId).IsRequired();
                entity.Property(o => o.PasswordHash).IsRequired();
                entity.Property(o => o.ConfirmedEmail).IsRequired().HasDefaultValue(0);

                entity.HasIndex(u => u.UniqueId).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });

            
        }
    }
}
