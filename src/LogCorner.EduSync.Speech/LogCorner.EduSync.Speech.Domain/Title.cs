using LogCorner.EduSync.Speech.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Domain
{
    public class Title : IEquatable<Title>
    {
        private const int MinLenght = 10;
        private const int MaxLenght = 60;
        public string Value { get; }

        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidLenghtAggregateException("Value is null or whitespace");
            }
            if (value?.Length < MinLenght)
                throw new InvalidLenghtAggregateException("Value is too short");

            if (value?.Length > MaxLenght)
                throw new InvalidLenghtAggregateException("Value is too long");

            Value = value;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Title);
        }

        public bool Equals(Title other)
        {
            return other != null &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static bool operator ==(Title title1, Title title2)
        {
            return EqualityComparer<Title>.Default.Equals(title1, title2);
        }

        public static bool operator !=(Title title1, Title title2)
        {
            return !(title1 == title2);
        }
    }
}