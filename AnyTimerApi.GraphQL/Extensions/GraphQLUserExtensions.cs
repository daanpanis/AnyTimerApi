using System.Security.Claims;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
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

        public static Task<UserRecord> UserRecord<T>(this ResolveFieldContext<T> context)
        {
            return context.UserRecord(context.User()?.GetUserId());
        }

        public static async Task<UserRecord> UserRecord<T>(this ResolveFieldContext<T> context, string userId)
        {
            try
            {
                return userId != null ? await FirebaseAuth.DefaultInstance.GetUserAsync(userId) : null;
            }
            catch (FirebaseAuthException ex)
            {
                return null;
            }
        }
    }
}