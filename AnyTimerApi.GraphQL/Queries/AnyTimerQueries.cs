using System;
using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using GraphQL;
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
            type.Field<AnyTimerType>(
                "anytimer",
                arguments: new QueryArguments(
                    new QueryArgument(typeof(IdGraphType)) {Name = SchemaConstants.Id}
                ),
                resolve: context =>
                {
                    if (Guid.TryParse(context.GetArgument<string>("ownerId"), out var id)) return _repository.ById(id);
                    context.Errors.Add(new ExecutionError("Wrong value for guid"));
                    return null;
                }
            );

            type.Field<ListGraphType<AnyTimerType>>(
                "received",
                arguments: new QueryArguments(
                    new QueryArgument(typeof(IdGraphType)) {Name = SchemaConstants.UserId}
                ),
                resolve: context => _repository.ReceivedByUser(context.GetArgument<string>(SchemaConstants.UserId))
            );

            type.Field<ListGraphType<AnyTimerType>>(
                "sent",
                arguments: new QueryArguments(
                    new QueryArgument(typeof(IdGraphType)) {Name = SchemaConstants.UserId}
                ),
                resolve: context => _repository.SentByUser(context.GetArgument<string>(SchemaConstants.UserId))
            );
        }
    }
}