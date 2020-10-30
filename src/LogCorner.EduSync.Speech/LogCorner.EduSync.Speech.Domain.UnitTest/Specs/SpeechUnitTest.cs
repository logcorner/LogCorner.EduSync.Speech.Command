using LogCorner.EduSync.Speech.Domain.Exceptions;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.SharedKernel.Events;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace LogCorner.EduSync.Speech.Domain.UnitTest.Specs
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
            var domainEvent = speech.GetUncommittedEvents().SingleOrDefault();
            var speechCreateEvent = (SpeechCreatedEvent)domainEvent;

            //Assert
            Assert.IsAssignableFrom<SpeechCreatedEvent>(domainEvent);
            Assert.NotNull(speechCreateEvent);
            Assert.Equal(id, speechCreateEvent.AggregateId);
            Assert.Equal(url.Value, speechCreateEvent.Url);
            Assert.Equal(title.Value, speechCreateEvent.Title);
            Assert.Equal(description.Value, speechCreateEvent.Description);
            Assert.Equal(speechType, new SpeechType(speechCreateEvent.Type.Name));
            Assert.True(DateTime.UtcNow < speechCreateEvent.OcurrendOn);
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
            var file = new MediaFile(Guid.NewGuid(), new UrlValue(
                "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE2ybMU?ver=c5fc&q=90&m=6&h=201&w=358&b=%23FFFFFFFF&l=f&o=t&aim=true"));

            //Act
            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);
            speech.CreateMedia(file, 0);
            var domainEvent = speech.GetUncommittedEvents().SingleOrDefault(s => s is MediaFileCreatedEvent);
            var mediaFileCreatedEvent = (MediaFileCreatedEvent)domainEvent;

            //Assert
            Assert.Contains(file, speech.MediaFileItems);

            Assert.NotNull(speech.MediaFileItems.Select(f => f.File));
            Assert.NotNull(mediaFileCreatedEvent);
            Assert.Equal(mediaFileCreatedEvent.AggregateId, speech.Id);
            Assert.Equal(mediaFileCreatedEvent.AggregateVersion, speech.Version);
        }

        [Fact]
        public void CreateMediaWithValidVersionReturnSuccess()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");
            var file = new MediaFile(Guid.NewGuid(), new UrlValue(
                "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE2ybMU?ver=c5fc&q=90&m=6&h=201&w=358&b=%23FFFFFFFF&l=f&o=t&aim=true"));

            //Act
            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);
            speech.CreateMedia(file, 0);
            var domainEvent = speech.GetUncommittedEvents().SingleOrDefault(s => s is MediaFileCreatedEvent);
            var mediaFileCreatedEvent = (MediaFileCreatedEvent)domainEvent;

            //Assert
            Assert.Contains(file, speech.MediaFileItems);

            Assert.NotNull(speech.MediaFileItems.Select(f => f.File));
            Assert.NotNull(domainEvent);
            Assert.True(speech.Version == 1);
            Assert.NotNull(mediaFileCreatedEvent);
            Assert.NotNull(mediaFileCreatedEvent.File);
            Assert.NotNull(mediaFileCreatedEvent.File);
        }

        [Fact]
        public void CreateMediaWithInvalidVersion()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");
            var file = new MediaFile(Guid.NewGuid(), new UrlValue(
                "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE2ybMU?ver=c5fc&q=90&m=6&h=201&w=358&b=%23FFFFFFFF&l=f&o=t&aim=true"));

            //Act
            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);
            Assert.Throws<ConcurrencyException>(() => speech.CreateMedia(file, -1));
        }

        [Fact]
        public void CreateMediaWithExistingMedaiShouldNotApplyEvent()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");
            var file = new MediaFile(Guid.NewGuid(), new UrlValue(
                "https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE2ybMU?ver=c5fc&q=90&m=6&h=201&w=358&b=%23FFFFFFFF&l=f&o=t&aim=true"));

            //Act
            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);
            speech.CreateMedia(file, 0);

            Assert.Throws<MediaFileAlreadyExistDomainException>(() => speech.CreateMedia(file, 0));
            var domainEvent = speech.GetUncommittedEvents().SingleOrDefault(s => s is MediaFileCreatedEvent);
            var mediaFileCreatedEvent = (MediaFileCreatedEvent)domainEvent;

            //Assert
            Assert.Contains(file, speech.MediaFileItems);

            Assert.NotNull(speech.MediaFileItems.Select(f => f.File));
            Assert.NotNull(domainEvent);
            Assert.True(mediaFileCreatedEvent.AggregateVersion == 1);
            Assert.Equal(Guid.Empty, mediaFileCreatedEvent.AggregateId);
        }

        [Fact]
        public void InstanciatingMediaFileWithPrivateConstrcutorShouldReturnNotNullInstance()
        {
            var instance = (MediaFile)typeof(MediaFile)
                  .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                      null,
                      new Type[0],
                      new ParameterModifier[0])
                  ?.Invoke(new object[0]);
            Assert.NotNull(instance);
            Assert.IsType<MediaFile>(instance);
        }

        #region changeTitle

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeTitleWhenTitleIsNullOrEmptyShouldRaiseArgumentNullAggregateException(string newTitle)
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<InvalidLenghtAggregateException>(() => speech.ChangeTitle(new Title(newTitle), It.IsAny<long>()));
        }

        [Fact]
        public void ChangeTitleWhenExpectedVersionIsNotEqualsToAggregateVersionShouldRaiseConcurrencyException()
        {
            //Arrange
            long expectedVersion = 1;
            string newTitle = "new Lorem Ipsum is simply dummy text of the printin";
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<ConcurrencyException>(() => speech.ChangeTitle(new Title(newTitle),
                expectedVersion));
        }

        [Fact]
        public void ChangeTitleWithValidArgumentsShouldApplySpeechTitleChangedEvent()
        {
            //Arrange
            long expectedVersion = 0;
            string newTitle = "new Lorem Ipsum is simply dummy text of the printin";
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            speech.ChangeTitle(new Title(newTitle), expectedVersion);

            //Assert
            Assert.Equal(newTitle, speech.Title.Value);
            Assert.Equal(description, speech.Description);
            Assert.Equal(url, speech.Url);
        }

        [Fact]
        public void ApplySpeechTitleChangedEventWithInvalidAggregateIdShouldRaiseInvalidDomainEventException()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<InvalidDomainEventException>(() => speech.Apply(new SpeechTitleChangedEvent(Guid.NewGuid(), It.IsAny<string>())));
        }

        #endregion changeTitle

        #region changeDescription

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeDescriptionWhenDescriptionIsNullOrEmptyShouldRaiseArgumentNullAggregateException(string newDescription)
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<InvalidLenghtAggregateException>(() => speech.ChangeDescription(new Description(newDescription), It.IsAny<long>()));
        }

        [Fact]
        public void ChangeDescriptionWhenExpectedVersionIsNotEqualsToAggregateVersionShouldRaiseConcurrencyException()
        {
            //Arrange
            long expectedVersion = 1;
            string newDescription = @" newLorem Ipsum is simply dummy text of the printing and typesetting industry.                                         Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took";
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<ConcurrencyException>(() => speech.ChangeDescription(new Description(newDescription),
                expectedVersion));
        }

        [Fact]
        public void ChangeDescriptionWithValidArgumentsShouldApplySpeechDescriptionChangedEvent()
        {
            //Arrange
            long expectedVersion = 0;
            string newDescription = "Nex desc Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took";
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            speech.ChangeDescription(new Description(newDescription), expectedVersion);

            //Assert
            Assert.Equal(title, speech.Title);
            Assert.Equal(newDescription, speech.Description.Value);
            Assert.Equal(url, speech.Url);
        }

        [Fact]
        public void ApplySpeechSpeechDescriptionChangedEventWithInvalidAggregateIdShouldRaiseInvalidDomainEventException()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<InvalidDomainEventException>(() => speech.Apply(new SpeechDescriptionChangedEvent(Guid.NewGuid(), It.IsAny<string>())));
        }

        #endregion changeDescription

        #region changeType

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeTypeWhenTitleIsNullOrEmptyShouldRaiseArgumentNullAggregateException(string newType)
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<InvalidEnumAggregateException>(() => speech.ChangeType(new SpeechType(newType), It.IsAny<long>()));
        }

        [Fact]
        public void ChangeTypeWhenExpectedVersionIsNotEqualsToAggregateVersionShouldRaiseConcurrencyException()
        {
            //Arrange
            long expectedVersion = 1;
            string newType = "Conferences";
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<ConcurrencyException>(() => speech.ChangeType(new SpeechType(newType), expectedVersion));
        }

        [Fact]
        public void ChangeTypeWithValidArgumentsShouldApplySpeechTypeChangedEvent()
        {
            //Arrange
            long expectedVersion = 0;
            string newType = "Conferences";
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            speech.ChangeType(new SpeechType(newType), expectedVersion);

            //Assert
            Assert.Equal(newType, speech.Type.StringValue);
            Assert.Equal(description, speech.Description);
            Assert.Equal(url, speech.Url);
        }

        [Fact]
        public void ApplySpeechTypeChangedEventWithInvalidAggregateIdShouldRaiseInvalidDomainEventException()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<InvalidDomainEventException>(() => speech.Apply(new SpeechTypeChangedEvent(Guid.NewGuid(), It.IsAny<LogCorner.EduSync.Speech.SharedKernel.Events.SpeechTypeEnum>())));
        }

        #endregion changeType

        #region changeUrl

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ChangeUrlWhenUrlIsNullOrEmptyShouldRaiseArgumentNullAggregateException(string newUrl)
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url_new.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<InvalidLenghtAggregateException>(() => speech.ChangeUrl(new UrlValue(newUrl), It.IsAny<long>()));
        }

        [Fact]
        public void ChangeUrlWhenExpectedVersionIsNotEqualsToAggregateVersionShouldRaiseConcurrencyException()
        {
            //Arrange
            long expectedVersion = 1;
            var newUrl = new UrlValue("http://url_new.com");
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<ConcurrencyException>(() => speech.ChangeUrl(newUrl, expectedVersion));
        }

        [Fact]
        public void ChangeUrlWithValidArgumentsShouldApplySpeechUrlChangedEvent()
        {
            //Arrange
            long expectedVersion = 0;
            string newUrl = "http://url_new.com";
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            speech.ChangeUrl(new UrlValue(newUrl), expectedVersion);

            //Assert
            Assert.Equal(title, speech.Title);
            Assert.Equal(description, speech.Description);
            Assert.Equal(newUrl, speech.Url.Value);
        }

        [Fact]
        public void ApplySpeechUrlChangedEventWithInvalidAggregateIdShouldRaiseInvalidDomainEventException()
        {
            //Arrange
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<InvalidDomainEventException>(() => speech.Apply(new SpeechUrlChangedEvent(Guid.NewGuid(), It.IsAny<string>())));
        }

        #endregion changeUrl

        #region delete Speech

        [Fact]
        public void DeleteSpeechWhenIdIsNotEqualsToAggregateIdShouldRaiseConcurrencyException()
        {
            //Arrange
            long expectedVersion = 1;
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<ConcurrencyException>(() => speech.Delete(It.IsAny<Guid>(), expectedVersion));
        }

        [Fact]
        public void DeleteSpeechWhenExpectedVersionIsNotEqualsToAggregateVersionShouldRaiseConcurrencyException()
        {
            //Arrange
            long expectedVersion = 1;
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(title, url, description, SpeechType.Conferences);

            //Act
            //Assert
            Assert.Throws<ConcurrencyException>(() => speech.Delete(It.IsAny<Guid>(), expectedVersion));
        }

        [Fact]
        public void DeleteSpeechWhenExpectedVersionIsEqualsToAggregateVersionShouldMarkSpeechAsDeleted()
        {
            //Arrange
            long expectedVersion = 0;
            var id = Guid.NewGuid();
            var title = new Title("Lorem Ipsum is simply dummy text of the printin");
            var description = new Description(@"Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                                              Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took ");
            var url = new UrlValue("http://url.com");

            var speech = new SpeechAggregate.Speech(id, title, url, description, SpeechType.Conferences);

            //Act
            speech.Delete(id, expectedVersion);

            //Assert

            Assert.True(speech.IsDeleted);
        }

        #endregion delete Speech
    }
}