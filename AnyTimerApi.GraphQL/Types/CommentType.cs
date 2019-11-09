using AnyTimerApi.Database.Entities;
using AnyTimerApi.GraphQL.Authentication;
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
                resolve: async context => await UserService.ById(context.Source.UserId)
            );
            Field(c => c.Time);
            Field(c => c.Text);
        }
    }
}