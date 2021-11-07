namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
{
    public class ObjectToDeserializeTo
    {
        public int Id { get; }

        public string Name { get; }

        public ObjectToDeserializeTo(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}