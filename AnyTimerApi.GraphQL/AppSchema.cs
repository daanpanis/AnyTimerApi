using System;
using AnyTimerApi.GraphQL.Queries;
using GraphQL.Types;
using GraphQL.Utilities;

namespace AnyTimerApi.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = new AppQueries(serviceProvider);
        }
    }
}