using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;
using Xunit;

namespace LogCorner.EduSync.Speech.Domain.UnitTest
{
    public class EventSourcingUnitTest
    {
        [Fact]
        public void ValidateVersionWithInvalidExpectedVersionShouldRaiseConcurrencyException()
        {
            //Arrange
            long expectedVersion = 0;
            IEventSourcing source = new StubEventSourcing();
            //Act
            //Assert
            Assert.Throws<ConcurrencyException>(()
                => source.ValidateVersion(expectedVersion));
        }

        [Fact]
        public void ValidateVersionWithValidExpectedVersionThenExpectedVersionShouldBeEqualsToAggregateVersion()
        {
            //Arrange
            long expectedVersion = -1;
            IEventSourcing source = new StubEventSourcing();
            //Act
            source.ValidateVersion(expectedVersion);
            //Assert
            Assert.Equal(expectedVersion, source.Version);
        }
    }

    public class StubEventSourcing : AggregateRoot<Guid>
    {
    }
}