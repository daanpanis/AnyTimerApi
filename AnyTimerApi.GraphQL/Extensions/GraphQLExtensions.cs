using GraphQL;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Extensions
{
    public static class GraphQLExtensions
    {
        public static object Error<T>(this ResolveFieldContext<T> context, GraphQLError error)
        {
            return Error(context, error.Build());
        }

        public static object Error<T>(this ResolveFieldContext<T> context, ExecutionError error)
        {
            context.Errors.Add(error);
            return null;
        }
    }
}