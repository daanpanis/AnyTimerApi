using System;
using System.Linq;
using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Authentication;
using AnyTimerApi.GraphQL.Extensions;
using AnyTimerApi.GraphQL.Types;
using AnyTimerApi.Repository;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Mutations
{
    public class CommentMutations : IMutation
    {
        private readonly IAnyTimerRepository _repository;
        private readonly ICommentRepository _commentRepository;

        public CommentMutations(IAnyTimerRepository repository, ICommentRepository commentRepository)
        {
            _repository = repository;
            _commentRepository = commentRepository;
        }

        public void SetupMutationDefinitions(ObjectGraphType type)
        {
            type.FieldAsync<CommentType>(
                "addComment",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = SchemaConstants.Id},
                    new QueryArgument<StringGraphType> {Name = SchemaConstants.Message}
                ),
                resolve: async context =>
                {
                    var anytimer = await _repository.ById(context.GetArgument<string>(SchemaConstants.Friends));
                    if (anytimer == null) return context.Error(GraphQLErrors.UnknownAnyTimer);
                    var user = await context.UserRecord();
                    if (!anytimer.ReceiverId.Equals(user.Uid) && !anytimer.CreatorId.Equals(user.Uid) &&
                        !anytimer.Senders.Any(sender => sender.SenderId.Equals(user.Uid)))
                        return context.Error(GraphQLErrors.NotMember);
                    var comment = new Comment
                    {
                        AnyTimerId = anytimer.Id,
                        Time = DateTime.Now,
                        UserId = user.Uid,
                        Text = context.GetArgument<string>(SchemaConstants.Message)
                    };

                    await _commentRepository.Add(comment);
                    return comment;
                }
            ).RequiresAuthentication();

            type.FieldAsync<CommentType>(
                "editComment",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = SchemaConstants.Id},
                    new QueryArgument<DateTimeGraphType> {Name = SchemaConstants.Time},
                    new QueryArgument<StringGraphType> {Name = SchemaConstants.Message}
                ),
                resolve: async context =>
                {
                    var user = await context.UserRecord();
                    var comment = await _commentRepository.Get(
                        context.GetArgument<string>(SchemaConstants.Id),
                        user.Uid,
                        context.GetArgument<DateTime>(SchemaConstants.Time)
                    );
                    if (comment == null) return context.Error(GraphQLErrors.UnknownComment);
                    comment.Edited = true;
                    comment.Text = context.GetArgument<string>(SchemaConstants.Message);

                    await _commentRepository.Update(comment);

                    return comment;
                }
            ).RequiresAuthentication();

            type.FieldAsync<CommentType>(
                "deleteComment",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = SchemaConstants.Name},
                    new QueryArgument<DateTimeGraphType> {Name = SchemaConstants.Time}
                ),
                resolve: async context =>
                {
                    var user = await context.UserRecord();
                    var comment = await _commentRepository.Get(
                        context.GetArgument<string>(SchemaConstants.Id),
                        user.Uid,
                        context.GetArgument<DateTime>(SchemaConstants.Time)
                    );
                    if (comment == null) return context.Error(GraphQLErrors.UnknownComment);
                    await _commentRepository.Delete(comment);
                    return comment;
                }
            ).RequiresAuthentication();
        }
    }
}