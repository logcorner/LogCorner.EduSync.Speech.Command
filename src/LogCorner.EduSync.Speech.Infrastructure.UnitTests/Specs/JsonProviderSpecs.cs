using LogCorner.EduSync.Speech.Command.SharedKernel.Serialyser;
using System.Text.Json;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTests.Specs
{
    public class JsonProviderSpecs
    {
        [Fact(DisplayName = "given string DeserializeObject should return object")]
        public void GivenStringDeserializeObjectWithTypeShouldReturnObject()
        {
            //Arrange

            var json = @"
                {
                    ""Id"" :1,
                    ""Name"":""Dupont""
                }";
            var obj = new ObjectToDeserializeTo(1, "Dupont");
            var type = obj.GetType().AssemblyQualifiedName;

            //Act
            IJsonProvider sut = new JsonDotNetProvider();
            var result = sut.DeserializeObject<ObjectToDeserializeTo>(json, type);
            //Assert

            Assert.Equal(obj.Id, result.Id);
            Assert.Equal(obj.Name, result.Name);
        }

        [Fact(DisplayName = "given event object serializeobject should return a string")]
        public void GivenEventObjectSerializeObjectShouldReturnString()
        {
            //Arrange

            var obj = new ObjectToDeserializeTo(1, "Dupont");
            string json = JsonSerializer.Serialize(obj);

            //Act
            IJsonProvider sut = new JsonDotNetProvider();
            var result = sut.SerializeObject(obj);

            //Assert

            Assert.Equal(json, result);
        }

        [Fact(DisplayName = "given string DeserializeObject should return object")]
        public void GivenStringDeserializeObjectShouldReturnObject()
        {
            //Arrange

            var json = @" {
                    ""Id"" :1,
                    ""Name"":""Dupont""
                }";
            var obj = new ObjectToDeserializeTo(1, "Dupont");

            //Act
            IJsonProvider sut = new JsonDotNetProvider();
            var result = sut.DeserializeObject<ObjectToDeserializeTo>(json);
            //Assert

            Assert.Equal(obj.Id, result.Id);
            Assert.Equal(obj.Name, result.Name);
        }
    }
}