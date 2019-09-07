using System;

namespace LogCorner.EduSync.Speech.Domain.Events
{
    public abstract class DomainEvent
    {
        public string Id { get; protected set; }
        public DateTime OcurrendOn { get; }
        public long Version { get; internal set; }

        public DomainEvent()
        {
            OcurrendOn = DateTime.UtcNow;
        }
    }
}