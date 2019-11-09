using AnyTimerApi.GraphQL.Extensions;
using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Queries
{
    public class AnyTimerQueries : IQuery
    {
        private readonly IAnyTimerRepository _repository;

        public AnyTimerQueries(IAnyTimerRepository repository)
        {
            _repository = repository;
        }

        public void SetupQueryDefinitions(ObjectGraphType type)
        {
            type.FieldAsync<AnyTimerType>(
                "anytimer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> {Name = SchemaConstants.Id}
                ),
                resolve: async context =>
                {
                    var anyTimer = await _repository.ById(context.GetArgument<string>(SchemaConstants.Id));
                    if (anyTimer == null) return context.Error(GraphQLErrors.UnknownAnyTimer);
                    if (!await _repository.IsSender(context.User().GetUserId(), anyTimer.Id))
                        return context.Error(GraphQLErrors.Unauthorized);
                    return anyTimer;
                }
            );

            type.FieldAsync<ListGraphType<AnyTimerType>>(
                "anytimers",
                resolve: async context => await _repository.AllForUser(context.User().GetUserId())
            );

            type.FieldAsync<ListGraphType<AnyTimerType>>(
                "anytimersReceived",
                resolve: async context => await _repository.Received(context.User().GetUserId()));

            type.FieldAsync<ListGraphType<AnyTimerType>>(
                "anytimersSent",
                resolve: async context => await _repository.Sent(context.User().GetUserId())
            );
        }
    }
}