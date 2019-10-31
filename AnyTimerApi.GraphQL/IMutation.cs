using GraphQL.Types;

namespace AnyTimerApi.GraphQL
{
    public interface IMutation
    {
        void SetupMutationDefinitions(ObjectGraphType type);
    }
}