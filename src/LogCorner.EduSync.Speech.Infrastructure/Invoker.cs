using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using System;
using System.Reflection;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class Invoker<T> : IInvoker<T> where T : AggregateRoot<Guid>
    {
        public TU CreateInstanceOfAggregateRoot<TU>()
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            return (TU)typeof(TU)
                .GetConstructor(bindingFlags,
                    null,
                    Type.EmptyTypes,
                    Array.Empty<ParameterModifier>())
                ?.Invoke(Array.Empty<object>());
        }
    }
}