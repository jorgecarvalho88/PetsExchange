using Microsoft.EntityFrameworkCore;
using PetApi.Infrastructure;
using PetApi.Infrastructure.Pet;
using PetApi.Model;
using System;
using System.Linq;
using Xunit;

namespace PetApiUT
{
    public class PetRepositoryTests
    {
        private Guid invalidGuid = Guid.Parse("53d5f947-7549-4814-b2c6-9777be51d35e");
        private Guid ownerId = Guid.Parse("d9998f9b-0fae-49cb-903d-6afa66a742cf");
        private Pet testPet = new Pet(
            new Breed("my breed", new PetApi.Model.Type("Dog")),
            Guid.Parse("d9998f9b-0fae-49cb-903d-6afa66a742cf"),
            "my name",
            "M",
            10,
            true,
            true,
            true,
            true, true,
            "very nice doggo", null);

        private readonly PetDbContext context;
        public PetRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder().UseInMemoryDatabase(
                Guid.NewGuid().ToString());

            context = new PetDbContext(dbOptions.Options);
        }

        [Fact]
        public async void GetPetById_Success()
        {
            //Arrange
            context.Pets.Add(testPet);
            await context.SaveChangesAsync();
            var petRepository = new PetRepository(context);

            //Act 
            var result = petRepository.Get(testPet.UniqueId);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void GetPetsByOwner_Success()
        {
            //Arrange
            context.Pets.Add(testPet);
            await context.SaveChangesAsync();
            var petRepository = new PetRepository(context);

            //Act 
            var result = petRepository.GetByOwner(ownerId);

            //Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(ownerId, result.FirstOrDefault().Owner);
        }

        [Fact]
        public async void GetPetById_Fail()
        {
            //Arrange
            context.Pets.Add(testPet);
            await context.SaveChangesAsync();
            var petRepository = new PetRepository(context);

            //Act 
            var result = petRepository.GetByOwner(invalidGuid);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public async void GetBreedById_Success()
        {
            //Arrange
            context.Pets.Add(testPet);
            await context.SaveChangesAsync();
            var petRepository = new PetRepository(context);

            //Act 
            var result = petRepository.GetBreed(testPet.Breed.UniqueId);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Equal("my breed", result.Name);
        }

        [Fact]
        public async void GetBreedById_Fail()
        {
            //Arrange
            context.Pets.Add(testPet);
            await context.SaveChangesAsync();
            var petRepository = new PetRepository(context);

            //Act 
            var result = petRepository.GetBreed(invalidGuid);

            //Assert
            Assert.Null(result);
        }
    }
}