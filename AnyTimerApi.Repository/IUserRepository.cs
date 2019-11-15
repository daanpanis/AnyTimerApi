using System.Threading.Tasks;
using AnyTimerApi.Database.Models;

namespace AnyTimerApi.Repository
{
    public interface IUserRepository
    {
        Task<User> ById(string userId);

        Task SaveUser(User user);

        Task DeleteUser(string userId);
    }
}