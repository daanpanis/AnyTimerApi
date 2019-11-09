using System;
using GraphQL;

namespace AnyTimerApi.GraphQL
{
    public static class GraphQLErrors
    {
        public static readonly GraphQLError AuthenticationRequired =
            new GraphQLError("authentication", "You must be authenticated to do this");

        public static readonly GraphQLError FriendRequestActive =
            new GraphQLError("friend_request_active", "There is already an active friend request");

        public static readonly GraphQLError UnknownUser = new GraphQLError("unknown_user", "No user found by this id");

        public static readonly GraphQLError UnknownFriendRequest =
            new GraphQLError("unknown_friend_request", "No friend request found by this id");

        public static readonly GraphQLError UnknownAnyTimer =
            new GraphQLError("unknown_anytimer", "No anytimer found by this id");

        public static readonly GraphQLError Unauthorized =
            new GraphQLError("unauthorized", "You are not authorized to do this");
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