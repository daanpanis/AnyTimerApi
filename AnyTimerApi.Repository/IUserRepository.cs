using System.Threading.Tasks;
using AnyTimerApi.Database.Entities;

namespace AnyTimerApi.Repository
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
    }
}