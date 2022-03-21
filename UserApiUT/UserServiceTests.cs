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
        private User testUser = new User("Jorge", "Carvalho", "jorge@mail.com", null, "Rua de cima", 4715011, "Braga", Convert.ToDateTime("30/10/1988"), null, null);
        private User invalidTestUser = new User("Jorge", "Carvalho", "jorge.com", null, "Rua de cima", 47150, "Braga", Convert.ToDateTime("30/10/1988"), null, null);
        private readonly UserContract invalidUserContract = new UserContract(new Guid(), "Jorge", "Carvalho", "jorge.com", null, "Rua de cima", 4715011, "Braga", Convert.ToDateTime("30/10/1988"), null, null, new List<string>());
        private readonly UserContract testUserContract = new UserContract(new Guid(), "Jorge", "Carvalho", "jorge@mail.com", null, "Rua de cima", 4715011, "Braga", Convert.ToDateTime("30/10/1988"), null, null, new List<string>());
        private readonly Mock<IUserRepository> _userRepository;
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
            Assert.Equal(testUser.FirstName, result.FirstName);
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
            Assert.Equal(testUser.FirstName, result.FirstName);
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
        public void AddUser_WithInvalidUser_ReturnsBadRequest()
        {
            //Arrange
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns((User)null);
            _userRepository.Setup(s => s.Create(It.IsAny<User>())).Verifiable();

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Add(invalidUserContract);

            //Assert
            _userRepository.Verify(s => s.BeginTransaction(), Times.Once());
            _userRepository.Verify(s => s.RollBackTransaction(), Times.Once());

            Assert.False(invalidUserContract.IsValid);
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

        [Fact]
        public void UpdateUser_WithValidUser_ReturnsOk()
        {
            //Arrange
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns(testUser);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Update(testUserContract);

            //Assert
            _userRepository.Verify(s => s.BeginTransaction(), Times.Once());
            _userRepository.Verify(s => s.Update(It.IsAny<User>()), Times.Once());
            _userRepository.Verify(s => s.Commit(), Times.Once());
            _userRepository.Verify(s => s.CommitTransaction(), Times.Once());

            Assert.True(result.IsValid);
        }

        [Fact]
        public void UpdateUser_WithUnexistingUser_ReturnsBadRequest()
        {
            //Arrange
            var errorMessage = "Invalid UniqueId";
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns((User)null);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Update(testUserContract);

            //Assert
            _userRepository.Verify(s => s.BeginTransaction(), Times.Once());
            _userRepository.Verify(s => s.RollBackTransaction(), Times.Once());

            //Assert
            Assert.Equal(errorMessage, result.Errors.First());
        }

        [Fact]
        public void UpdateUser_NotValidUser_ReturnsBadRequest()
        {
            //Arrange
            var errorMessage = "Email is Invalid";
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns(testUser);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Update(invalidUserContract);

            //Assert
            _userRepository.Verify(s => s.BeginTransaction(), Times.Once());
            _userRepository.Verify(s => s.RollBackTransaction(), Times.Once());

            //Assert
            Assert.False(result.IsValid);
            Assert.Equal(errorMessage, result.Errors.First());
        }

        [Fact]
        public void DeleteUser_WithValidUser_ReturnsOk()
        {
            //Arrange
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns(testUser);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Delete(testUserContract.UniqueId);

            //Assert
            _userRepository.Verify(s => s.BeginTransaction(), Times.Once());
            _userRepository.Verify(s => s.Delete(It.IsAny<User>()), Times.Once());
            _userRepository.Verify(s => s.Commit(), Times.Once());
            _userRepository.Verify(s => s.CommitTransaction(), Times.Once());

            Assert.True(result.IsValid);
        }

        [Fact]
        public void DeleteUser_WithUnexistingUser_ReturnsBadRequest()
        {
            //Arrange
            var errorMessage = "Invalid UniqueId";
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns((User)null);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Delete(testUserContract.UniqueId);

            //Assert
            _userRepository.Verify(s => s.BeginTransaction(), Times.Once());
            _userRepository.Verify(s => s.RollBackTransaction(), Times.Once());

            //Assert
            Assert.Equal(errorMessage, result.Errors.First());
        }
    }
}