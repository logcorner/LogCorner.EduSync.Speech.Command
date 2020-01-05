using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure.Exceptions;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class SpeechRepository : ISpeechRepository
    {
        private readonly IRepository<Domain.SpeechAggregate.Speech, Guid> _repository;

        public SpeechRepository(IRepository<Domain.SpeechAggregate.Speech, Guid> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Domain.SpeechAggregate.Speech speech)
        {
            await _repository.CreateAsync(speech);
        }

        public async Task UpdateAsync(Domain.SpeechAggregate.Speech entity)
        {
            if (entity == null)
            {
                throw new RepositoryArgumentNullException(nameof(entity));
            }
        }
    }
}