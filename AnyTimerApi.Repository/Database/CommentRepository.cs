using System;
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

        public async Task Add(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> Get(string anyTimerId, string userId, DateTime time)
        {
            return await _context.Comments.Where(comment =>
                    comment.AnyTimerId.Equals(anyTimerId) && comment.UserId.Equals(userId) && comment.Time.Equals(time))
                .FirstOrDefaultAsync();
        }

        public async Task Delete(Comment comment)
        {
            _context.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}