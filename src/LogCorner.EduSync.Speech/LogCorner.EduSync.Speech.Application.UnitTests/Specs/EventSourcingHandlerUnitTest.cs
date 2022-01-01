using LogCorner.EduSync.Notification.Common;
using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LogCorner.EduSync.Speech.Application.UnitTests.Specs
{
    public class EventSourcingHandlerUnitTest
    {
        [Fact(DisplayName = "Handle with null events should raise EventNullException")]
        public async Task HandleWithNullEventsShouldRaiseEventNullException()
        {
            //Arrange
            var mockEventStoreRepository = new Mock<IEventStoreRepository>();
            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();

            var mockEventSerializer = new Mock<IEventSerializer>();

            long version = 0;

            //Act
            //Assert
            var sut = new EventSourcingHandler(moqUnitOfWork.Object, mockEventStoreRepository.Object, mockEventSerializer.Object, It.IsAny<ISignalRPublisher>());
            await Assert.ThrowsAsync<EventNullException>(() => sut.Handle(null, version));
        }

        [Fact(DisplayName = "Handle with events should call AppendAsync")]
        public async Task HandleWithEventsShouldCallAppendAsync()
        {
            //Arrange
            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();
            moqUnitOfWork.Setup(m => m.Commit()).Verifiable();
            var mockEventStoreRepository = new Mock<IEventStoreRepository>();
            mockEventStoreRepository.Setup(e => e.AppendAsync(It.IsAny<EventStore>()))
                .Returns(Task.FromResult(true))
                .Verifiable();

            var mockEventSerializer = new Mock<IEventSerializer>();

            var @event = new SpeechCreatedEvent(It.IsAny<Guid>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<SpeechTypeEnum>());

            long version = 0;

            var moqSignalRPublisher = new Mock<ISignalRPublisher>();
            moqSignalRPublisher.Setup(p => p.SubscribeAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

            moqSignalRPublisher.Setup(p => p.PublishAsync(It.IsAny<string>(), It.IsAny<EventStore>())).Returns(Task.CompletedTask);

            //Act
            var sut = new EventSourcingHandler(moqUnitOfWork.Object, mockEventStoreRepository.Object,
                mockEventSerializer.Object, moqSignalRPublisher.Object);
            await sut.Handle(@event, version);

            //Assert
            mockEventStoreRepository.Verify(m => m.AppendAsync(It.IsAny<EventStore>()),
                Times.Once, "AppendAsync must be called only once");
        }

        [Fact(DisplayName = "Handle with events should call AppendAsync")]
        public async Task HandleWithEventsShouldCallCommitAsync()
        {
            //Arrange
            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();
            moqUnitOfWork.Setup(m => m.Commit()).Verifiable();
            var mockEventStoreRepository = new Mock<IEventStoreRepository>();
            mockEventStoreRepository.Setup(e => e.AppendAsync(It.IsAny<EventStore>()))
                .Returns(Task.FromResult(true))
                .Verifiable();

            var mockEventSerializer = new Mock<IEventSerializer>();

            var @event = new SpeechCreatedEvent(It.IsAny<Guid>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<SpeechTypeEnum>());

            long version = 0;

            var moqSignalRPublisher = new Mock<ISignalRPublisher>();
            moqSignalRPublisher.Setup(p => p.SubscribeAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

            moqSignalRPublisher.Setup(p => p.PublishAsync(It.IsAny<string>(), It.IsAny<EventStore>())).Returns(Task.CompletedTask);

            //Act
            var sut = new EventSourcingHandler(moqUnitOfWork.Object, mockEventStoreRepository.Object,
                mockEventSerializer.Object, moqSignalRPublisher.Object);
            await sut.Handle(@event, version);

            //Assert
            // Verify that SaveChanges is called
            moqUnitOfWork.Verify(m => m.Commit(), Times.Once, "Commit must be called only once");
        }
    }
}