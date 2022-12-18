using LogCorner.EduSync.Speech.Command.SharedKernel.Events;

namespace LogCorner.EduSync.Speech.Infrastructure.UnitTests.Specs
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