using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AnyTimerApi.Utilities;
using FirebaseAdmin.Auth;

namespace AnyTimerApi.GraphQL.Authentication
{
    public static class UserService
    {
        public static int CacheSize { get; set; } = 250;
        private static readonly LinkedDictionary<string, UserRecord> Cache = new LinkedDictionary<string, UserRecord>();

        private static readonly Dictionary<string, ClaimsPrincipal> ContextBoundUser =
            new Dictionary<string, ClaimsPrincipal>();

        public static void BindContextUser(ClaimsPrincipal user)
        {
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return;
            ContextBoundUser[userId] = user;
        }

        public static void UnBindContextUser(ClaimsPrincipal user)
        {
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return;
            ContextBoundUser.Remove(userId);
        }

        private static async Task<UserRecord> QueryRecord(string userId)
        {
            if (userId == null)
            {
                return null;
            }

            try
            {
                return await FirebaseAuth.DefaultInstance.GetUserAsync(userId);
            }
            catch (FirebaseAuthException)
            {
                return null;
            }
        }


        public static async Task<UserRecord> FromRequest(ClaimsPrincipal user)
        {
            if (user == null || !user.Identity.IsAuthenticated) return null;

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var displayName = user.FindFirst(ClaimTypes.Name)?.Value;
            if (userId == null) return null;
            if (!Cache.ContainsKey(userId)) return AddRecord(await QueryRecord(userId));

            var cached = Cache[userId];
            if ((email != null && !cached.Email.Equals(email)) ||
                (displayName != null && !cached.DisplayName.Equals(displayName)))
            {
                return MarkActive(await QueryRecord(userId));
            }

            return MarkActive(cached);
        }

        public static async Task<UserRecord> ById(string userId)
        {
            if (userId == null) return null;
            if (ContextBoundUser.ContainsKey(userId)) return await FromRequest(ContextBoundUser[userId]);
            if (Cache.ContainsKey(userId)) return MarkActive(Cache[userId]);
            var userRecord = await QueryRecord(userId);
            return userRecord == null ? null : AddRecord(userRecord);
        }

        private static UserRecord AddRecord(UserRecord record)
        {
            if (record == null) return null;
            Cache.Add(record.Uid, record);
            while (Cache.Count > CacheSize)
            {
                Cache.RemoveLast();
            }

            return record;
        }

        private static UserRecord MarkActive(UserRecord record)
        {
            if (record == null) return null;
            Cache[record.Uid] = record;
            return record;
        }
    }
}