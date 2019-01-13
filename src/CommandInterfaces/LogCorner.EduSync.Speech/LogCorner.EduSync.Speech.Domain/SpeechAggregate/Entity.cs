namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }

        protected Entity()
        {
        }
    }
}