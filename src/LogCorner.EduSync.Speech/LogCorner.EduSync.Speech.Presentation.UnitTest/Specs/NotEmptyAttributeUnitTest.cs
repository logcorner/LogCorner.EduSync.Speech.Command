using LogCorner.EduSync.Speech.Presentation.Dtos;
using System;
using Xunit;

namespace LogCorner.EduSync.Speech.Presentation.UnitTest.Specs
{
    public class NotEmptyAttributeUnitTest
    {
        [Fact]
        public void NotEmptyAttributeWithEmptyGuidIsNotValid()
        {
            //Arrange
            //Act
            var sut = new NotEmptyAttribute();
            var result = sut.IsValid(Guid.Empty);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void NotEmptyAttributeWithNullShouldReturnTrue()
        {
            //Arrange
            //Act
            var sut = new NotEmptyAttribute();
            var result = sut.IsValid(null);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void NotEmptyAttributeWithValidGuidShouldReturnTrue()
        {
            //Arrange
            //Act
            var sut = new NotEmptyAttribute();
            var result = sut.IsValid(Guid.NewGuid());

            //Assert
            Assert.True(result);
        }
    }
}