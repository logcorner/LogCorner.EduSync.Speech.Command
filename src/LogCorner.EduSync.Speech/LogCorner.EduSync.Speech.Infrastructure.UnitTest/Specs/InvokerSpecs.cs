using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System.Collections.Generic;
using LogCorner.EduSync.Speech.Domain;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
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
            Assert.Equal(new Title(null), result.Title);
            Assert.Equal(default, result.Description);
            Assert.Equal(default, result.Url);
            Assert.Equal(default, result.Type);
            Assert.Equal(new List<MediaFile>(), result.MediaFileItems);
        }
    }
}