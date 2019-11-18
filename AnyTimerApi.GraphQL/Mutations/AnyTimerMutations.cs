using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Authentication;
using AnyTimerApi.GraphQL.Extensions;
using AnyTimerApi.GraphQL.InputTypes;
using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Mutations
{
    public class AnyTimerMutations : IMutation
    {
        public static StatusEvent UpdateStatus(AnyTimer anytimer, AnyTimerStatus status, string message = null)
        {
            anytimer.Status = status;
            anytimer.LastUpdated = DateTime.Now;
            if (anytimer.StatusEvents == null) anytimer.StatusEvents = new List<StatusEvent>();
            var statusEvent = new StatusEvent
            {
                Status = status,
                AnyTimerId = anytimer.Id,
                EventTime = anytimer.LastUpdated,
                Message = message
            };
            anytimer.StatusEvents.Add(statusEvent);
            return statusEvent;
        }

        private readonly IAnyTimerRepository _repository;
        private readonly IFriendRequestRepository _friendRepository;

        public AnyTimerMutations(IAnyTimerRepository repository, IFriendRequestRepository friendRepository)
        {
            _repository = repository;
            _friendRepository = friendRepository;
        }

        public void SetupMutationDefinitions(ObjectGraphType type)
        {
            type.FieldAsync<AnyTimerType>(
                "newAnyTimer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<NewAnyTimerInputType>> {Name = SchemaConstants.Args}
                ),
                resolve: async context =>
                {
                    var args = context.GetArgument<NewAnyTimerInputType>(SchemaConstants.Args);
                    if (args.Senders.Count == 0)
                        return (AnyTimer) context.Error(GraphQLErrors.NoSenders);

                    var user = await context.UserRecord();
                    if (string.Equals(user.Uid, args.Receiver))
                        return context.Error(GraphQLErrors.ReceiverSameAsSender);
                    var anytimer = new AnyTimer
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatorId = user.Uid,
                        CreatedTime = DateTime.Now,
                        LastUpdated = DateTime.Now,
                        Status = AnyTimerStatus.Requested,
                        Reason = args.Reason,
                    };
                    var receiver = await context.UserRecord(args.Receiver);
                    if (receiver == null)
                        return (AnyTimer) context.Error(GraphQLErrors.UnknownUser(args.Receiver));
                    if ((await _friendRepository.ByUsers(user.Uid, receiver.Uid))?.Status !=
                        FriendRequestStatus.Accepted)
                        return (AnyTimer) context.Error(GraphQLErrors.NotFriends(receiver.Uid));
                    anytimer.ReceiverId = receiver.Uid;

                    await ResolveSenders(args, anytimer, context);

                    if (context.Errors.Count > 0) return null;

                    UpdateStatus(anytimer, AnyTimerStatus.Requested);

                    await _repository.Add(anytimer);
                    // TODO Send push notifications

                    return anytimer;
                }
            ).RequiresAuthentication();

            type.FieldAsync<AnyTimerType>(
                "editAnyTimer",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = SchemaConstants.Id},
                    new QueryArgument<AnyTimerInputType> {Name = SchemaConstants.Args}
                ),
                resolve: async context =>
                {
                    var args = context.GetArgument<AnyTimerInputType>(SchemaConstants.Args);
                    if (args.Senders.Count == 0)
                        return (AnyTimer) context.Error(GraphQLErrors.NoSenders);

                    var anytimer = await _repository.ById(context.GetArgument<string>(SchemaConstants.Id));
                    if (anytimer == null) return context.Error(GraphQLErrors.UnknownAnyTimer);
                    var user = await context.UserRecord();
                    if (!anytimer.CreatorId.Equals(user.Uid)) return context.Error(GraphQLErrors.NotCreator);
                    if (anytimer.Status == AnyTimerStatus.Cancelled || anytimer.Status == AnyTimerStatus.Accepted)
                        return context.Error(GraphQLErrors.NotEditable);

                    anytimer.Reason = args.Reason;
                    UpdateStatus(anytimer, AnyTimerStatus.Edited);

                    await ResolveSenders(args, anytimer, context);

                    if (context.Errors.Count > 0) return null;

                    await _repository.Update(anytimer);

                    return anytimer;
                }
            ).RequiresAuthentication();
        }

        private Task ResolveSenders(AnyTimerInputType args, AnyTimer anyTimer,
            ResolveFieldContext<object> context)
        {
            return Task.WhenAll(args.Senders.Select(async senderInput =>
            {
                if (anyTimer.ReceiverId.Equals(senderInput.User))
                {
                    context.Error(GraphQLErrors.ReceiverSameAsSender);
                    return;
                }

                if (!senderInput.User.Equals(anyTimer.CreatorId))
                {
                    var sender = await context.UserRecord(senderInput.User);
                    if (sender == null)
                    {
                        context.Error(GraphQLErrors.UnknownUser(senderInput.User));
                        return;
                    }

                    if ((await _friendRepository.ByUsers(anyTimer.CreatorId, sender.Uid))?.Status !=
                        FriendRequestStatus.Accepted)
                    {
                        context.Error(GraphQLErrors.NotFriends(sender.Uid));
                        return;
                    }
                }

                anyTimer.Senders.Add(new AnyTimerSender
                {
                    SenderId = senderInput.User,
                    Amount = senderInput.Amount,
                    AnyTimerId = anyTimer.Id
                });
            }));
        }
    }
}