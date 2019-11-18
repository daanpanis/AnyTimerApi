using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnyTimerApi.Database.Entities;

namespace AnyTimerApi.Repository
{
    public interface ICommentRepository
    {
        Task<ICollection<Comment>> ForAnyTimer(string anyTimerId);
        Task Add(Comment comment);
        Task Update(Comment comment);
        Task<Comment> Get(string anyTimerId, string userId, DateTime time);
        Task Delete(Comment comment);
    }
}