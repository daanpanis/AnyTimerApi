using System.Collections.Generic;
using System.Threading.Tasks;
using AnyTimerApi.Database.Entities;

namespace AnyTimerApi.Repository
{
    public interface IAnyTimerRepository
    {
        Task<AnyTimer> ById(string anyTimerId);

        Task<IEnumerable<AnyTimer>> AllForUser(string userId);

        Task<IEnumerable<AnyTimer>> Received(string userId);

        Task<IEnumerable<AnyTimer>> Sent(string userId);

        Task<ICollection<AnyTimerSender>> Senders(string anyTimerId);

        Task<ICollection<StatusEvent>> StatusEvents(string anyTimerId);

        Task<bool> IsSender(string userId, string anyTimerId);
    }
}