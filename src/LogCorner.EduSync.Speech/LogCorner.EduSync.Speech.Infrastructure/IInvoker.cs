using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public interface IInvoker<out T> where T : AggregateRoot<Guid>
    {
        T CreateInstanceOfAggregateRoot();
    }
}