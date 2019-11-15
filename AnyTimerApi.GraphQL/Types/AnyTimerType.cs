using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Extensions;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class AnyTimerType : ObjectGraphType<AnyTimer>
    {
        public AnyTimerType(IAnyTimerRepository repository, ICommentRepository commentRepository)
        {
            Name = "AnyTimer";
            Field(a => a.Id, type: typeof(IdGraphType));
            Field(a => a.CreatedTime);
            Field(a => a.LastUpdated);
            Field<AnyTimerStatusType>("Status");
            Field(a => a.Reason);
            FieldAsync<UserType>(
                "receiver",
                resolve: async context => await context.UserRecord(context.Source.ReceiverId)
            );
            FieldAsync<UserType>(
                "creator",
                resolve: async context => await context.UserRecord(context.Source.CreatorId)
            );
            FieldAsync<ListGraphType<AnyTimerSenderType>>(
                "senders",
                resolve: async context => await repository.Senders(context.Source.Id));
            FieldAsync<ListGraphType<StatusEventType>>(
                "statusEvents",
                resolve: async context => await repository.StatusEvents(context.Source.Id)
            );
            FieldAsync<ListGraphType<CommentType>>(
                "comments",
                resolve: async context => await commentRepository.ForAnyTimer(context.Source.Id)
            );
        }
    }

    public class AnyTimerStatusType : EnumerationGraphType<AnyTimerStatus>
    {
        public AnyTimerStatusType()
        {
            Name = "AnyTimerStatus";
        }
    }
}