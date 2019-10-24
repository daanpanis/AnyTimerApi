using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Queries
{
    public class FriendRequestQueries : IQuery
    {
        private readonly IFriendRequestRepository _repository;

        public FriendRequestQueries(IFriendRequestRepository repository)
        {
            _repository = repository;
        }

        public void SetupQueryDefinitions(ObjectGraphType type)
        {
            type.FieldAsync<FriendRequestType>(
                "request",
                arguments: new QueryArguments(
                    new QueryArgument(typeof(IdGraphType)) {Name = SchemaConstants.Id}
                ),
                resolve: async context => await _repository.ById(context.GetArgument<string>(SchemaConstants.Id)));
            type.FieldAsync<FriendRequestType>(
                "requests",
                arguments: new QueryArguments(
                    new QueryArgument(typeof(IdGraphType)) {Name = SchemaConstants.UserId}
                ),
                resolve: async context => await _repository.ByUser(context.GetArgument<string>(SchemaConstants.UserId))
            );
            type.FieldAsync<ListGraphType<FriendRequestType>>(
                "sentRequests",
                arguments: new QueryArguments(
                    new QueryArgument(typeof(IdGraphType)) {Name = SchemaConstants.UserId}
                ),
                resolve: async context =>
                    await _repository.SentRequests(context.GetArgument<string>(SchemaConstants.UserId))
            );
            type.FieldAsync<ListGraphType<FriendRequestType>>(
                "receivedRequests",
                arguments: new QueryArguments(
                    new QueryArgument(typeof(IdGraphType)) {Name = SchemaConstants.UserId}
                ),
                resolve: async context =>
                    await _repository.ReceivedRequests(context.GetArgument<string>(SchemaConstants.UserId))
            );
        }
    }
}