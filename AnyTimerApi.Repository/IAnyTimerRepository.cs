using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnyTimerApi.Database.Entities;

namespace AnyTimerApi.Repository
{
    public interface IAnyTimerRepository
    {
        Task<AnyTimer> ById(Guid id);

        Task<IEnumerable<AnyTimer>> ReceivedByUser(string userId);

        Task<IEnumerable<AnyTimer>> SentByUser(string userId);
    }
}