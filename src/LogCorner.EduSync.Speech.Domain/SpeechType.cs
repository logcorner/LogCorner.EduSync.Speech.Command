using LogCorner.EduSync.Speech.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Domain
{
    public sealed class SpeechType : IEquatable<SpeechType>
    {
        public static readonly SpeechType TraingVideo = new SpeechType(SpeechTypes.TraingVideo);
        public static readonly SpeechType Conferences = new SpeechType(SpeechTypes.Conferences);
        public static readonly SpeechType SelfPacedLabs = new SpeechType(SpeechTypes.SelfPacedLabs);

        public SpeechTypes Value { get; }

        public string StringValue => Value.ToString();

        public int IntValue => (int)Value;

        private SpeechType(SpeechTypes value)
        {
            Value = value;
        }

        public SpeechType(string value)
        {
            Enum.TryParse(typeof(SpeechTypes), value, out var result);

            Value = (SpeechTypes?)result ??
                    throw new InvalidEnumAggregateException(ErrorCode.InvalidLenght, "SpeechType should be valid and not empty");
        }

        public SpeechType(int value)
        {
            if (!Enum.IsDefined(typeof(SpeechTypes), value))
            {
                throw new InvalidEnumAggregateException(ErrorCode.InvalidEnum, $"{value} is not valid for SpeechType");
            }
            Value = (SpeechTypes)Enum.ToObject(typeof(SpeechTypes), value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SpeechType);
        }

        public bool Equals(SpeechType other)
        {
            return other != null &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static bool operator ==(SpeechType type1, SpeechType type2)
        {
            return EqualityComparer<SpeechType>.Default.Equals(type1, type2);
        }

        public static bool operator !=(SpeechType type1, SpeechType type2)
        {
            return !(type1 == type2);
        }
    }
}