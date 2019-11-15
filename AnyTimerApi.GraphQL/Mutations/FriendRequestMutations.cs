using System;
using AnyTimerApi.GraphQL.Authentication;
using AnyTimerApi.GraphQL.Extensions;
using AnyTimerApi.GraphQL.Responses;
using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Mutations
{
    public class FriendRequestMutations : IMutation
    {
        private readonly IFriendRequestRepository _repository;

        public FriendRequestMutations(IFriendRequestRepository repository)
        {
            _repository = repository;
        }

        public void SetupMutationDefinitions(ObjectGraphType type)
        {
            type.FieldAsync<RequestFriendResponseType>(
                "requestFriend",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> {Name = SchemaConstants.UserId}
                ),
                resolve: async context =>
                {
                    var target = await context.UserRecord(context.GetArgument<string>(SchemaConstants.UserId));
                    if (target == null) return context.Error(GraphQLErrors.UnknownUser());

                    var response = new RequestFriendResponse
                    {
                        RequesterId = context.User().GetUserId(),
                        RequestedId = target.Uid,
                        Time = DateTime.Now
                    };

                    if (await _repository.ByUsers(response.RequesterId, response.RequestedId) != null)
                        return context.Error(GraphQLErrors.FriendRequestActive);

                    await _repository.AddFriendRequest(response.RequesterId, response.RequestedId, response.Time);
                    // TODO Send push notification
                    return response;
                }
            ).RequiresAuthentication();
        }
    }
}