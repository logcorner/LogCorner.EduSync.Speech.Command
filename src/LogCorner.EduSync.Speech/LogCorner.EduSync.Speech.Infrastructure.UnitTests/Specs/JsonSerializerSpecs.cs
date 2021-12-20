using LogCorner.EduSync.Speech.Command.SharedKernel.Events;
using LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser;
using Moq;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTests.Specs
{
    public class JsonSerializerSpecs
    {
        [Fact(DisplayName = "Deserialize Event Stream should return an event")]
        public void DeserializeEventStreamWithTypeShouldReturnAnEvent()
        {
            //Arrange
            var evt = new EventStub(12);
            var json = @"{
                            'Id': '12',
                            'MediaFileId': '26443db2-1ca1-4f19-bd34-98654a61741c',
                            'AggregateId': '735f8407-16be-44b5-be96-2bab582b5298',
                            'EventId': 'b6c73140-684b-447c-b8fd-9c78c975dc38',
                            'AggregateVersion': 1,
                            'OcurrendOn': '2019-09-08T10:55:48.5529384Z'
                         }";
            Mock<IJsonProvider> moqJsonProvider = new Mock<IJsonProvider>();
            moqJsonProvider.Setup(m => m.DeserializeObject<EventStub>(It.IsAny<string>(),
                evt.GetType().AssemblyQualifiedName)).Returns(evt);

            //Act
            var sut = new JsonSerializer(moqJsonProvider.Object);

            var result = sut.Deserialize<EventStub>(evt.GetType().AssemblyQualifiedName, json);
            //Assert
            Assert.Equal(evt, result);
        }

        [Fact(DisplayName = "Deserialize Event Stream should return an event")]
        public void DeserializeEventStreamShouldReturnAnEvent()
        {
            //Arrange
            var evt = new EventStub(12);
            var json = @"{
                            'Id': '12',
                            'MediaFileId': '26443db2-1ca1-4f19-bd34-98654a61741c',
                            'AggregateId': '735f8407-16be-44b5-be96-2bab582b5298',
                            'EventId': 'b6c73140-684b-447c-b8fd-9c78c975dc38',
                            'AggregateVersion': 1,
                            'OcurrendOn': '2019-09-08T10:55:48.5529384Z'
                         }";
            Mock<IJsonProvider> moqJsonProvider = new Mock<IJsonProvider>();
            moqJsonProvider.Setup(m => m.DeserializeObject<EventStub>(It.IsAny<string>()
                )).Returns(evt);

            //Act
            var sut = new JsonSerializer(moqJsonProvider.Object);

            var result = sut.Deserialize<EventStub>(json);
            //Assert
            Assert.Equal(evt, result);
        }

        [Fact(DisplayName = "Serialize event stream should return a string")]
        public void SerializeEventStreamShouldReturnAString()
        {
            //Arrange
            var evt = new EventStub(12);
            var json = @"{
                            'Id': '12',
                            'MediaFileId': '26443db2-1ca1-4f19-bd34-98654a61741c',
                            'AggregateId': '735f8407-16be-44b5-be96-2bab582b5298',
                            'EventId': 'b6c73140-684b-447c-b8fd-9c78c975dc38',
                            'AggregateVersion': 1,
                            'OcurrendOn': '2019-09-08T10:55:48.5529384Z'
                         }";
            Mock<IJsonProvider> moqJsonProvider = new Mock<IJsonProvider>();
            moqJsonProvider.Setup(m => m.SerializeObject(It.IsAny<Event>())).Returns(json);

            //Act
            var sut = new JsonSerializer(moqJsonProvider.Object);

            var result = sut.Serialize<EventStub>(evt);
            //Assert
            Assert.Equal(json, result);
        }
    }
}