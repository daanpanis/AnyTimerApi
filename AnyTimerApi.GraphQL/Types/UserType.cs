using AnyTimerApi.Database.Entities;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(u => u.Id, type: typeof(IdGraphType));
            Field(u => u.Name);
            Field(u => u.Age);
        }
    }
}