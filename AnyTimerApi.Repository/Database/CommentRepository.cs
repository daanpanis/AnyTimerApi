using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyTimerApi.Database;
using AnyTimerApi.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnyTimerApi.Repository.Database
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContext _context;

        public CommentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Comment>> ForAnyTimer(string anyTimerId)
        {
            return await _context.Comments.Where(comment => comment.AnyTimerId.Equals(anyTimerId)).ToListAsync();
        }
    }
}