using System.Security.Claims;
using GraphQL.Types;

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
    }
}