using LogCorner.EduSync.Speech.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace LogCorner.EduSync.Speech.Domain
{
    public class UrlValue : IEquatable<UrlValue>
    {
        public string Value { get; }

        public UrlValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidLenghtAggregateException(ErrorCode.InvalidLenght, "url should not be empty");

            if (!CheckUrlValid(value))
                throw new InvalidUrlAggregateException(ErrorCode.InvalidLenght, "url is invalid");
            Value = value;
        }

        private static bool CheckUrlValid(string source) =>
            Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) &&
            (uriResult.Scheme == Uri.UriSchemeHttp
             || uriResult.Scheme == Uri.UriSchemeHttps
             || uriResult.Scheme == Uri.UriSchemeFile);

        public override bool Equals(object obj)
        {
            return Equals(obj as UrlValue);
        }

        public bool Equals(UrlValue other)
        {
            return other != null &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static bool operator ==(UrlValue value1, UrlValue value2)
        {
            return EqualityComparer<UrlValue>.Default.Equals(value1, value2);
        }

        public static bool operator !=(UrlValue value1, UrlValue value2)
        {
            return !(value1 == value2);
        }
    }
}