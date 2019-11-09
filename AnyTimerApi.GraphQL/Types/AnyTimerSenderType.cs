using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Authentication;
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
                resolve: async context => await UserService.ById(context.Source.SenderId)
            );
            Field(s => s.Amount);
        }
    }
}