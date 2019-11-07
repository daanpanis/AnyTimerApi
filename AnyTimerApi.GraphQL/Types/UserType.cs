using AnyTimerApi.Repository;
using FirebaseAdmin.Auth;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class UserType : ObjectGraphType<UserRecord>
    {
        public UserType(IFriendRequestRepository friendRequestRepository)
        {
            Field(u => u.Uid, type: typeof(IdGraphType));
            Field(SchemaConstants.Name, u => u.DisplayName);
            Field(u => u.Email);
            Field(u => u.PhotoUrl);
            Field<ListGraphType<UserType>>(SchemaConstants.Friends,
                resolve: context => friendRequestRepository.GetFriends(context.Source.Uid));
        }
    }
}