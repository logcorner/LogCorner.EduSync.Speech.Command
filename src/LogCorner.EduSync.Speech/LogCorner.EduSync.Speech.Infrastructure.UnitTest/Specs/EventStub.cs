using LogCorner.EduSync.Speech.Domain.Events;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTest.Specs
{
    public class EventStub : Event
    {
        private int Id { get; }

        public EventStub(int id)
        {
            Id = id;
        }
    }
}