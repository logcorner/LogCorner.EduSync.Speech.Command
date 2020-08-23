using LogCorner.EduSync.Speech.SharedKernel.Events;

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