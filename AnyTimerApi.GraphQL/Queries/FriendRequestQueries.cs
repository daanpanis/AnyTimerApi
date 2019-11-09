using AnyTimerApi.GraphQL.Authentication;
using AnyTimerApi.GraphQL.Extensions;
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
                "friendRequest",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> {Name = SchemaConstants.Id}
                ),
                resolve: async context =>
                {
                    var userId = context.User().GetUserId();
                    var friendRequest = await _repository.ById(context.GetArgument<string>(SchemaConstants.Id));
                    if (friendRequest == null) return context.Error(GraphQLErrors.UnknownFriendRequest);
                    if (!friendRequest.RequesterId.Equals(userId) && !friendRequest.RequestedId.Equals(userId))
                        return context.Error(GraphQLErrors.Unauthorized);
                    return friendRequest;
                }
            ).RequiresAuthentication();

            type.FieldAsync<ListGraphType<FriendRequestType>>(
                "friendRequests",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BooleanGraphType>>
                        {Name = SchemaConstants.OnlyRequested, DefaultValue = false}
                ),
                resolve: async context =>
                {
                    var userId = context.User().GetUserId();
                    var onlyRequested = context.GetArgument<bool>(SchemaConstants.OnlyRequested);
                    return onlyRequested ? await _repository.Requests(userId) : await _repository.All(userId);
                }
            ).RequiresAuthentication();
        }
    }
}