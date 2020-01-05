using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Domain;
using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LogCorner.EduSync.Speech.Application.UnitTest.Specs
{
    public class UpdateSpeechUseCaseUnitTest
    {
        [Fact(DisplayName = "Handling update  when command is null  should raise ApplicationArgumentNullException")]
        public async Task HandlingUpdateWhenCommandIsNullShouldRaiseApplicationArgumentNullException()
        {
            //Arrange
            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();

            Mock<ISpeechRepository> moqSpeechRepository = new Mock<ISpeechRepository>();
            var mockEventSourcingSubscriber = new Mock<IEventSourcingSubscriber>();

            //Act
            IUpdateSpeechUseCase usecase = new RegisterSpeechUseCase(moqUnitOfWork.Object, moqSpeechRepository.Object,
                mockEventSourcingSubscriber.Object, It.IsAny<IEventStoreRepository<Domain.SpeechAggregate.Speech>>());

            //Assert
            await Assert.ThrowsAsync<ArgumentNullApplicationException>(() => usecase.Handle(null));
        }

        [Fact(DisplayName = "Handling update  when speech does not exist should raise ApplicationNotFoundException")]
        public async Task HandlingUpdateWhenSpeechDoesNotExistShouldRaiseApplicationNotFoundException()
        {
            //Arrange
            UpdateSpeechCommandMessage command = new UpdateSpeechCommandMessage(
                Guid.Empty, null, null, null, null, 0);
            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();

            Mock<ISpeechRepository> moqSpeechRepository = new Mock<ISpeechRepository>();

            var mockEventSourcingSubscriber = new Mock<IEventSourcingSubscriber>();

            Mock<IEventStoreRepository<Domain.SpeechAggregate.Speech>> moqEventStoreRepository =
                new Mock<IEventStoreRepository<Domain.SpeechAggregate.Speech>>();

            moqEventStoreRepository.Setup(m => m.GetByIdAsync<Domain.SpeechAggregate.Speech>(command.SpeechId))
                .Returns(Task.FromResult((Domain.SpeechAggregate.Speech)null));

            IUpdateSpeechUseCase usecase = new RegisterSpeechUseCase(moqUnitOfWork.Object, moqSpeechRepository.Object,
                mockEventSourcingSubscriber.Object, moqEventStoreRepository.Object);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundApplicationException>(() => usecase.Handle(command));
        }

        [Fact(DisplayName = "Handling Update when Command is not null should update speech Title")]
        public async Task HandlingUpdateWhenCommandIsNotNullShouldUpdateSpeechTitle()
        {
            //Arrange
            string title = @"Lorem Ipsum is simply dummy text";
            string newTitle = @"New Lorem Ipsum is simply dummy text";
            string description = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book";
            string url = "http://www.test.com";

            UpdateSpeechCommandMessage command = new UpdateSpeechCommandMessage(Guid.NewGuid(),
                newTitle, description, url, SpeechType.Conferences.Value.ToString(), 0);

            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();
            var mockEventSourcingSubscriber = new Mock<IEventSourcingSubscriber>();

            Mock<ISpeechRepository> moqSpeechRepository = new Mock<ISpeechRepository>();
            moqSpeechRepository.Setup(x =>
                    x.UpdateAsync(It.IsAny<Domain.SpeechAggregate.Speech>()))
                .Returns(Task.CompletedTask).Verifiable();

            var speech = new Domain.SpeechAggregate.Speech(Guid.NewGuid(),
                new Title(title), new UrlValue(url),
                new Description(description), SpeechType.Conferences);

            Mock<IEventStoreRepository<Domain.SpeechAggregate.Speech>> moqEventStoreRepository =
                new Mock<IEventStoreRepository<Domain.SpeechAggregate.Speech>>();
            moqEventStoreRepository.Setup(m =>
                    m.GetByIdAsync<Domain.SpeechAggregate.Speech>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(speech));

            IUpdateSpeechUseCase usecase = new RegisterSpeechUseCase(moqUnitOfWork.Object, moqSpeechRepository.Object,
                mockEventSourcingSubscriber.Object, moqEventStoreRepository.Object);

            //Act
            await usecase.Handle(command);

            //Assert
           // var type = (SpeechTypeEnum) Enum.Parse(typeof(SpeechTypeEnum), command.Type, true);
            moqSpeechRepository.Verify(m =>
                m.UpdateAsync(It.Is<Domain.SpeechAggregate.Speech>(n =>
                n.Id.Equals(speech.Id) 
             && n.Description.Value.Equals(command.Description, StringComparison.InvariantCultureIgnoreCase)
             && n.Title.Value.Equals(command.Title)
             && n.Url.Value.Equals(command.Url, StringComparison.InvariantCultureIgnoreCase)
             && n.Type.Equals(new SpeechType(command.Type))
            )), Times.Once);

            mockEventSourcingSubscriber.Verify(m =>
                m.Subscribe(It.IsAny<IEventSourcing>()), Times.Once);
            moqUnitOfWork.Verify(m => m.Commit(), Times.Once);
        }

        [Fact(DisplayName =
            "Handling Update when Expected version is not equal to aggregate version should raise ConcurrencyException")]
        public async Task HandlingUpdateWhenExpectedVersionIsNotEqualToAggregateVersionShouldRaiseConcurrencyException()
        {
            //Arrange
            string title = @"Lorem Ipsum is simply dummy text";
            string newTitle = @"New Lorem Ipsum is simply dummy text";
            string description = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book";
            string url = "http://www.test.com";
            UpdateSpeechCommandMessage command = new UpdateSpeechCommandMessage(Guid.NewGuid(),
                newTitle, description, url, SpeechType.Conferences.ToString(), 2);

            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();
            var mockEventSourcingSubscriber = new Mock<IEventSourcingSubscriber>();
            Mock<ISpeechRepository> moqSpeechRepository = new Mock<ISpeechRepository>();

            var speech = new Domain.SpeechAggregate.Speech(Guid.NewGuid(),
                new Title(title), new UrlValue("http://mysite.com"),
                new Description(description), SpeechType.Conferences);

            Mock<IEventStoreRepository<Domain.SpeechAggregate.Speech>> moqEventStoreRepository =
                new Mock<IEventStoreRepository<Domain.SpeechAggregate.Speech>>();
            moqEventStoreRepository.Setup(m =>
                    m.GetByIdAsync<Domain.SpeechAggregate.Speech>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(speech));

            IUpdateSpeechUseCase usecase = new RegisterSpeechUseCase(moqUnitOfWork.Object, moqSpeechRepository.Object,
                mockEventSourcingSubscriber.Object, moqEventStoreRepository.Object);
            //Act
            //Assert
            await Assert.ThrowsAsync<ConcurrencyException>(() => usecase.Handle(command));
        }
    }
}