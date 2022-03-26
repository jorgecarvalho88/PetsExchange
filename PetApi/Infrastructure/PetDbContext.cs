using Microsoft.EntityFrameworkCore;
using PetApi.Infrastructure.Breed;
using PetApi.Infrastructure.Pet;
using PetApi.Infrastructure.Type;

namespace PetApi.Infrastructure
{
    public class PetDbContext : DbContext
    {
        public PetDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Model.Pet> Pets => Set<Model.Pet>();
        public DbSet<Model.Breed> Breeds => Set<Model.Breed>();
        public DbSet<Model.Type> Types => Set<Model.Type>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PetContext.SetModel(modelBuilder);
            BreedContext.SetModel(modelBuilder);
            TypeContext.SetModel(modelBuilder);
        }
    }
}
