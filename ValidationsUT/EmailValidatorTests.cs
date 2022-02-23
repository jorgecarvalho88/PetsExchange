using Validations;
using Xunit;

namespace ValidationsUT
{
    public class EmailValidatorTests
    {
        [Fact]
        public void Validations_EmailValidator_Validate_Succeeds()
        {
            //Arrage
            var email = "jorge@gmail.com";

            //Act
            var result = EmailValidatior.IsValidEmail(email);  

            //Acert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("jorge.com", "Email is Invalid")]
        [InlineData("jorge@gmail", "Email is Invalid")]
        [InlineData("jorge", "Email is Invalid")]
        [InlineData(null, "Email is Invalid")]
        public void Validations_EmailValidator_Validate_Fails(string? email, string expectedResult)
        {
            var result = EmailValidatior.IsValidEmail(email);
            Assert.Equal(expectedResult, result);
        }
    }
}
