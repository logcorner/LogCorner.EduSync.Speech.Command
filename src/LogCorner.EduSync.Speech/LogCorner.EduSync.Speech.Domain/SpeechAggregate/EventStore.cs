using System;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public class EventStore
    {
        public int Id { get; private set; }
        public long Version { get; private set; }
        public Guid AggregateId { get; private set; }
        public string Name { get; private set; }
        public string TypeName { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public string PayLoad { get; private set; }
        public bool IsSync { get; private set; }

        private EventStore()
        {
        }

        public EventStore(Guid aggregateId, long aggregateVersion, string name,
            string typeName, DateTime occurredOn, string serializedBody)
        {
            Version = aggregateVersion;
            AggregateId = aggregateId;
            Name = name;
            TypeName = typeName;
            OccurredOn = occurredOn;
            PayLoad = serializedBody;
        }
    }
}