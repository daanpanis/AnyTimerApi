using System;
using AnyTimerApi.GraphQL.Authentication;
using AnyTimerApi.GraphQL.InputTypes;
using AnyTimerApi.GraphQL.Mutations;
using AnyTimerApi.GraphQL.Queries;
using AnyTimerApi.GraphQL.Types;
using GraphQL;
using GraphQL.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnyTimerApi.GraphQL.Extensions
{
    public static class AppServiceCollectionExtensions
    {
        public static void AddAnyTimerApp(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddScoped<IServiceProvider>(provider => new FuncServiceProvider(provider.GetService));
            services.AddScoped<UserType>();
            services.AddScoped<AnyTimerSenderType>();
            services.AddScoped<AnyTimerStatusType>();
            services.AddScoped<AnyTimerType>();
            services.AddScoped<FriendRequestType>();
            services.AddScoped<FriendRequestStatusType>();
            services.AddScoped<RequestFriendResponseType>();
            services.AddScoped<CommentType>();
            services.AddScoped<StatusEventType>();

            services.AddScoped<AnyTimerInputType>();
            services.AddScoped<AnyTimerSenderInputType>();

            services.AddScoped<UserQueries>();
            services.AddScoped<AnyTimerQueries>();
            services.AddScoped<FriendRequestQueries>();

            services.AddScoped<FriendRequestMutations>();
            services.AddScoped<AnyTimerMutations>();
            
            services.AddTransient<IValidationRule>(s => new AuthenticationValidationRule());

            services.AddScoped<AppSchema>();

            
            
            if (configuration == null) return;
            try
            {
                UserService.CacheSize = Convert.ToInt32(configuration["AppSettings:UserCacheSize"]);
            }
            catch (FormatException)
            {
            }
        }
    }
}