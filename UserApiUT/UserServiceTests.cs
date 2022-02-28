using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using UserApi.Infrastructure.User;
using UserApi.Model;
using UserApi.Service;
using UserApiContract;
using Xunit;

namespace UserApiUT
{
    public class UserServiceTests
    {
        private User testUser = new User("Jorge", "jorge@gmail.com");
        private UserContract testUserContract = new UserContract(new Guid(), "Jorge", "jorge@gmail.com", new List<string>());
        private Mock<IUserRepository> _userRepository;
        public UserServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public void GetUserById_WithExistingUser_ReturnsOk()
        {
            //Arrange
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns(testUser);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Get(testUser.UniqueId);

            //Assert
            Assert.Equal(testUser.Name, result.Name);
            Assert.Equal(testUser.Email, result.Email);
        }

        [Fact]
        public void GetUserById_WithUnexistingUser_ReturnsNotFound()
        {
            //Arrange
            var errorMessage = "User not found";
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns((User)null);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Get(testUser.UniqueId);

            //Assert
            Assert.Equal(errorMessage, result.Errors.FirstOrDefault());
        }

        [Fact]
        public void GetUserByEmail_WithExistingUser_ReturnsOk()
        {
            //Arrange
            _userRepository.Setup(s => s.Get(It.IsAny<string>())).Returns(testUser);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Get(testUser.Email);

            //Assert
            Assert.Equal(testUser.Name, result.Name);
            Assert.Equal(testUser.Email, result.Email);
        }

        [Fact]
        public void GetUserByEmail_WithUnexistingUser_ReturnsNotFound()
        {
            //Arrange
            var errorMessage = "User not found";
            _userRepository.Setup(s => s.Get(It.IsAny<string>())).Returns((User)null);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Get(testUser.Email);

            //Assert
            Assert.Equal(errorMessage, result.Errors.FirstOrDefault());
        }

        [Fact]
        public void AddUser_WithValidUser_ReturnsOk()
        {
            //Arrange
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns((User)null);
            _userRepository.Setup(s => s.Create(It.IsAny<User>())).Verifiable();

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Add(testUserContract);

            //Assert
            _userRepository.Verify(s => s.BeginTransaction(), Times.Once());
            _userRepository.Verify(s => s.Create(It.IsAny<User>()), Times.Once());
            _userRepository.Verify(s => s.Commit(), Times.Once());
            _userRepository.Verify(s => s.CommitTransaction(), Times.Once());

            Assert.True(testUserContract.IsValid);
        }

        [Fact]
        public void AddUser_WithExistingUser_ReturnsBadRequest()
        {
            //Arrange
            var errorMessage = "User already exists";
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns(testUser);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Add(testUserContract);

            //Assert
            _userRepository.Verify(s => s.BeginTransaction(), Times.Once());
            _userRepository.Verify(s => s.RollBackTransaction(), Times.Once());

            Assert.Equal(errorMessage, result.Errors.First());
        }
    }
}