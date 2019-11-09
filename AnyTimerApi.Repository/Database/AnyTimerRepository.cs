using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyTimerApi.Database;
using AnyTimerApi.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnyTimerApi.Repository.Database
{
    public class AnyTimerRepository : IAnyTimerRepository
    {
        private readonly DatabaseContext _context;

        public AnyTimerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<AnyTimer> ById(string id)
        {
            return await _context.AnyTimers.FirstOrDefaultAsync(anyTimer => anyTimer.Id.Equals(id));
        }

        public async Task<IEnumerable<AnyTimer>> AllForUser(string userId)
        {
            return await _context.AnyTimers.Include(a => a.Senders).Where(a =>
                a.ReceiverId.Equals(userId) || a.Senders.Any(sender => sender.SenderId.Equals(userId))).ToListAsync();
        }

        public async Task<IEnumerable<AnyTimer>> Received(string userId)
        {
            return await _context.AnyTimers.Where(anyTimer => anyTimer.ReceiverId.Equals(userId)).ToListAsync();
        }

        public async Task<IEnumerable<AnyTimer>> Sent(string userId)
        {
            return await _context.AnyTimerSenders.Include(sender => sender.AnyTimer)
                .Where(sender => sender.SenderId.Equals(userId)).Select(sender => sender.AnyTimer).ToListAsync();
        }

        public async Task<ICollection<AnyTimerSender>> Senders(string anyTimerId)
        {
            return await _context.AnyTimerSenders.Where(sender => sender.AnyTimerId.Equals(anyTimerId)).ToListAsync();
        }

        public async Task<ICollection<StatusEvent>> StatusEvents(string anyTimerId)
        {
            return await _context.StatusEvents.Where(e => e.AnyTimerId.SequenceEqual(anyTimerId)).ToListAsync();
        }

        public async Task<bool> IsSender(string userId, string anyTimerId)
        {
            return await _context.AnyTimerSenders.AnyAsync(sender =>
                sender.SenderId.Equals(userId) && sender.AnyTimerId.Equals(anyTimerId));
        }
    }
}