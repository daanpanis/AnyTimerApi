using System;
using AnyTimerApi.GraphQL.Queries;
using GraphQL.Types;
using GraphQL.Utilities;

namespace AnyTimerApi.GraphQL
{
    public class AnyTimerSchema : Schema
    {
        public AnyTimerSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<UserQueries>();
        }
    }
}