//using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
//using LogCorner.EduSync.Speech.Domain.Exceptions;
//using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using Xunit;

//namespace LogCorner.EduSync.Speech.Domain.UnitTests.Specs
//{
//    public class EventSourcingUnitTest
//    {
//        [Fact]
//        public void ValidateVersionWithInvalidExpectedVersionShouldRaiseConcurrencyException()
//        {
//            //Arrange
//            long expectedVersion = 0;
//            IEventSourcing source = new StubEventSourcing();
//            //Act
//            //Assert
//            Assert.Throws<ConcurrencyException>(()
//                => source.ValidateVersion(expectedVersion));
//        }

//        [Fact]
//        public void ValidateVersionWithValidExpectedVersionThenExpectedVersionShouldBeEqualsToAggregateVersion()
//        {
//            //Arrange
//            long expectedVersion = -1;
//            IEventSourcing source = new StubEventSourcing();
//            //Act
//            source.ValidateVersion(expectedVersion);
//            //Assert
//            Assert.Equal(expectedVersion, source.Version);
//        }

//        [Fact]
//        public void ApplyEventShouldPopulateAggregatePropertiesWithEventProperties()
//        {
//            //Arrange
//            long expectedVersion = -1;
//            var speechType = new SpeechType("Conferences");
//            var evt = new SpeechCreatedEvent(Guid.NewGuid(),
//               "SpeechCreatedEvent Title ",
//                "http://url-evt.com",
//               "SpeechCreatedEvent description must be very long as a description than people can understand without efforts",
//                new SpeechTypeEnum(speechType.IntValue, speechType.StringValue));
//            var aggregate = CreateNewAggregate<SpeechAggregate.Speech>();

//            //Act
//            aggregate.ApplyEvent(evt, expectedVersion);

//            //Assert
//            Assert.Equal(evt.AggregateId, aggregate.Id);
//            Assert.Equal(evt.Title, aggregate.Title.Value);
//            Assert.Equal(evt.Url, aggregate.Url.Value);
//            Assert.Equal(evt.Description, aggregate.Description.Value);
//            Assert.Equal(evt.Type.Name, aggregate.Type.Value.ToString());

//            Assert.IsAssignableFrom<IDomainEvent>(evt);
//        }

//        [Fact]
//        public void GetUncommittedEventsOfNewAggregateShouldReturnListOfIDomainEvent()
//        {
//            //Arrange

//            IEventSourcing aggregate = CreateNewAggregate<StubEventSourcing>();

//            //Act
//            var result = aggregate.GetUncommittedEvents();

//            //Assert
//            Assert.NotNull(result);
//            Assert.Empty(result);
//            Assert.IsAssignableFrom<IEnumerable<IDomainEvent>>(result);
//        }

//        [Fact]
//        public void AddDomainEventWithInvalidVesrionShouldRaiseConncurrencyException()
//        {
//            //Arrange
//            long expectedVersion = 0;
//            var sut = CreateNewAggregate<StubEventSourcing>();
//            //Act
//            //Assert
//            Assert.Throws<ConcurrencyException>(()
//                => sut.ExposeAddDomainEvent(It.IsAny<IDomainEvent>(), expectedVersion));
//        }

//        [Fact]
//        public void AddDomainEventWithValidVersionThenVersionOfEventShouldBeEqualsToCurrentVerSionOfAggregate()
//        {
//            //Arrange
//            long expectedVersion = -1;
//            IDomainEvent @event = new SubEvent(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>());
//            var sut = CreateNewAggregate<StubEventSourcing>();

//            //Act
//            sut.ExposeAddDomainEvent(@event, expectedVersion);

//            //Assert
//            Assert.Equal(sut.Version, @event.AggregateVersion);
//        }

//        [Fact]
//        public void AddDomainEventWithValidVersionThenEventShouldBeAppliedToAggregate()
//        {
//            //Arrange
//            long expectedVersion = -1;

//            var sut = CreateNewAggregate<StubEventSourcing>();
//            var @event = new SubEvent(Guid.NewGuid(), sut.Id, "test value");
//            //Act
//            sut.ExposeAddDomainEvent(@event, expectedVersion);

//            //Assert
//            Assert.Equal(sut.Id, @event.AggregateId);
//            Assert.Equal(sut.Value, @event.Value);
//        }

//        [Fact]
//        public void AddDomainEventWithValidVersionThenUncommittedEventsShouldBeSingle()
//        {
//            //Arrange
//            long expectedVersion = -1;

//            var sut = CreateNewAggregate<StubEventSourcing>();
//            var @event = new SubEvent(Guid.NewGuid(), sut.Id, "test value");

//            //Act
//            sut.ExposeAddDomainEvent(@event, expectedVersion);
//            var uncommittedEvents = sut.GetUncommittedEvents().Single();

//            //Assert
//            Assert.NotNull(uncommittedEvents);
//            Assert.Equal(uncommittedEvents, @event);
//        }

//        [Fact]
//        public void ClearUncommittedEventsThenUncommittedEventsShouldBeEmpty()
//        {
//            //Arrange
//            long expectedVersion = -1;

//            var sut = CreateNewAggregate<StubEventSourcing>();
//            var @event = new SubEvent(Guid.NewGuid(), sut.Id, "test value");

//            //Act
//            sut.ExposeAddDomainEvent(@event, expectedVersion);
//            sut.ClearUncommittedEvents();
//            var uncommittedEvents = sut.GetUncommittedEvents();

//            //Assert
//            Assert.NotNull(uncommittedEvents);
//            Assert.Empty(uncommittedEvents);
//        }

//        private static T CreateNewAggregate<T>() where T : AggregateRoot<Guid>
//        {
//            return (T)typeof(T)
//                .GetConstructor(BindingFlags.Instance |
//                                         BindingFlags.NonPublic |
//                                         BindingFlags.Public,
//                                null,
//                                Type.EmptyTypes,
//                                Array.Empty<ParameterModifier>())
//                ?.Invoke(Array.Empty<object>());
//        }
//    }
//}