using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;
using System.Reflection;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class Invoker<T> : IInvoker<T> where T : AggregateRoot<Guid>
    {
        public T CreateInstanceOfAggregateRoot<T>()
        {
            return (T)typeof(T)
                .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    new Type[0],
                    new ParameterModifier[0])
                ?.Invoke(new object[0]);
        }
    }
}