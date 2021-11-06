using LogCorner.EduSync.Speech.Domain.Exceptions;
using Xunit;

namespace LogCorner.EduSync.Speech.Domain.UnitTest.Specs
{
    public class SpeechTypeValueObjectUnitTest
    {
        [Fact]
        public void EqualityIsTrueWhenObjectsAreSameValuesTest()
        {
            //Arrange
            var url1 = new SpeechType(SpeechTypeEnum.Conferences.ToString());
            var url2 = new SpeechType(SpeechTypeEnum.Conferences.ToString());

            Assert.Equal(url1, url2);
            Assert.True(url1.Equals(url2));
            Assert.True(url1.Equals((object)url2));
            Assert.Equal(url1.GetHashCode(), url2.GetHashCode());
        }

        [Fact]
        public void EqualityIsFalseWhenObjectsAreDifferentValuesTest()
        {
            //Arrange
            var url1 = new SpeechType(SpeechTypeEnum.Conferences.ToString());
            var url2 = new SpeechType(SpeechTypeEnum.SelfPacedLabs.ToString());

            Assert.False(url1 == url2);
        }

        [Fact]
        public void SpeechTypeWithNotDefinedValueShouldRaiseInvalidEnumAggregateException()
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<InvalidEnumAggregateException>(() => new SpeechType(4));
        }
    }
}