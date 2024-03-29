﻿using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class EventStore
    {
        public int Id { get; set; }
        public long Version { get; set; }
        public Guid AggregateId { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public DateTime OccurredOn { get; set; }
        public string PayLoad { get; set; }

        private EventStore()
        {
        }

        public EventStore(Guid aggregateId, long version, string name, string typeName, DateTime occurredOn, string payLoad)
        {
            Version = version;
            AggregateId = aggregateId;
            Name = name;
            TypeName = typeName;
            OccurredOn = occurredOn;
            PayLoad = payLoad;
        }
    }
}