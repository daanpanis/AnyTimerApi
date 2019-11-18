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

        public static GraphQLError UnknownUser(string userId = null)
        {
            return new GraphQLError("unknown_user",
                "No user found by " + (userId != null ? $"id {userId}" : "this id"));
        }

        public static GraphQLError NotFriends(string userId = null)
        {
            return new GraphQLError("not_friends",
                "You must be friends with " + (userId ?? "this user") + " to perform this action");
        }

        public static readonly GraphQLError UnknownFriendRequest =
            new GraphQLError("unknown_friend_request", "No friend request found by this id");

        public static readonly GraphQLError UnknownAnyTimer =
            new GraphQLError("unknown_anytimer", "No anytimer found by this id");

        public static readonly GraphQLError Unauthorized =
            new GraphQLError("unauthorized", "You are not authorized to do this");

        public static readonly GraphQLError NoSenders =
            new GraphQLError("no_senders", "There must be at least one sender");

        public static readonly GraphQLError ReceiverSameAsSender = new GraphQLError("receiver_same_as_sender",
            "The receiver can't be the same as one of the senders'");

        public static readonly GraphQLError NotReceiver =
            new GraphQLError("not_receiver", "You are not the receiver of this anytimer");

        public static readonly GraphQLError NotEditable =
            new GraphQLError("not_editable", "This anytimer isn't editable");

        public static readonly GraphQLError NotCreator =
            new GraphQLError("not_creator", "You are not the creator of this anytimer");

        public static readonly GraphQLError NotMember =
            new GraphQLError("not_member", "You are not a member of this anytimer");

        public static readonly GraphQLError UnknownComment = new GraphQLError("unknown_comment", "No comment found");
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