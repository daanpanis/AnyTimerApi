using AnyTimerApi.Repository;
using BeetleX.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnyTimerApi.Redis
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration = null)
        {
            BeetleX.Redis.Redis.Default.DataFormater = new JsonFormater();
            BeetleX.Redis.Redis.Default.Host.AddWriteHost("localhost");

            services.AddScoped<IUserRepository, RedisUserRepository>();

            return services;
        }
    }
}