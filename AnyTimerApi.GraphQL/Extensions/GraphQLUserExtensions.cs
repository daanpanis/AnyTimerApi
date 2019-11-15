using System.Security.Claims;
using System.Threading.Tasks;
using AnyTimerApi.Database.Models;
using AnyTimerApi.GraphQL.Authentication;
using FirebaseAdmin.Auth;
using GraphQL.Types;
using GraphQL.Utilities;
using Microsoft.AspNetCore.Http;

namespace AnyTimerApi.GraphQL.Extensions
{
    public static class GraphQLUserExtensions
    {
        public static ClaimsPrincipal User<T>(this ResolveFieldContext<T> context)
        {
            return (context.UserContext as GraphQLUserContext)?.User;
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static async Task<User> UserRecord<T>(this ResolveFieldContext<T> context)
        {
            var userService = (context.UserContext as GraphQLUserContext)?.HttpContext?.RequestServices?
                .GetRequiredService<UserService>();
            if (userService == null) return null;
            return await userService.FromRequest(context.User());
        }

        public static async Task<User> UserRecord<T>(this ResolveFieldContext<T> context, string userId)
        {
            var userService = (context.UserContext as GraphQLUserContext)?.HttpContext?.RequestServices
                ?.GetRequiredService<UserService>();
            if (userService == null) return null;
            return await userService.ById(userId);
        }

        public static void BindUser(this HttpContext context)
        {
            context.RequestServices.GetRequiredService<UserService>().BindContextUser(context.User);
        }

        public static void UnBindUser(this HttpContext context)
        {
            context.RequestServices.GetRequiredService<UserService>().UnBindContextUser(context.User);
        }
    }
}