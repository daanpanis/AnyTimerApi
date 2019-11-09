using System.Security.Claims;
using System.Threading.Tasks;
using AnyTimerApi.GraphQL.Authentication;
using FirebaseAdmin.Auth;
using GraphQL.Types;
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

        public static async Task<UserRecord> UserRecord<T>(this ResolveFieldContext<T> context)
        {
            return await UserService.FromRequest(context.User());
        }

        public static async Task<UserRecord> UserRecord<T>(this ResolveFieldContext<T> context, string userId)
        {
            return await UserService.ById(userId);
        }

        public static void BindUser(this HttpContext context)
        {
            UserService.UnBindContextUser(context.User);
        }

        public static void UnBindUser(this HttpContext context)
        {
            UserService.BindContextUser(context.User);
        }
    }
}