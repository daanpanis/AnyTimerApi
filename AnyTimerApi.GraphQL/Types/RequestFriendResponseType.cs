using AnyTimerApi.GraphQL.Extensions;
using AnyTimerApi.GraphQL.Responses;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class RequestFriendResponseType : ObjectGraphType<RequestFriendResponse>
    {
        public RequestFriendResponseType()
        {
            Name = "RequestFriendResponse";
            Field(r => r.Time);
            FieldAsync<UserType>(SchemaConstants.Requester,
                resolve: async context => await context.UserRecord(context.Source.RequesterId));
            FieldAsync<UserType>(SchemaConstants.Requested,
                resolve: async context => await context.UserRecord(context.Source.RequestedId));
        }
    }
}