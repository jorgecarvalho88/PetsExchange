using Microsoft.EntityFrameworkCore;

namespace PetApi.Infrastructure.Breed
{
    public class BreedContext
    {
        public static void SetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Breed>().ToTable("Breeds");
            modelBuilder.Entity<Model.Breed>().HasKey(s => s.Id);
            modelBuilder.Entity<Model.Breed>().Ignore(c => c.Errors);
            modelBuilder.Entity<Model.Breed>().HasOne(p => p.PetType).WithMany(s => s.Breeds).HasForeignKey("TypeId");
                                      
            modelBuilder.Entity<Model.Breed>(entity =>
            {
                entity.Property(o => o.Name).IsRequired().HasMaxLength(20);
                entity.Property(o => o.Id).IsRequired();
                entity.Property(o => o.UniqueId).IsRequired();

                entity.HasIndex(u => u.UniqueId).IsUnique();
            });
        }
    }
}
