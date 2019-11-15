using System.Threading.Tasks;
using AnyTimerApi.Database.Models;
using AnyTimerApi.Repository;

namespace AnyTimerApi.Redis
{
    public class RedisUserRepository : IUserRepository
    {
        public async Task<User> ById(string userId)
        {
            return await BeetleX.Redis.Redis.Default.Get<User>(userId);
        }

        public async Task SaveUser(User user)
        {
            await BeetleX.Redis.Redis.Default.Set(user.Uid, user);
        }

        public async Task DeleteUser(string userId)
        {
            await BeetleX.Redis.Redis.Default.Del(userId);
        }
    }
}