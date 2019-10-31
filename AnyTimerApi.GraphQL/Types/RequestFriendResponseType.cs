using AnyTimerApi.GraphQL.Responses;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class RequestFriendResponseType : ObjectGraphType<RequestFriendResponse>
    {
        public RequestFriendResponseType(IUserRepository userRepository)
        {
            FieldAsync<UserType>(SchemaConstants.Requester,
                resolve: async context => await userRepository.GetById(context.Source.RequesterId));
            FieldAsync<UserType>(SchemaConstants.Requested,
                resolve: async context => await userRepository.GetById(context.Source.RequestedId));
        }
    }
}