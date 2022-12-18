//using LogCorner.EduSync.Notification.Common.Hub;
//using LogCorner.EduSync.Speech.Application.Exceptions;
//using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
//using LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser;
//using LogCorner.EduSync.Speech.Domain.IRepository;
//using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
//using LogCorner.EduSync.Speech.Resiliency;
//using LogCorner.EduSync.Speech.Telemetry;
//using Moq;
//using OpenTelemetry.Context.Propagation;
//using Polly;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Threading.Tasks;
//using LogCorner.EduSync.Speech.Application.EventSourcing;
//using Xunit;

//namespace LogCorner.EduSync.Speech.Application.UnitTests.Specs
//{
//    public class EventSourcingHandlerUnitTest
//    {
//        [Fact(DisplayName = "Handle with null events should raise EventNullException")]
//        public async Task HandleWithNullEventsShouldRaiseEventNullException()
//        {
//            //Arrange
//            var mockEventStoreRepository = new Mock<IEventStoreRepository>();
//            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();

//            var mockEventSerializer = new Mock<IEventSerializer>();

//            long version = 0;

//            //Act
//            //Assert
//            var sut = new EventSourcingHandler(moqUnitOfWork.Object, mockEventStoreRepository.Object, mockEventSerializer.Object, It.IsAny<ISignalRPublisher>(), It.IsAny<ITraceService>(), It.IsAny<IResiliencyService>());
//            await Assert.ThrowsAsync<EventNullException>(() => sut.Handle(null, version));
//        }

//        [Fact(DisplayName = "Handle with events should call AppendAsync")]
//        public async Task HandleWithEventsShouldCallAppendAsync()
//        {
//            //Arrange
//            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();
//            moqUnitOfWork.Setup(m => m.Commit()).Verifiable();
//            var mockEventStoreRepository = new Mock<IEventStoreRepository>();
//            mockEventStoreRepository.Setup(e => e.AppendAsync(It.IsAny<EventStore>()))
//                .Returns(Task.FromResult(true))
//                .Verifiable();

//            var mockEventSerializer = new Mock<IEventSerializer>();

//            var @event = new SpeechCreatedEvent(It.IsAny<Guid>(), It.IsAny<string>(),
//                It.IsAny<string>(), It.IsAny<string>(),
//                It.IsAny<SpeechTypeEnum>());

//            long version = 0;

//            var moqSignalRPublisher = new Mock<ISignalRPublisher>();
//            moqSignalRPublisher.Setup(p => p.SubscribeAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

//            moqSignalRPublisher.Setup(p => p.PublishAsync(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<EventStore>())).Returns(Task.CompletedTask);

//            // moqSignalRPublisher.Protected().Setup<Task>("PublishAsync", ItExpr.IsAny<string>(), ItExpr.IsAny<EventStore>()).Returns(Task.CompletedTask);

//            var mockTraceService = new Mock<ITraceService>();
//            mockTraceService.Setup(m => m.AddActivityToHeader(It.IsAny<Activity>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<TextMapPropagator>())).Verifiable();

//            var mockResiliencyService = new Mock<IResiliencyService>();
//            mockResiliencyService.SetupAllProperties();
//            mockResiliencyService.Object.ExponentialExceptionRetry = Policy.NoOpAsync();
//            //Act
//            var sut = new EventSourcingHandler(moqUnitOfWork.Object, mockEventStoreRepository.Object,
//                mockEventSerializer.Object, moqSignalRPublisher.Object, mockTraceService.Object, mockResiliencyService.Object);
//            await sut.Handle(@event, version);

//            //Assert
//            mockEventStoreRepository.Verify(m => m.AppendAsync(It.IsAny<EventStore>()),
//                Times.Once, "AppendAsync must be called only once");
//        }

//        [Fact(DisplayName = "Handle with events should call AppendAsync")]
//        public async Task HandleWithEventsShouldCallCommitAsync()
//        {
//            //Arrange
//            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();
//            moqUnitOfWork.Setup(m => m.Commit()).Verifiable();
//            var mockEventStoreRepository = new Mock<IEventStoreRepository>();
//            mockEventStoreRepository.Setup(e => e.AppendAsync(It.IsAny<EventStore>()))
//                .Returns(Task.FromResult(true))
//                .Verifiable();

//            var mockEventSerializer = new Mock<IEventSerializer>();
//            var mockTraceService = new Mock<ITraceService>();
//            mockTraceService.Setup(m => m.AddActivityToHeader(It.IsAny<Activity>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<TextMapPropagator>())).Verifiable();

//            //var mockResiliencyService = new Mock<IResiliencyService>();
//            //mockResiliencyService.Setup(m => m.ExponentialExceptionRetry).Returns(

//            //    new MockResiliencyService().ExponentialExceptionRetry

//            //);

//            var mockResiliencyService = new Mock<IResiliencyService>();
//            mockResiliencyService.SetupAllProperties();
//            mockResiliencyService.Object.ExponentialExceptionRetry = Policy.NoOpAsync();

//            //mockResiliencyService.SetupAllProperties();
//            //mockResiliencyService.Object.ExponentialExceptionRetry = Policy.NoOpAsync()

//            var @event = new SpeechCreatedEvent(It.IsAny<Guid>(), It.IsAny<string>(),
//                It.IsAny<string>(), It.IsAny<string>(),
//                It.IsAny<SpeechTypeEnum>());

//            long version = 0;

//            var moqSignalRPublisher = new Mock<ISignalRPublisher>();
//            moqSignalRPublisher.Setup(p => p.SubscribeAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

//            moqSignalRPublisher.Setup(p => p.PublishAsync(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<EventStore>())).Returns(Task.CompletedTask);

//            //Act
//            var sut = new EventSourcingHandler(moqUnitOfWork.Object, mockEventStoreRepository.Object,
//                mockEventSerializer.Object, moqSignalRPublisher.Object, mockTraceService.Object, mockResiliencyService.Object);
//            await sut.Handle(@event, version);

//            //Assert
//            // Verify that SaveChanges is called
//            moqUnitOfWork.Verify(m => m.Commit(), Times.Once, "Commit must be called only once");
//        }
//    }
//}