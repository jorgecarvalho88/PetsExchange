using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validations;
using Xunit;

namespace ValidationsUT
{
    public class StringValidatorTests
    {
        [Fact]
        public void Validations_StringValidator_ValidateLength_Succeeds()
        {
            //Arrage
            var myName = "Jorge";

            //Act
            var result = StringValidator.ValidateLength(myName, "firstName", 50);

            //Acert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("joao ribeiro da silva alves antunes", "name", 10, "name: Max length is 10 chars")]
        public void Validations_StringValidator_ValidateLength_Fails(string? str, string property, int length, string expectedResult)
        {
            var result = StringValidator.ValidateLength(str, property, length);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Validations_StringValidator_ValidateIsNullOrWhiteSpace_Succeeds()
        {
            //Arrage
            var myName = "Jorge Carvalho";
            var property = "name";

            //Act
            var result = StringValidator.ValidateIsNullOrWhiteSpace(myName, property);

            //Acert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(null, "name", "name: Required Field")]
        [InlineData("", "name", "name: Required Field")]
        public void Validations_StringValidator_ValidateIsNullOrWhiteSpace_Fails(string? email, string property, string expectedResult)
        {
            var result = StringValidator.ValidateIsNullOrWhiteSpace(email, property);
            Assert.Equal(expectedResult, result);
        }
    }
}
