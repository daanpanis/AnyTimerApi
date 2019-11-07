using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Mutations
{
    public class AuthenticationMutations : IMutation
    {
        private readonly IFriendRequestRepository _repository;

        public void SetupMutationDefinitions(ObjectGraphType type)
        {
        }
    }
}