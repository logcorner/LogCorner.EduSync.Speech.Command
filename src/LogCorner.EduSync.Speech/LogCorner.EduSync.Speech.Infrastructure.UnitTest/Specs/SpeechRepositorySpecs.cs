using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure.Exceptions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
{
    public class SpeechRepositorySpecs
    {
        [Fact(DisplayName = @"Verify that CreateAsync can be called on SpeechRepository
                             and fire Repository.CreateAsync only once")]
        public async Task Verify_that_CreateAsync_can_be_called_on_SpeechRepository_and_fire_RepositoryCreateAsync_only_once()
        {
            //Arrange
            Mock<IRepository<Domain.SpeechAggregate.Speech, Guid>> mockRepository =
                new Mock<IRepository<Domain.SpeechAggregate.Speech, Guid>>();
            mockRepository.Setup(x => x.CreateAsync(It.IsAny<Domain.SpeechAggregate.Speech>()))
                .Returns(Task.CompletedTask)
                .Callback<Domain.SpeechAggregate.Speech>(x => { });

            ISpeechRepository sut = new SpeechRepository(mockRepository.Object);

            //Act
            await sut.CreateAsync(It.IsAny<Domain.SpeechAggregate.Speech>());

            //Assert
            mockRepository.Verify(x => x.CreateAsync(It.IsAny<Domain.SpeechAggregate.Speech>()), Times.Once);
        }

        [Fact(DisplayName = "Handling Update when Speech is null  should raise RepositoryArgumentNullException")]
        public async Task HandlingUpdateWhenSpeechIsNullShouldRaiseRepositoryArgumentNullException()
        {
            //Arrange
            Mock<IRepository<Domain.SpeechAggregate.Speech, Guid>> mockRepository =
                new Mock<IRepository<Domain.SpeechAggregate.Speech, Guid>>();

            ISpeechRepository sut = new SpeechRepository(mockRepository.Object);
            //Act
            //Assert
            await Assert.ThrowsAsync<RepositoryArgumentNullException>(() => sut.UpdateAsync(null));
        }
    }
}