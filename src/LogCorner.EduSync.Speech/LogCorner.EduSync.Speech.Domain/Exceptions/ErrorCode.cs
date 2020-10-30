namespace LogCorner.EduSync.Speech.Domain.Exceptions
{
    public static class ErrorCode
    {
        public const int InvalidVersionSpecified = 5000;
        public const int InvalidDomainEvent = 5001;
        public const int InvalidLenght = 5002;
        public const int BadAggregateId = 5003;
        public const int NullInstanceOfAggregate = 5004;
        public const int AlreadyExist = 5005;
    }
}