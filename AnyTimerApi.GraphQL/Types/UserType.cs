using AnyTimerApi.Repository;
using FirebaseAdmin.Auth;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class UserType : ObjectGraphType<UserRecord>
    {
        public UserType(IFriendRequestRepository friendRequestRepository)
        {
            Name = "User";
            Field(u => u.Uid, type: typeof(IdGraphType));
            Field(SchemaConstants.Name, u => u.DisplayName, nullable: true);
            Field(u => u.Email);
            Field(u => u.PhotoUrl, nullable: true);
            Field<ListGraphType<UserType>>(SchemaConstants.Friends,
                resolve: context => friendRequestRepository.GetFriends(context.Source.Uid));
        }
    }
}