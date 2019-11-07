using AnyTimerApi.GraphQL.Extensions;
using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Queries
{
    public class UserQueries : IQuery
    {
        private readonly IUserRepository _repository;

        public UserQueries(IUserRepository repository)
        {
            _repository = repository;
        }

        public void SetupQueryDefinitions(ObjectGraphType type)
        {
            type.FieldAsync<UserType>("user",
                arguments: new QueryArguments(
                    new QueryArgument(typeof(IdGraphType)) {Name = SchemaConstants.Id}
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