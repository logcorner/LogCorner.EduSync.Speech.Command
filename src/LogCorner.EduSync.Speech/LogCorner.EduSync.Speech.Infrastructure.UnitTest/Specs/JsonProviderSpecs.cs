using Xunit;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
{
    public class JsonProviderSpecs
    {
        [Fact(DisplayName = "given string deserializeobject should return object")]
        public void GivenStringDeserializeObjectShouldReturnObject()
        {
            //Arrange

            var json = @"
                {
                    'Id' :1,
                    'Name':'Dupont'
                }";
            var obj = new EventOject(1, "Dupont");
            var type = obj.GetType().AssemblyQualifiedName;
            //Act
            IJsonProvider sut = new JsonProvider();
            var result = sut.DeserializeObject<EventOject>(json, type);
            //Assert

            Assert.Equal(obj.Id, result.Id);
            Assert.Equal(obj.Name, result.Name);
        }
    }
}