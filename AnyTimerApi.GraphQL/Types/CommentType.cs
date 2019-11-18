using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Extensions;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class CommentType : ObjectGraphType<Comment>
    {
        public CommentType()
        {
            Name = "Comment";
            FieldAsync<UserType>(
                "user",
                resolve: async context => await context.UserRecord(context.Source.UserId)
            );
            Field(c => c.Time);
            Field(c => c.Text);
            Field(c => c.Edited);
        }
    }
}