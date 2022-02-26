using Moq;
using System;
using System.Linq;
using UserApi.Infrastructure.User;
using UserApi.Model;
using UserApi.Service;
using Xunit;

namespace UserApiUT
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepository;
        public UserServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public void GetUser_WithExistingUser_ReturnsOk()
        {
            //Arrange
            var user = new User("Jorge", "jorge@mail.com");
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns(user);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Get(user.UniqueId);

            //Assert
            Assert.Equal(result.Name, user.Name);
            Assert.Equal(result.Email, user.Email);
        }

        [Fact]
        public void GetUser_WithUnexistingUser_ReturnsNotFound()
        {
            //Arrange
            var errorMessage = "User not found";
            var user = new User("Dont Exist", "empty@mail.com");
            _userRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns((User)null);

            //Act 
            var userService = new UserService(_userRepository.Object);
            var result = userService.Get(user.UniqueId);

            //Assert
            Assert.Equal(errorMessage, result.Errors.FirstOrDefault());
        }
    }
}