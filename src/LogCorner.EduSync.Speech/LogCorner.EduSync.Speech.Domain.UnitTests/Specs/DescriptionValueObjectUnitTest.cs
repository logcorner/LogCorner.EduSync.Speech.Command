using Xunit;

namespace LogCorner.EduSync.Speech.Domain.UnitTests.Specs
{
    public class DescriptionValueObjectUnitTest
    {
        [Fact]
        public void EqualityIsTrueWhenObjectsAreSameValuesTest()
        {
            //Arrange
            var description1 = new Description("rem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.");
            var description2 = new Description("rem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.");

            Assert.Equal(description1, description2);
            Assert.True(description1.Equals(description2));
            Assert.True(description1.Equals((object)description2));
            Assert.Equal(description1.GetHashCode(), description2.GetHashCode());
        }

        [Fact]
        public void EqualityIsFalseWhenObjectsAreDifferentValuesTest()
        {
            //Arrange
            var description1 = new Description("rem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.");
            var description2 = new Description("defLorem rem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.");

            Assert.False(description1 == description2);
        }
    }
}