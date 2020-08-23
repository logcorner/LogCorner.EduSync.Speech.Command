using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogCorner.EduSync.Speech.SharedKernel.Events;
using Xunit;

namespace LogCorner.EduSync.Speech.Application.UnitTest.Specs
{
    public class EventSourcingSubscriberUnitTest
    {
        [Fact(DisplayName = "Subscribe with no uncommitted events should not call handle")]
        public async Task SubscribeWithNoUncommittedEventsShouldNotCallHandle()
        {
            //Arrange
            var mockEventSourcingHandler = new Mock<IEventSourcingHandler<Event>>();
            mockEventSourcingHandler.Setup(e => e.Handle(It.IsAny<Event>(),
                It.IsAny<long>())).Verifiable();

            var moqAggregate = new Mock<IEventSourcing>();

            //Act
            var sut = new EventSourcingSubscriber(mockEventSourcingHandler.Object);
            await sut.Subscribe(moqAggregate.Object);

            //Assert
            mockEventSourcingHandler.Verify(m => m.Handle(It.IsAny<Event>(),
                It.IsAny<long>()), Times.Never, "Handle must not be called");
            Assert.Equal(0, moqAggregate.Object.Version);
        }

        [Fact(DisplayName = "Subscribe with uncommitted events should call handle only once")]
        public async Task SubscribeWithUncommittedEventsShouldCallHandleOnlyOnce()
        {
            //Arrange
            var mockEventSourcingHandler = new Mock<IEventSourcingHandler<Event>>();

            mockEventSourcingHandler.Setup(e => e.Handle(It.IsAny<Event>(),
                It.IsAny<long>())).Returns(Task.FromResult(true)).Verifiable();

            var moqAggregate = new Mock<IEventSourcing>();
            moqAggregate.Setup(a => a.GetUncommittedEvents()).Returns(new List<Event>
            {
                new SpeechCreatedEvent(It.IsAny<Guid>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>())
            });

            //Act
            var sut = new EventSourcingSubscriber(mockEventSourcingHandler.Object);
            await sut.Subscribe(moqAggregate.Object);

            //Assert
            mockEventSourcingHandler.Verify(m => m.Handle(It.IsAny<Event>(),
                It.IsAny<long>()), Times.Once, "Handle must be called only once");
            Assert.Equal(0, moqAggregate.Object.Version);
            Assert.Single(moqAggregate.Object.GetUncommittedEvents());
        }
    }
}