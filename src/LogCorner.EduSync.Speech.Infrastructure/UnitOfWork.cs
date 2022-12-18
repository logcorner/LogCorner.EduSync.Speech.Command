using LogCorner.EduSync.Speech.Domain.IRepository;
using System;

namespace LogCorner.EduSync.Speech.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _databaseContext;

        public UnitOfWork(DataBaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Commit()
        {
            _databaseContext.SaveChanges();
        }

        public void Dispose()
        {
            _databaseContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}