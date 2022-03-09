using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validations;
using Xunit;

namespace ValidationsUT
{
    public class PassworValidatorTests
    {
        [Fact]
        public void Validations_PasswordValidator_GenerateAndValidatePassword_Succeeds()
        {
            //Arrage
            var password = "Jorge";

            //Act
            var result = PasswordValidator.GeneratePassword(password);
            var isValidResult = PasswordValidator.ValidatePassword(password, result);

            //Acert
            Assert.True(isValidResult);
        }

        [Theory]
        [InlineData("mystrongpassword", "thisisawrongpassword", false)]
        [InlineData("CapsPassWord", "capspassword", false)]
        public void Validations_PasswordValidator_ValidatePassword_Fails(string password, string wrongPassword, bool expectedResult)
        {
            var result = PasswordValidator.GeneratePassword(password);
            var isResultValid = PasswordValidator.ValidatePassword(wrongPassword, result);

            Assert.False(isResultValid);
        }
    }
}
