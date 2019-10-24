using AnyTimerApi.Database.Entities;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(IFriendRequestRepository friendRequestRepository)
        {
            Field(u => u.Id, type: typeof(IdGraphType));
            Field(u => u.Name);
            Field(u => u.Age);
            Field<ListGraphType<UserType>>(SchemaConstants.Friends,
                resolve: context => friendRequestRepository.GetFriends(context.Source.Id));
        }
    }
}