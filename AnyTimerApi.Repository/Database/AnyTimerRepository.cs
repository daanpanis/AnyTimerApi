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

        public async Task<AnyTimer> ById(Guid id)
        {
            return await _context.AnyTimers.FirstOrDefaultAsync(anyTimer => anyTimer.Id.Equals(id));
        }

        public async Task<IEnumerable<AnyTimer>> ReceivedByUser(string userId)
        {
            return await _context.AnyTimers.Where(anyTimer => anyTimer.ReceiverId.Equals(userId)).ToListAsync();
        }

        public async Task<IEnumerable<AnyTimer>> SentByUser(string userId)
        {
            return await _context.AnyTimers.Where(anyTimer => anyTimer.RequesterId.Equals(userId)).ToListAsync();
        }
    }
}