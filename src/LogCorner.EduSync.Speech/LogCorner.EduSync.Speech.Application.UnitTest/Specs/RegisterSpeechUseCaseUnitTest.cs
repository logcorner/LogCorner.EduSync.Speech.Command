using LogCorner.EduSync.Speech.Application.Exceptions;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Moq;
using System.Threading.Tasks;
using LogCorner.EduSync.Speech.Application.Commands;
using Xunit;

namespace LogCorner.EduSync.Speech.Application.UnitTest.Specs
{
    public class RegisterSpeechUseCaseUnitTest
    {
        [Fact(DisplayName = "register speech use case with valid input return success")]
        public async Task RegisterSpeechUseCaseWithValidInputReturnSuccessTest()
        {
            //Arrange
            /* ------------ I will use UnitOfWork pattern, it will help me to treat aggregate roots
                            as a unit for the purpose of data changes */
            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();
            moqUnitOfWork.Setup(m => m.Commit()).Verifiable();

            /* ------------ I will use repository pattern, aggregate roots are the only objects my
                            code loads from the repository.*/
            Mock<ISpeechRepository> moqSpeechRepository = new Mock<ISpeechRepository>();
            moqSpeechRepository.Setup(m => m.CreateAsync(It.IsAny<Domain.SpeechAggregate.Speech>()))
                .Returns(Task.FromResult<ISpeechRepository>(null)).Verifiable();

            // ------------ I'm on the command side of CQRS pattern, so I don't need an output port
            // ------------ I need a command to regsiter a new speech
            var registerSpeechCommand = new RegisterSpeechCommandMessage(
                "Microservices getting started",
                "A Microservices from scratch online event Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took rem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                "http://microservices-getting-started.logcorner.com",
                "2");

            var mockEventSourcingSubscriber = new Mock<IEventSourcingSubscriber>();
            //Act
            // ------------ RegisterSpeechUseCase is the object under test
            IRegisterSpeechUseCase usecase =
                new SpeechUseCase(moqUnitOfWork.Object, moqSpeechRepository.Object, mockEventSourcingSubscriber.Object, It.IsAny<IEventStoreRepository<Domain.SpeechAggregate.Speech>>());

            await usecase.Handle(registerSpeechCommand);

            //Assert
            /* ------------ The object returns void , so I will verify that a new Speech will be inserted into the database
                            when SaveChanges is called.*/

            moqSpeechRepository.Verify(m => m.CreateAsync(It.IsAny<Domain.SpeechAggregate.Speech>()), Times.Once,
                "CreateAsync must be called only once");

            // Verify that SaveChanges is called
            moqUnitOfWork.Verify(m => m.Commit(), Times.Once, "Commit must be called only once");
        }

        [Fact(DisplayName = "register speech use case with null speech throws an ApplicationArgumentNullException")]
        public async Task RegisterSpeechUseCaseWithNullSpeechThrowsExceptionTest()
        {
            //Arrange
            Mock<IUnitOfWork> moqUnitOfWork = new Mock<IUnitOfWork>();

            Mock<ISpeechRepository> moqSpeechRepository = new Mock<ISpeechRepository>();
            var mockEventSourcingSubscriber = new Mock<IEventSourcingSubscriber>();

            //Act
            IRegisterSpeechUseCase usecase = new SpeechUseCase(moqUnitOfWork.Object, moqSpeechRepository.Object,
                mockEventSourcingSubscriber.Object, It.IsAny<IEventStoreRepository<Domain.SpeechAggregate.Speech>>());

            //Assert
            await Assert.ThrowsAsync<ArgumentNullApplicationException>(() => usecase.Handle(null));
        }
    }
}