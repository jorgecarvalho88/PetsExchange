using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validations;
using Xunit;

namespace ValidationsUT
{
    public class PasswordValidatorTests
    {
        [Fact]
        public void Validations_PasswordValidator_GenerateAndValidatePassword_Succeeds()
        {
            //Arrage
            var password = "This is my password";

            //Act
            var result = PasswordValidator.GeneratePassword(password);
            var isValidResult = PasswordValidator.ValidatePassword(password, result);

            //Acert
            Assert.NotNull(result);
            Assert.True(isValidResult);
        }

        [Theory]
        [InlineData("mystrongpassword", "notmypassword" , false)]
        [InlineData("CapsPassword", "capspassword" , false)]
        public void Validations_PasswordValidator_ValidatePassword_Fails(string password, string wrongPassword, bool expectedResult)
        {
            //Act
            var result = PasswordValidator.GeneratePassword(password);
            var isValidResult = PasswordValidator.ValidatePassword(wrongPassword, result);

            //Acert
            Assert.False(isValidResult);
        }
    }
}
