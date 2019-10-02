using GraphQL.Types;

namespace AnyTimerApi.GraphQL
{
    public interface IQuery
    {
        void SetupQueryDefinitions(ObjectGraphType type);
    }
}