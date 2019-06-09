using LogCorner.EduSync.Speech.Domain.IRepository;
using System;

namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public interface ISpeechRepository : IRepository<Speech, Guid>
    {
    }
}