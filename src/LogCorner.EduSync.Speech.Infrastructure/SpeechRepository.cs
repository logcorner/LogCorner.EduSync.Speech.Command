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
            _context.ChangeTracker.TrackGraph(speech, a =>
           {
               a.Entry.State = a.Entry.IsKeySet ? EntityState.Modified : EntityState.Added;
           });

            foreach (var item in _context.ChangeTracker.Entries())
            {
                Console.WriteLine(item.Entity.GetType().Name, item.State.ToString());
            }

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Domain.SpeechAggregate.Speech speech)
        {
            if (speech == null)
            {
                throw new ArgumentNullRepositoryException(nameof(speech));
            }
            _context.ChangeTracker.TrackGraph(speech, a =>
              {
                  a.Entry.State = EntityState.Modified;
              });

            await _repository.UpdateAsync(speech);
        }
    }
}