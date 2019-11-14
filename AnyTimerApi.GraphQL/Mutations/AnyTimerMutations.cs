using AnyTimerApi.GraphQL.InputTypes;
using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Mutations
{
    public class AnyTimerMutations : IMutation
    {
        private readonly IAnyTimerRepository _repository;

        public AnyTimerMutations(IAnyTimerRepository repository)
        {
            _repository = repository;
        }

        public void SetupMutationDefinitions(ObjectGraphType type)
        {
            type.FieldAsync<AnyTimerType>(
                "newAnyTimer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AnyTimerInputType>> {Name = SchemaConstants.Args}
                ),
                resolve: async context =>
                {
                    var args = context.GetArgument<AnyTimerInputType>(SchemaConstants.Args);
                    return null;
                }
            );
        }
    }
}