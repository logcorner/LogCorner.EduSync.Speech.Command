using System;
using System.Threading.Tasks;
using LogCorner.EduSync.Speech.Domain;
using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTests.Specs
{
    public class SpeechRepositorySpecs
    {
        [Fact(DisplayName = @"Verify that CreateAsync can be called on SpeechRepository  and fire Repository.CreateAsync only once")]
        public async Task Verify_that_CreateAsync_can_be_called_on_SpeechRepository_and_fire_RepositoryCreateAsync_only_once()
        {
            //Arrange
            Mock<IRepository<Domain.SpeechAggregate.Speech, Guid>> mockRepository =
                new Mock<IRepository<Domain.SpeechAggregate.Speech, Guid>>();
            mockRepository.Setup(x => x.CreateAsync(It.IsAny<Domain.SpeechAggregate.Speech>()))
                .Returns(Task.CompletedTask)
                .Callback<Domain.SpeechAggregate.Speech>(x => { });

            ISpeechRepository sut = new SpeechRepository(mockRepository.Object, It.IsAny<DataBaseContext>());

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

            ISpeechRepository sut = new SpeechRepository(mockRepository.Object, It.IsAny<DataBaseContext>());

            //Act
            //Assert
            await Assert.ThrowsAsync<ArgumentNullRepositoryException>(() => sut.UpdateAsync(null));
        }

        [Fact(DisplayName = "Handling Update when the speech  is valid and exist should perform update")]
        public async Task HandlingUpdateWhenTheSpeechIsValidAndExistShouldPerformUpdate()
        {
            //Arrange
            Guid speechId = Guid.NewGuid();
            string newTitle = @"New Lorem Ipsum is simply dummy text";
            string description = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book";
            var speech = new Domain.SpeechAggregate.Speech(speechId,
                new Title(newTitle), new UrlValue("http://mysite.com"), new Description(description),
                SpeechType.Conferences);

            Mock<IRepository<Domain.SpeechAggregate.Speech, Guid>> mockRepository =
                new Mock<IRepository<Domain.SpeechAggregate.Speech, Guid>>();

            DbContextOptionsBuilder<DataBaseContext> optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            optionsBuilder.UseInMemoryDatabase("FakeInMemoryData");
            var context = new DataBaseContext(optionsBuilder.Options);
            //context.Speech.Add(speechToUpdate);
            //context.SaveChanges();
            ISpeechRepository sut = new SpeechRepository(mockRepository.Object, context);

            //Act
            await sut.UpdateAsync(speech);

            //Assert
            var result = context.Entry(speech).Entity.Title.Value;
            Assert.Equal(newTitle, result);
        }

        [Fact(DisplayName = @"Verify that UpdateAsync can be called on SpeechRepository and fire Repository.UpdateAsync only once")]
        public async Task Verify_that_UpdateAsync_can_be_called_on_SpeechRepository_and_fire_RepositoryUpdateAsync_only_once()
        {
            //Arrange

            Guid speechId = Guid.NewGuid();
            string newTitle = @"New Lorem Ipsum is simply dummy text";
            string description = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book";
            var speech = new Domain.SpeechAggregate.Speech(speechId,
                new Title(newTitle), new UrlValue("http://mysite.com"), new Description(description),
                SpeechType.Conferences);

            Mock<IRepository<Domain.SpeechAggregate.Speech, Guid>> mockRepository =
                new Mock<IRepository<Domain.SpeechAggregate.Speech, Guid>>();

            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Domain.SpeechAggregate.Speech>()))
                .Returns(Task.CompletedTask)
                .Callback<Domain.SpeechAggregate.Speech>(x => { });

            var options = new DbContextOptionsBuilder<DataBaseContext>().UseInMemoryDatabase(databaseName: "InMemoryDB").Options;
            var context = new DataBaseContext(options);
            await context.Speech.AddAsync(speech);

            ISpeechRepository sut = new SpeechRepository(mockRepository.Object, context);

            //Act
            await sut.DeleteAsync(speech);

            //Assert
            mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Domain.SpeechAggregate.Speech>()), Times.Once);
        }
    }
}