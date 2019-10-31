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
                    new QueryArgument(typeof(IdGraphType)) {Name = SchemaConstants.UserId}
                ),
                resolve: async (context) =>
                {
                    var response = new RequestFriendResponse
                    {
                        RequesterId = context.User().GetUserId(),
                        RequestedId = context.GetArgument<string>(SchemaConstants.UserId),
                        Time = DateTime.Now
                    };

                    var currentRequest = await _repository.ByUsers(response.RequesterId, response.RequestedId);
                    if (currentRequest != null)
                    {
                        context.Errors.Add(GraphQLErrors.FriendRequestActive.Build());
                        return null;
                    }

                    await _repository.AddFriendRequest(response.RequesterId, response.RequestedId, response.Time);
                    return response;
                }
            ).RequiresAuthentication();
        }
    }
}