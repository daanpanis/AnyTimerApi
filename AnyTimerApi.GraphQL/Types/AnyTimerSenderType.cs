using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Extensions;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class AnyTimerSenderType : ObjectGraphType<AnyTimerSender>
    {
        public AnyTimerSenderType()
        {
            Name = "AnyTimerSender";
            FieldAsync<UserType>(
                "user",
                resolve: async context => await context.UserRecord(context.Source.SenderId)
            );
            Field(s => s.Amount);
        }
    }
}