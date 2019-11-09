using System.Collections.Generic;
using System.Threading.Tasks;
using AnyTimerApi.Database.Entities;

namespace AnyTimerApi.Repository
{
    public interface ICommentRepository
    {
        Task<ICollection<Comment>> ForAnyTimer(string anyTimerId);
    }
}