using Microsoft.EntityFrameworkCore;

namespace PetApi.Infrastructure.Pet
{
    public class PetContext
    {
        public static void SetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Pet>().ToTable("Pets");
            modelBuilder.Entity<Model.Pet>().HasKey(s => s.Id);
            modelBuilder.Entity<Model.Pet>().Ignore(c => c.Errors);
            modelBuilder.Entity<Model.Pet>().HasOne(p => p.Breed).WithMany(s => s.Pets).HasForeignKey("BreedId");
                                
            modelBuilder.Entity<Model.Pet>(entity =>
            {
                entity.Property(o => o.Name).IsRequired().HasMaxLength(20);
                entity.Property(o => o.Sex).IsRequired();
                entity.Property(o => o.Weight).IsRequired();
                entity.Property(o => o.Weight).IsRequired();
                entity.Property(o => o.MicroChiped).IsRequired().HasDefaultValue(0);
                entity.Property(o => o.Neutered).IsRequired().HasDefaultValue(0);
                entity.Property(o => o.Trained).IsRequired().HasDefaultValue(0);
                entity.Property(o => o.FriendlyAroundDogs).IsRequired().HasDefaultValue(0);
                entity.Property(o => o.FriendlyAroundCats).IsRequired().HasDefaultValue(0);
                entity.Property(o => o.Description).IsRequired().HasMaxLength(500);
                entity.Property(o => o.Observations).HasMaxLength(500);
                entity.Property(o => o.Id).IsRequired();
                entity.Property(o => o.UniqueId).IsRequired();

                entity.HasIndex(u => u.UniqueId).IsUnique();
            });
        }
    }
}
