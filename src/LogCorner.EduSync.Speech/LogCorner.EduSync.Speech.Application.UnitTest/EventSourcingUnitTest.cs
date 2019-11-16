using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Domain.Events;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LogCorner.EduSync.Speech.Application.UnitTest
{
    public class EventSourcingUnitTest
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
    }
}