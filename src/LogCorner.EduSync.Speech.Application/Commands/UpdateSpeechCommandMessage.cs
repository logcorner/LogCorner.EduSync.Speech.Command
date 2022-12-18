﻿using LogCorner.EduSync.Speech.Application.Interfaces;
using System;

namespace LogCorner.EduSync.Speech.Application.Commands
{
    public class UpdateSpeechCommandMessage : ICommand
    {
        public Guid SpeechId { get; }
        public string Title { get; }
        public string Description { get; }
        public string Url { get; }
        public int? Type { get; }

        public long OriginalVersion { get; }

        public UpdateSpeechCommandMessage(Guid id, string title, string description, string url, int? type, long originalVersion)
        {
            SpeechId = id;
            Title = title;
            Description = description;
            Url = url;
            Type = type;
            OriginalVersion = originalVersion;
        }
    }
}