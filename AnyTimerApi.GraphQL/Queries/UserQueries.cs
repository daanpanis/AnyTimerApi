using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Queries
{
    public class UserQueries : ObjectGraphType
    {
        public UserQueries(IUserRepository repository)
        {
            Field<UserType>("user",
                arguments: new QueryArguments(
                    new QueryArgument(typeof(IdGraphType)) {Name = "id"}
                ),
                resolve: context => repository.GetById(context.GetArgument<string>("id")));
        }
    }
}