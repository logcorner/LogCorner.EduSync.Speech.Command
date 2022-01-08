using LogCorner.EduSync.Speech.Application.Commands;
using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Application.Interfaces;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Domain;
using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LogCorner.EduSync.Speech.Application.UnitTests.Specs
{
    public class DeleteSpeechUseCaseUnitTest
    {
        [Fact(DisplayName = "Handling Delete  when command is null  should raise ApplicationArgumentNullException")]
        public async Task HandlingDeleteWhenCommandIsNullShouldRaiseApplicationArgumentNullException()
        {
            //Arrange

            Mock<ISpeechRepository> moqSpeechRepository = new Mock<ISpeechRepository>();
            var mockEventSourcingSubscriber = new Mock<IEventSourcingSubscriber>();

            //Act
            IDeleteSpeechUseCase usecase = new SpeechUseCase(moqSpeechRepository.Object,
                mockEventSourcingSubscriber.Object, It.IsAny<IEventStoreRepository>());

            //Assert
            await Assert.ThrowsAsync<ArgumentNullApplicationException>(() => usecase.Handle(null));
        }

        [Fact(DisplayName = "Handling Delete  when speech does not exist should raise ApplicationNotFoundException")]
        public async Task HandlingDeleteWhenSpeechDoesNotExistShouldRaiseApplicationNotFoundException()
        {
            //Arrange
            DeleteSpeechCommandMessage command = new DeleteSpeechCommandMessage(
                Guid.Empty, 0);
            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();

            Mock<ISpeechRepository> moqSpeechRepository = new Mock<ISpeechRepository>();

            var mockEventSourcingSubscriber = new Mock<IEventSourcingSubscriber>();

            Mock<IEventStoreRepository> moqEventStoreRepository =
                new Mock<IEventStoreRepository>();

            moqEventStoreRepository.Setup(m => m.GetByIdAsync<Domain.SpeechAggregate.Speech>(command.SpeechId))
                .Returns(Task.FromResult((Domain.SpeechAggregate.Speech)null));

            IDeleteSpeechUseCase usecase = new SpeechUseCase(moqSpeechRepository.Object,
                mockEventSourcingSubscriber.Object, moqEventStoreRepository.Object);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundApplicationException>(() => usecase.Handle(command));
        }

        [Fact(DisplayName = "Handling Delete when Expected version is not equal to aggregate version should raise ConcurrencyException")]
        public async Task HandlingDeleteWhenExpectedVersionIsNotEqualToAggregateVersionShouldRaiseConcurrencyException()
        {
            //Arrange
            var id = Guid.NewGuid();
            string title = @"Lorem Ipsum is simply dummy text";
            string description = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book";
            DeleteSpeechCommandMessage command = new DeleteSpeechCommandMessage(id, 2);

            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();
            var mockEventSourcingSubscriber = new Mock<IEventSourcingSubscriber>();
            Mock<ISpeechRepository> moqSpeechRepository = new Mock<ISpeechRepository>();

            var speech = new Domain.SpeechAggregate.Speech(id,
                new Title(title), new UrlValue("http://mysite.com"),
                new Description(description), SpeechType.Conferences);

            Mock<IEventStoreRepository> moqEventStoreRepository =
                new Mock<IEventStoreRepository>();
            moqEventStoreRepository.Setup(m =>
                    m.GetByIdAsync<Domain.SpeechAggregate.Speech>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(speech));

            IDeleteSpeechUseCase usecase = new SpeechUseCase(moqSpeechRepository.Object,
                mockEventSourcingSubscriber.Object, moqEventStoreRepository.Object);
            //Act
            //Assert
            await Assert.ThrowsAsync<ConcurrencyException>(() => usecase.Handle(command));
        }

        [Fact(DisplayName = "Delete speech use case with valid input return success")]
        public async Task DeleteSpeechUseCaseWithValidInputReturnSuccessTest()
        {
            //Arrange
            long orignalVersion = 0;
            var id = Guid.NewGuid();
            string title = @"Lorem Ipsum is simply dummy text";
            string description = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book";
            var speech = new Domain.SpeechAggregate.Speech(id,
                new Title(title), new UrlValue("http://mysite.com"),
                new Description(description), SpeechType.Conferences);

            /* ------------ I will use repository pattern, aggregate roots are the only objects my
                            code loads from the repository.*/
            Mock<ISpeechRepository> moqSpeechRepository = new Mock<ISpeechRepository>();
            moqSpeechRepository.Setup(m => m.DeleteAsync(It.IsAny<Domain.SpeechAggregate.Speech>()))
                .Returns(Task.FromResult<ISpeechRepository>(null)).Verifiable();

            var mockEventStoreRepository = new Mock<IEventStoreRepository>();
            mockEventStoreRepository.Setup(m => m.GetByIdAsync<Domain.SpeechAggregate.Speech>(It.IsAny<Guid>())).Returns(Task.FromResult(speech));

            // ------------ I'm on the command side of CQRS pattern, so I don't need an output port
            // ------------ I need a command to delete a new speech
            var deleteSpeechCommand = new DeleteSpeechCommandMessage(id, orignalVersion);

            var mockEventSourcingSubscriber = new Mock<IEventSourcingSubscriber>();
            //Act
            // ------------ DeleteSpeechUseCase is the object under test
            IDeleteSpeechUseCase usecase = new SpeechUseCase(moqSpeechRepository.Object, mockEventSourcingSubscriber.Object, mockEventStoreRepository.Object);

            await usecase.Handle(deleteSpeechCommand);

            //Assert
            /* ------------ The object returns void , so I will verify that a new Speech will be inserted into the database
                            when SaveChanges is called.*/

            moqSpeechRepository.Verify(m => m.DeleteAsync(It.IsAny<Domain.SpeechAggregate.Speech>()), Times.Once,
                "DeleteAsync must be called only once");
        }
    }
}