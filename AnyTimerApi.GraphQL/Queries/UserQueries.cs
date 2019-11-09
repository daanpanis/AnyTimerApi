using AnyTimerApi.GraphQL.Extensions;
using AnyTimerApi.GraphQL.Types;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Queries
{
    public class UserQueries : IQuery
    {
        public void SetupQueryDefinitions(ObjectGraphType type)
        {
            type.FieldAsync<UserType>("user",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> {Name = SchemaConstants.Id}
                ),
                resolve: async context =>
                {
                    var user = await context.UserRecord();
                    if (user == null) throw GraphQLErrors.AuthenticationRequired.Build();
                    return user;
                });
        }
    }
}