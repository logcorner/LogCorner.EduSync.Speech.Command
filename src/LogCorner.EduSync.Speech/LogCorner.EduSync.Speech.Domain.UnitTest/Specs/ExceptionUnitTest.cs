using LogCorner.EduSync.Speech.Domain.Exceptions;
using Xunit;

namespace LogCorner.EduSync.Speech.Domain.UnitTest.Specs
{
    public class ExceptionUnitTest
    {
        [Fact]
        public void Should_set_exception_values()
        {
            //Arrange
            string message = "test message";

            int errorCode = 1;

            //Act
            var sut = new ConcurrencyException(errorCode, message);

            //Assert
            Assert.Equal(errorCode, sut.ErrorCode);
            Assert.Equal(message, sut.Message);
        }
    }
}