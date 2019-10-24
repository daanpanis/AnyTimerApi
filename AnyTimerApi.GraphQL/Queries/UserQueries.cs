using System;
using System.Diagnostics;
using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Authentication;
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
                resolve: context =>
                {
                    Console.WriteLine(((GraphQLUserContext) context.UserContext).User);
                    Debug.WriteLine(((GraphQLUserContext) context.UserContext).User);
//                    return _repository.GetById(context.GetArgument<string>(SchemaConstants.Id));
                    return new User
                    {
                        Name = ((GraphQLUserContext) context.UserContext).User.Identity.IsAuthenticated + ""
                    };
                }).RequiresAuthentication();
        }
    }
}