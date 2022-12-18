using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTests.Specs
{
    public class InvokerSpecs
    {
        [Fact(DisplayName = "CreateInstance Of AggregateRoot Should Return AN Empty Aggregate")]
        public void CreateInstanceOfAggregateRootShouldReturnEmptyAggregate()
        {
            //Arrange
            IInvoker<Domain.SpeechAggregate.Speech> sut = new Invoker<Domain.SpeechAggregate.Speech>();

            //Act
            var result = sut.CreateInstanceOfAggregateRoot<Domain.SpeechAggregate.Speech>();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Domain.SpeechAggregate.Speech>(result);

            Assert.Equal(default, result.Id);
        }
    }
}