using System;
using AnyTimerApi.GraphQL.Mutations;
using GraphQL.Types;
using GraphQL.Utilities;

namespace AnyTimerApi.GraphQL
{
    public class AppMutations : ObjectGraphType
    {
        private readonly IServiceProvider _serviceProvider;

        public AppMutations(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            SetupMutation<FriendRequestMutations>();
        }

        private void SetupMutation<T>() where T : IMutation
        {
            _serviceProvider.GetRequiredService<T>().SetupMutationDefinitions(this);
        }
    }
}