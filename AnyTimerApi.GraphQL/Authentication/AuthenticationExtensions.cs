using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Authentication
{
    public static class AuthenticationExtensions
    {
        private const string AuthenticatedKey = "Authenticated";

        public static void RequiresAuthentication(this IProvideMetadata metadata)
        {
            metadata.Metadata[AuthenticatedKey] = true;
        }

        public static bool DoesRequireAuthentication(this IProvideMetadata metadata)
        {
            var authenticatedValue = metadata.Metadata.ContainsKey(AuthenticatedKey)
                ? metadata.Metadata[AuthenticatedKey]
                : null;
            return authenticatedValue == null || (bool) authenticatedValue;
        }
    }
}