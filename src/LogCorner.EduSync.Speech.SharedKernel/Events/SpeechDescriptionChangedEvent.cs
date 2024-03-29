﻿using System;

namespace LogCorner.EduSync.Speech.Command.SharedKernel.Events
{
    public class SpeechDescriptionChangedEvent : Event
    {
        public string Description { get; }

        public SpeechDescriptionChangedEvent(Guid aggregateId, string description, Guid eventId, DateTime ocurrendOn, long aggregateVersion = default)
        {
            AggregateId = aggregateId;
            Description = description;
            EventId = eventId;
            OcurrendOn = ocurrendOn;
            AggregateVersion = aggregateVersion;
        }
    }
}