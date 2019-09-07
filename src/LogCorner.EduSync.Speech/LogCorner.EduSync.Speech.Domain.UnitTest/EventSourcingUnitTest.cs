using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LogCorner.EduSync.Speech.Domain.Events;
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

        [Fact]
        public void ApplyEventShouldPopulateAggregatePropertiesWithEventProperties()
        {
            //Arrange
            long expectedVersion = -1;
           
            var evt = new SpeechCreatedEvent(Guid.NewGuid(),
                new Title("SpeechCreatedEvent Title "),
                new UrlValue("http://url-evt.com"),
                new Description("SpeechCreatedEvent description must be very long as a description than people can understand without efforts"),
                SpeechType.Conferences);
            var aggregate =  CreateNewAggregate();
           
            //Act
            aggregate.ApplyEvent(evt,expectedVersion);

            //Assert
            Assert.Equal(evt.AggregateId, aggregate.Id);
            Assert.Equal(evt.Title, aggregate.Title);
            Assert.Equal(evt.Url, aggregate.Url);
            Assert.Equal(evt.Description, aggregate.Description);
            Assert.Equal(evt.Type, aggregate.Type);

            Assert.IsAssignableFrom<IDomainEvent>(evt);
        }

        [Fact]
        public void GetUncommittedEventsOfNewAggregateShouldReturnListOfIDomainEvent()
        {
            //Arrange
            IEventSourcing aggregate =  CreateNewAggregate();
          
            //Act
            var result = aggregate.GetUncommittedEvents();
            
            //Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsAssignableFrom<IEnumerable<IDomainEvent>>(result);
        }

        private SpeechAggregate.Speech CreateNewAggregate()
        {
            return (SpeechAggregate.Speech)typeof(SpeechAggregate.Speech)
                .GetConstructor(BindingFlags.Instance |
                                         BindingFlags.NonPublic | 
                                         BindingFlags.Public,
                                null,
                                new Type[0], 
                                new ParameterModifier[0])
                ?.Invoke(new object[0]);
        }
    }
}