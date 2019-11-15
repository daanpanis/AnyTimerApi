using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Authentication;
using AnyTimerApi.GraphQL.Extensions;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class FriendRequestType : ObjectGraphType<FriendRequest>
    {
        public FriendRequestType()
        {
            Name = "FriendRequest";
            Field(r => r.Id, type: typeof(IdGraphType));
            Field<FriendRequestStatusType>("Status");
            Field(r => r.CreatedTime);
            FieldAsync<UserType>(
                "requester",
                resolve: async context => await context.UserRecord(context.Source.RequesterId)
            );
            FieldAsync<UserType>(
                "requested",
                resolve: async context => await context.UserRecord(context.Source.RequestedId)
            );
        }
    }

    public class FriendRequestStatusType : EnumerationGraphType<FriendRequestStatus>
    {
        public FriendRequestStatusType()
        {
            Name = "FriendRequestStatus";
        }
    }
}