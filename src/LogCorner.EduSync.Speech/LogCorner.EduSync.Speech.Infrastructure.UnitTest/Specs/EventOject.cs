namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
{
    public class EventOject
    {
        public int Id { get; }

        public string Name { get; }

        public EventOject(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}