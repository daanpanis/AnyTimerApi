using System;
using AnyTimerApi.GraphQL.Queries;
using GraphQL.Types;
using GraphQL.Utilities;

namespace AnyTimerApi.GraphQL
{
    public class AppQueries : ObjectGraphType
    {
        private readonly IServiceProvider _provider;

        public AppQueries(IServiceProvider provider)
        {
            _provider = provider;
            SetupQuery<AnyTimerQueries>();
            SetupQuery<UserQueries>();
            SetupQuery<FriendRequestQueries>();
        }

        private void SetupQuery<T>() where T : IQuery
        {
            _provider.GetRequiredService<T>().SetupQueryDefinitions(this);
        }
    }
}