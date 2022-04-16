using Microsoft.EntityFrameworkCore;
using PetApi.Infrastructure;
using PetApi.Infrastructure.Breed;
using PetApi.Model;
using System;
using Xunit;

namespace PetApiUT
{
    public class BreedRepositoryTests
    {
        private Guid invalidGuid = Guid.Parse("53d5f947-7549-4814-b2c6-9777be51d35e");
        private Breed testBreed = new Breed("my breed", new PetApi.Model.Type("Dog"));
        private readonly PetDbContext context;
        public BreedRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder().UseInMemoryDatabase(
                Guid.NewGuid().ToString());

            context = new PetDbContext(dbOptions.Options);
        }

        [Fact]
        public async void GetBrredById_Success()
        {
            //Arrange
            context.Breeds.Add(testBreed);
            await context.SaveChangesAsync();
            var breedRepository = new BreedRepository(context);

            //Act 
            var result = breedRepository.Get(testBreed.UniqueId);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Equal(testBreed.UniqueId, result.UniqueId);
        }

        [Fact]
        public async void GetBreedByName_Success()
        {
            //Arrange
            context.Breeds.Add(testBreed);
            await context.SaveChangesAsync();
            var breedRepository = new BreedRepository(context);

            //Act 
            var result = breedRepository.Get(testBreed.Name);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Equal(testBreed.Name, result.Name);
        }

        [Fact]
        public async void GetTypeById_Success()
        {
            //Arrange
            context.Breeds.Add(testBreed);
            await context.SaveChangesAsync();
            var breedRepository = new BreedRepository(context);

            //Act 
            var result = breedRepository.GetType(testBreed.PetType.UniqueId);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Equal(testBreed.PetType.UniqueId, result.UniqueId);
        }
    }
}
