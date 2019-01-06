using System;

namespace LogCorner.EduSync.Speech.Domain.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}