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
            type.Field<UserType>("user",
                arguments: new QueryArguments(
                    new QueryArgument(typeof(IdGraphType)) {Name = SchemaConstants.Id}
                ),
                resolve: context => _repository.GetById(context.GetArgument<string>(SchemaConstants.Id)));
        }
    }
}