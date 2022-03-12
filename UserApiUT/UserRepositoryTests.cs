using DataExtension;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using UserApi.Infrastructure;
using UserApi.Infrastructure.User;
using UserApi.Model;
using Xunit;

namespace UserApiUT
{
    public class UserRepositoryTests
    {
        private readonly UserDbContext context;
        private User testUser = new User("Jorge", "Carvalho", "jorge@mail.com", "12345");
        public UserRepositoryTests()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder().UseInMemoryDatabase(
                Guid.NewGuid().ToString());

            context = new UserDbContext(dbOptions.Options);
        }

        [Fact]
        public async void GetUserById_Success()
        {
            //Arrange
            context.Users.Add(testUser);
            await context.SaveChangesAsync();
            var userRepository = new UserRepository(context);

            //Act 
            var result = userRepository.Get(testUser.UniqueId);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void GetUserByEmail_Success()
        {
            //Arrange
            context.Users.Add(testUser);
            await context.SaveChangesAsync();
            var userRepository = new UserRepository(context);

            //Act 
            var result = userRepository.Get(testUser.Email);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void AddUser_Success()
        {
            //Arrange
            var userRepository = new UserRepository(context);

            //Act 
            var result = userRepository.Create(testUser);
            userRepository.Commit();
            userRepository.CommitTransaction();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsValid);
        }

        [Fact]
        public async void UpdateUser_Success()
        {
            //Arrange
            context.Users.Add(testUser);
            await context.SaveChangesAsync();
            var userRepository = new UserRepository(context);

            //Act 
            testUser.SetEmail("jbc@mail.com");
            testUser.SetName("Jorge","Carvalho");
            userRepository.Update(testUser);
            userRepository.Commit();
            userRepository.CommitTransaction();
            var result = userRepository.Get(testUser.UniqueId);

            //Assert
            Assert.Equal(result.FirstName, testUser.FirstName);
            Assert.Equal(result.Email, testUser.Email);
        }

        [Fact]
        public async void DeleteUser_Success()
        {
            //Arrange
            context.Users.Add(testUser);
            await context.SaveChangesAsync();
            var userRepository = new UserRepository(context);

            //Act 
            userRepository.Delete(testUser);
            userRepository.Commit();
            userRepository.CommitTransaction();
            var result = userRepository.Get(testUser.UniqueId);

            //Assert
            Assert.Null(result);
        }
    }
}
