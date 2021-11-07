using LogCorner.EduSync.Speech.SharedKernel.Serialyser;
using Newtonsoft.Json;
using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
{
    public class JsonProviderSpecs
    {
        [Fact(DisplayName = "given string DeserializeObject should return object")]
        public void GivenStringDeserializeObjectWithTypeShouldReturnObject()
        {
            //Arrange

            var json = @"
                {
                    'Id' :1,
                    'Name':'Dupont'
                }";
            var obj = new ObjectToDeserializeTo(1, "Dupont");
            var type = obj.GetType().AssemblyQualifiedName;

            //Act
            IJsonProvider sut = new JsonProvider();
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
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            //Act
            IJsonProvider sut = new JsonProvider();
            var result = sut.SerializeObject(obj);

            //Assert

            Assert.Equal(json, result);
        }

        [Fact(DisplayName = "given string DeserializeObject should return object")]
        public void GivenStringDeserializeObjectShouldReturnObject()
        {
            //Arrange

            var json = @"
                {
                    'Id' :1,
                    'Name':'Dupont'
                }";
            var obj = new ObjectToDeserializeTo(1, "Dupont");

            //Act
            IJsonProvider sut = new JsonProvider();
            var result = sut.DeserializeObject<ObjectToDeserializeTo>(json);
            //Assert

            Assert.Equal(obj.Id, result.Id);
            Assert.Equal(obj.Name, result.Name);
        }
    }
}