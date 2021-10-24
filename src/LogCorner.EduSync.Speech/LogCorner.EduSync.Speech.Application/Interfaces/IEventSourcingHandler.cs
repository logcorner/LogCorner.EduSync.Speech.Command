﻿using LogCorner.EduSync.Speech.SharedKernel.Events;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Application.Interfaces
{
    public interface IEventSourcingHandler<T> where T : IDomainEvent
    {
        Task Handle(T @event, long aggregateVersion);
    }
}