using System;
using System.Diagnostics;
using AnyTimerApi.Database.Entities;
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
                    var userRecord = await context.UserRecord();
                    Console.WriteLine(((GraphQLUserContext) context.UserContext).User);
                    Debug.WriteLine(((GraphQLUserContext) context.UserContext).User);
                    return new User
                    {
                        Id = context.GetArgument<string>(SchemaConstants.Id),
                        Name = ((GraphQLUserContext) context?.UserContext)?.User?.Identity?.IsAuthenticated + ""
                    };
                });
        }
    }
}