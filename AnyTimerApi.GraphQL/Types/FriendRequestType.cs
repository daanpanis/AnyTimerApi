using AnyTimerApi.Database.Entities;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class FriendRequestType : ObjectGraphType<FriendRequest>
    {
        public FriendRequestType(IUserRepository userRepository)
        {
            Field(r => r.Id, type: typeof(IdGraphType));
            Field<FriendRequestStatusType>("Status");
//            Field(r => r.LastUpdated);
            Field<UserType>(
                "requester",
                resolve: context => userRepository.GetById(context.Source.RequesterId)
            );
            Field<UserType>(
                "requested",
                resolve: context => userRepository.GetById(context.Source.RequestedId)
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