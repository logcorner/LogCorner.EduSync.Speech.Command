using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LogCorner.EduSync.Speech.Application.UnitTest
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
            await Assert.ThrowsAsync<ApplicationArgumentNullException>(() => usecase.Handle(null));
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
            await Assert.ThrowsAsync<ApplicationNotFoundException>(() => usecase.Handle(command));
        }
    }
}