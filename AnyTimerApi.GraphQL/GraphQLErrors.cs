using System;
using GraphQL;
using GraphQL.Validation;

namespace AnyTimerApi.GraphQL
{
    public static class GraphQLErrors
    {
        public static readonly GraphQLError AuthenticationRequired =
            new GraphQLError("authentication", "You must be authenticated to do this");

        public static readonly GraphQLError FriendRequestActive =
            new GraphQLError("friend_request_active", "There is already an active friend request");
    }

    public class GraphQLError
    {
        public string KeyCode { get; }
        public string Message { get; }

        public GraphQLError(string keyCode, string message)
        {
            KeyCode = keyCode;
            Message = message;
        }

        public ExecutionError Build()
        {
            return new ExecutionError(Message, (Exception) null)
            {
                Code = KeyCode
            };
        }
    }
}