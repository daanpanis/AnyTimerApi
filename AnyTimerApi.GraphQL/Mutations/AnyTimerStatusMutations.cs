using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Authentication;
using AnyTimerApi.GraphQL.Extensions;
using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Mutations
{
    public class AnyTimerStatusMutations : IMutation
    {
        private readonly IAnyTimerRepository _repository;

        public AnyTimerStatusMutations(IAnyTimerRepository repository)
        {
            _repository = repository;
        }

        public void SetupMutationDefinitions(ObjectGraphType type)
        {
            type.FieldAsync<StatusEventType>(
                "cancelAnyTimer",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = SchemaConstants.Id}
                ),
                resolve: async context =>
                {
                    var user = await context.UserRecord();
                    var anytimer = await _repository.ById(context.GetArgument<string>(SchemaConstants.Id));
                    if (anytimer == null) return context.Error(GraphQLErrors.UnknownAnyTimer);
                    if (!anytimer.CreatorId.Equals(user.Uid)) return context.Error(GraphQLErrors.NotCreator);
                    if (anytimer.Status == AnyTimerStatus.Cancelled) return context.Error(GraphQLErrors.NotEditable);

                    // TODO Update total own for senders

                    var evt = AnyTimerMutations.UpdateStatus(anytimer, AnyTimerStatus.Cancelled);
                    await _repository.Update(anytimer);

                    return evt;
                }
            ).RequiresAuthentication();

            type.FieldAsync<StatusEventType>(
                "acceptAnyTimer",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = SchemaConstants.Id}
                ),
                resolve: async context =>
                {
                    var user = await context.UserRecord();
                    var anytimer = await _repository.ById(context.GetArgument<string>(SchemaConstants.Id));
                    if (anytimer == null) return context.Error(GraphQLErrors.UnknownAnyTimer);
                    if (!anytimer.ReceiverId.Equals(user.Uid)) return context.Error(GraphQLErrors.NotReceiver);
                    if (anytimer.Status != AnyTimerStatus.Requested && anytimer.Status != AnyTimerStatus.Disputed &&
                        anytimer.Status != AnyTimerStatus.Edited)
                        return context.Error(GraphQLErrors.NotEditable);

                    var evt = AnyTimerMutations.UpdateStatus(anytimer, AnyTimerStatus.Accepted);

                    // TODO Send push notifications
                    // TODO Add to total of owed for senders.

                    await _repository.Update(anytimer);
                    return evt;
                }
            ).RequiresAuthentication();

            type.FieldAsync<StatusEventType>(
                "disputeAnyTimer",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = SchemaConstants.Id},
                    new QueryArgument<StringGraphType> {Name = SchemaConstants.Message}
                ),
                resolve: async context =>
                {
                    var user = await context.UserRecord();
                    var anytimer = await _repository.ById(context.GetArgument<string>(SchemaConstants.Id));
                    if (anytimer == null) return context.Error(GraphQLErrors.UnknownAnyTimer);
                    if (!anytimer.ReceiverId.Equals(user.Uid)) return context.Error(GraphQLErrors.NotReceiver);
                    if (anytimer.Status != AnyTimerStatus.Requested && anytimer.Status != AnyTimerStatus.Edited)
                        return context.Error(GraphQLErrors.NotEditable);

                    var evt = AnyTimerMutations.UpdateStatus(anytimer, AnyTimerStatus.Accepted,
                        context.GetArgument<string>(SchemaConstants.Message));

                    // Add to total of owed for senders
                    await _repository.Update(anytimer);

                    return evt;
                }
            );
        }
    }
}