using Microsoft.EntityFrameworkCore;

namespace PetApi.Infrastructure.Type
{
    public class TypeContext
    {
        public static void SetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Type>().ToTable("Types");
            modelBuilder.Entity<Model.Type>().HasKey(s => s.Id);
            modelBuilder.Entity<Model.Type>().Ignore(c => c.Errors);
            modelBuilder.Entity<Model.Type>().HasMany(p => p.Breeds).WithOne(s => s.PetType);
                                      
            modelBuilder.Entity<Model.Type>(entity =>
            {
                entity.Property(o => o.Name).IsRequired().HasMaxLength(20);
                entity.Property(o => o.Id).IsRequired();
                entity.Property(o => o.UniqueId).IsRequired();

                entity.HasIndex(u => u.UniqueId).IsUnique();
            });
        }
    }
}
