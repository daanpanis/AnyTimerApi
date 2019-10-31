using System;
using AnyTimerApi.GraphQL.Authentication;
using AnyTimerApi.GraphQL.Mutations;
using AnyTimerApi.GraphQL.Queries;
using AnyTimerApi.GraphQL.Types;
using GraphQL;
using GraphQL.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace AnyTimerApi.GraphQL
{
    public static class AppServiceCollectionExtensions
    {
        public static void AddAnyTimerApp(this IServiceCollection services)
        {
            services.AddScoped<IServiceProvider>(provider => new FuncServiceProvider(provider.GetService));
            services.AddScoped<UserType>();
            services.AddScoped<AnyTimerType>();
            services.AddScoped<FriendRequestType>();
            services.AddScoped<FriendRequestStatusType>();
            services.AddScoped<RequestFriendResponseType>();

            services.AddScoped<UserQueries>();
            services.AddScoped<AnyTimerQueries>();
            services.AddScoped<FriendRequestQueries>();

            services.AddScoped<FriendRequestMutations>();

            services.AddTransient<IValidationRule>(s => new AuthenticationValidationRule());

            services.AddScoped<AppSchema>();
        }
    }
}