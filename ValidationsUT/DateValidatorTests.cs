using System;
using System.Collections.Generic;
using System.Globalization;
using Validations;
using Xunit;

namespace ValidationsUT
{
    public class DateValidatorTests
    {
        public static IEnumerable<object[]> GetPastDate()
        {
            yield return new object[] { DateTime.Parse("01-01-2022", new CultureInfo("pt-PT")), "Past date" };
        }

        [Fact]
        public void Validations_DateValidator_ValidateNotInPast_Succeeds()
        {
            //Arrage
            var date = DateTime.Now.AddDays(5);

            //Act
            var result = DateValidator.ValidateNotInPast(date);

            //Acert
            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetPastDate))]
        [InlineData(null, "Invalid")]
        public void Validations_DateValidator_ValidateNotInPast_Fails(DateTime? date, string expectedResult)
        {
            var result = DateValidator.ValidateNotInPast(date);
            Assert.Equal(expectedResult, result);
        }
    }
}