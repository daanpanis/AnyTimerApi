using AnyTimerApi.Database.Entities;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class AnyTimerType : ObjectGraphType<AnyTimer>
    {
        public AnyTimerType(IUserRepository repository)
        {
            Field(a => a.Id, type: typeof(IdGraphType));
            Field(a => a.Amount);
            Field<UserType>(
                "receiver",
                resolve: context => repository.GetById(context.Source.ReceiverId)
            );
            Field<UserType>(
                "requester",
                resolve: context => repository.GetById(context.Source.RequesterId)
            );
        }
    }
}