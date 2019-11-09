using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Authentication;
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
            Field<UserType>(
                "requester",
                resolve: context => UserService.ById(context.Source.RequesterId)
            );
            Field<UserType>(
                "requested",
                resolve: context => UserService.ById(context.Source.RequestedId)
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