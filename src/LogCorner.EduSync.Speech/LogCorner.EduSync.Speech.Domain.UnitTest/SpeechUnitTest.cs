using LogCorner.EduSync.Speech.Domain.Events;
using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace LogCorner.EduSync.Speech.Domain.UnitTest
{
    public class SpeechUnitTest
    {
        [Fact]
        public void RegisterSpeechWithTitleLessThanNCaractersThrowsDomainException()
        {
            //Arrange
            var title = "abc";

            //Act
            //Assert
            Assert.Throws<InvalidLenghtAggregateException>(() => new SpeechAggregate.Speech(new Title(title),
                It.IsAny<UrlValue>(),
                It.IsAny<Description>(),
                It.IsAny<SpeechType>()));
        }

        [Fact]
        public void RegisterSpeechWithTitleMoreThanNCaractersThrowsDomainException()
        {
            //Arrange
            var title = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum";

            //Act
            //Assert
            Assert.Throws<InvalidLenghtAggregateException>(() => new SpeechAggregate.Speech(new Title(title),
                It.IsAny<UrlValue>(),
                It.IsAny<Description>(),
                It.IsAny<SpeechType>()));
        }

        [Fact]
        public void RegisterSpeechWithTitleNullThrowsDomainException()
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ArgumentNullAggregateException>(() => new SpeechAggregate.Speech(null, It.IsAny<UrlValue>(),
                It.IsAny<Description>(),
                It.IsAny<SpeechType>()));
        }

        [Fact]
        public void RegisterSpeechWithDescriptionLessThanNCaractersThrowsDomainException()
        {
            //Arrange
            var description = "abc";

            //Act
            //Assert
            Assert.Throws<InvalidLenghtAggregateException>(() => new SpeechAggregate.Speech(It.IsAny<Title>(),
                It.IsAny<UrlValue>(),
                new Description(description),
                It.IsAny<SpeechType>()));
        }

        [Fact]
        public void RegisterSpeechWithDescriptionMoreThanNCaractersThrowsDomainException()
        {
            //Arrange
            var description = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum";

            //Act
            //Assert
            Assert.Throws<InvalidLenghtAggregateException>(() => new SpeechAggregate.Speech(It.IsAny<Title>(),
                It.IsAny<UrlValue>(),
                new Description(description),
                It.IsAny<SpeechType>()));
        }

        [Fact]
        public void RegisterSpeechWithDescriptionNullThrowsDomainException()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            //Act
            //Assert
            Assert.Throws<ArgumentNullAggregateException>(() => new SpeechAggregate.Speech(title, It.IsAny<UrlValue>(),
                null,
                It.IsAny<SpeechType>()));
        }

        [Fact]
        public void RegisterSpeechWithNullUrlThrowsDomainException()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");

            //Act
            //Assert
            Assert.Throws<ArgumentNullAggregateException>(() => new SpeechAggregate.Speech(title, It.IsAny<UrlValue>(),
                It.IsAny<Description>(),
                It.IsAny<SpeechType>()));
        }

        [Fact]
        public void RegisterSpeechWithEmptyUrlThrowsDomainException()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");

            //Act
            //Assert
            Assert.Throws<InvalidLenghtAggregateException>(() => new SpeechAggregate.Speech(title, new UrlValue(string.Empty),
                It.IsAny<Description>(),
                It.IsAny<SpeechType>()));
        }

        [Fact]
        public void RegisterSpeechWithInvalidUrlThrowsDomainException()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");

            //Act
            //Assert
            Assert.Throws<InvalidUrlAggregateException>(() => new SpeechAggregate.Speech(title, new UrlValue("thisIsnotAValiUrl"),
                It.IsAny<Description>(),
                It.IsAny<SpeechType>()));
        }

        [Fact]
        public void RegisterSpeechWithNullTypeThrowsDomainException()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var url = new UrlValue("http://url.com");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            //Act
            //Assert
            Assert.Throws<ArgumentNullAggregateException>(() => new SpeechAggregate.Speech(title, url,
                description,
                null));
        }

        [Fact]
        public void RegisterSpeechWithInvalidTypeThrowsDomainException()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");
            //Act
            //Assert
            Assert.Throws<InvalidEnumAggregateException>(() => new SpeechAggregate.Speech(title, url,
                description,
                new SpeechType("patati")));
        }

        [Theory]
        [ClassData(typeof(SpeechTypeTestData))]
        public void RegisterSpeechWithValidDataReturnsSuccess(SpeechType speechType)
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            //Act
            var speech = new SpeechAggregate.Speech(title, url, description, speechType);

            //Assert
            Assert.Equal(title.Value, speech.Title.Value);
            Assert.Equal(description.Value, speech.Description.Value);
            Assert.Equal(url.Value, speech.Url.Value);
            Assert.Equal(speechType.Value, speech.Type.Value);
        }

        [Theory]
        [ClassData(typeof(SpeechTypeTestData))]
        public void RegisterSpeechWithValidDataRaiseDomainEvent(SpeechType speechType)
        {
            //Arrange
            var id = Guid.NewGuid();
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            //Act
            var speech = new SpeechAggregate.Speech(id, title, url, description, speechType);
            var domainEvent = speech.DomainEvents.SingleOrDefault();
            var speechCreateEvent = (SpeechCreatedEvent)domainEvent;

            //Assert
            Assert.IsAssignableFrom<SpeechCreatedEvent>(domainEvent);
            Assert.NotNull(speechCreateEvent);
            Assert.Equal(id.ToString(), domainEvent.Id);
            Assert.Equal(url, speechCreateEvent.Url);
            Assert.Equal(title, speechCreateEvent.Title);
            Assert.Equal(description, speechCreateEvent.Description);
            Assert.Equal(speechType, speechCreateEvent.Type);
            Assert.True(DateTime.Now > speechCreateEvent.OcurrendOn);
        }

        [Theory]
        [ClassData(typeof(SpeechTypeTestData))]
        public void CreateMediaWithNullMediaFile(SpeechType speechType)
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            //Act
            var speech = new SpeechAggregate.Speech(title, url, description, speechType);
            //Assert
            Assert.Throws<ArgumentNullAggregateException>(() => speech.CreateMedia(null, 0));
        }

        [Fact]
        public void CreateMediaWithValidMediaFileRetunrSuccess()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");
            var file = new MediaFile(new UrlValue(
                "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE2ybMU?ver=c5fc&q=90&m=6&h=201&w=358&b=%23FFFFFFFF&l=f&o=t&aim=true"));

            //Act
            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);
            speech.CreateMedia(file, 1);
            var domainEvent = speech.DomainEvents.SingleOrDefault(s => s is MediaFileCreatedEvent);
            var mediaFileCreatedEvent = (MediaFileCreatedEvent)domainEvent;

            //Assert
            Assert.Contains(file, speech.MediaFileItems);

            Assert.NotNull(speech.MediaFileItems.Select(f => f.File));
            Assert.NotNull(mediaFileCreatedEvent);
            Assert.NotNull(mediaFileCreatedEvent.File);
            Assert.NotNull(mediaFileCreatedEvent.File.Value);
        }

        [Fact]
        public void CreateMediaWithValidVersionReturnSuccess()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");
            var file = new MediaFile(new UrlValue(
                "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE2ybMU?ver=c5fc&q=90&m=6&h=201&w=358&b=%23FFFFFFFF&l=f&o=t&aim=true"));

            //Act
            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);
            speech.CreateMedia(file, 1);
            var domainEvent = speech.DomainEvents.SingleOrDefault(s => s is MediaFileCreatedEvent);
            var mediaFileCreatedEvent = (MediaFileCreatedEvent)domainEvent;

            //Assert
            Assert.Contains(file, speech.MediaFileItems);

            Assert.NotNull(speech.MediaFileItems.Select(f => f.File));
            Assert.NotNull(domainEvent);
            Assert.True(domainEvent.Version == 2);
            Assert.True(speech.Version == 2);
            Assert.NotNull(mediaFileCreatedEvent);
            Assert.NotNull(mediaFileCreatedEvent.File);
            Assert.NotNull(mediaFileCreatedEvent.File.Value);
        }

        [Fact]
        public void CreateMediaWithInvalidVersion()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");
            var file = new MediaFile(new UrlValue(
                "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE2ybMU?ver=c5fc&q=90&m=6&h=201&w=358&b=%23FFFFFFFF&l=f&o=t&aim=true"));

            //Act
            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);
            Assert.Throws<ConcurrencyException>(() => speech.CreateMedia(file, 0));
        }

        [Fact]
        public void CreateMediaWithExistingMedaiReturnSuccess()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");
            var file = new MediaFile(new UrlValue(
                "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE2ybMU?ver=c5fc&q=90&m=6&h=201&w=358&b=%23FFFFFFFF&l=f&o=t&aim=true"));

            //Act
            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);
            speech.CreateMedia(file, 1);

            Assert.Throws<MediaFileAlreadyExisteDomainException>(() => speech.CreateMedia(file, 1));
            /*var domainEvent = speech.DomainEvents.SingleOrDefault(s => s is MediaFileCreatedEvent);
            var mediaFileCreatedEvent = (MediaFileCreatedEvent)domainEvent;

            //Assert
            Assert.Contains(file, speech.MediaFileItems);

            Assert.NotNull(speech.MediaFileItems.Select(f => f.File));
            Assert.NotNull(domainEvent);
            Assert.True(domainEvent.Version == 2);
            Assert.True(speech.Version == 2);
            Assert.NotNull(mediaFileCreatedEvent);
            Assert.NotNull(mediaFileCreatedEvent.File);
            Assert.NotNull(mediaFileCreatedEvent.File.Value);*/
        }
    }
}