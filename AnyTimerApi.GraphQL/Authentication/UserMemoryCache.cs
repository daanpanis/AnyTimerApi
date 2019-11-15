using AnyTimerApi.Database.Models;
using AnyTimerApi.Utilities;
using FirebaseAdmin.Auth;

namespace AnyTimerApi.GraphQL.Authentication
{
    public class UserMemoryCache
    {
        public static int CacheSize { get; set; } = 250;
        private readonly LinkedDictionary<string, User> _cache = new LinkedDictionary<string, User>();

        public bool HasCached(string userId)
        {
            return _cache.ContainsKey(userId);
        }

        public User Get(string userId)
        {
            return MarkActive(_cache[userId]);
        }

        public User Cache(User user)
        {
            if (user == null) return null;
            _cache.Add(user.Uid, user);
            while (_cache.Count > CacheSize)
            {
                _cache.RemoveLast();
            }

            return user;
        }

        private User MarkActive(User user)
        {
            if (user == null) return null;
            _cache[user.Uid] = user;
            return user;
        }
    }
}