using LogCorner.EduSync.Speech.Domain.IRepository;
using LogCorner.EduSync.Speech.Domain.SpeechAggregate;
using LogCorner.EduSync.Speech.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public class SpeechRepository : ISpeechRepository
    {
        private readonly IRepository<Domain.SpeechAggregate.Speech, Guid> _repository;
        private readonly DataBaseContext _context;

        public SpeechRepository(IRepository<Domain.SpeechAggregate.Speech, Guid> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task CreateAsync(Domain.SpeechAggregate.Speech speech)
        {
            await _repository.CreateAsync(speech);
        }

        public async Task UpdateAsync(Domain.SpeechAggregate.Speech speech)
        {
            if (speech == null)
            {
                throw new ArgumentNullRepositoryException(nameof(speech));
            }

            var existingSpeech = await _context.Speech
                .Include(b => b.MediaFileItems)
                .FirstOrDefaultAsync(b => b.Id == speech.Id);

            _context.Entry(existingSpeech ?? throw new NotFoundRepositoryException(nameof(existingSpeech))).CurrentValues.SetValues(speech);
        }
    }
}