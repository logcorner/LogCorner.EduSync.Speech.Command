using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LogCorner.EduSync.Speech.Application.UnitTest
{
    public class EventSourcingHandlerUnitTest
    {
        [Fact(DisplayName = "Handle with null events should raise EventNullException")]
        public async Task HandleWithNullEventsShouldRaiseEventNullException()
        {
            //Arrange
            var mockEventStoreRepository = new Mock<IEventStoreRepository<AggregateRoot<Guid>>>();
            mockEventStoreRepository.Setup(e => e.AppendAsync(It.IsAny<EventStore>()))
                .Verifiable();

            var mockEventSerializer = new Mock<IEventSerializer>();

            long version = 0;

            //Act
            //Assert
            var sut = new EventSourcingHandler<AggregateRoot<Guid>>(mockEventStoreRepository.Object, mockEventSerializer.Object);
            await Assert.ThrowsAsync<EventNullException>(() => sut.Handle(null, version));
        }
    }
}