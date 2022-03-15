using Microsoft.EntityFrameworkCore;

namespace AuthApi.Infrastructure.RefreshToken
{
    public class RefreshTokenContext
    {
        public static void SetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.RefreshToken>().ToTable("RefreshTokens");
            modelBuilder.Entity<Model.RefreshToken>().HasKey(s => s.Id);
            modelBuilder.Entity<Model.RefreshToken>().Ignore(c => c.Errors);

            modelBuilder.Entity<Model.RefreshToken>(entity =>
            {
                entity.Property(o => o.UserId).IsRequired();
                entity.Property(o => o.Token).IsRequired();
                entity.Property(o => o.JwtId).IsRequired();
                entity.Property(o => o.IsUsed).IsRequired().HasDefaultValue(0);
                entity.Property(o => o.IsRevoked).IsRequired().HasDefaultValue(0);
                entity.Property(o => o.AddedDate).IsRequired();
                entity.Property(o => o.ExpDate).IsRequired();
            });


        }
    }
}
