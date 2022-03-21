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
                entity.Property(o => o.FirstName).IsRequired().HasMaxLength(15);
                entity.Property(o => o.LastName).IsRequired().HasMaxLength(15);
                entity.Property(o => o.Email).IsRequired().HasMaxLength(20);
                entity.Property(o => o.MobileNumber).HasColumnType("char(9)");
                entity.Property(o => o.Address).IsRequired().HasMaxLength(50);
                entity.Property(o => o.PostCode).IsRequired().HasColumnType("char(7)");
                entity.Property(o => o.City).IsRequired().HasMaxLength(20);
                entity.Property(o => o.DateOfBirth).IsRequired();
                entity.Property(o => o.SitterProfileId);
                entity.Property(o => o.ProfilePhotoUrl);

                entity.Property(o => o.Id).IsRequired();
                entity.Property(o => o.UniqueId).IsRequired();

                entity.HasIndex(u => u.UniqueId).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.SitterProfileId).IsUnique();
                entity.HasIndex(u => u.MobileNumber).IsUnique();
            });

            
        }
    }
}
