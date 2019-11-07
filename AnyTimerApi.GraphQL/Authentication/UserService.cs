using System.Security.Claims;
using System.Threading.Tasks;
using AnyTimerApi.Utilities;
using FirebaseAdmin.Auth;

namespace AnyTimerApi.GraphQL.Authentication
{
    public class UserService
    {
        private static async Task<UserRecord> QueryRecord(string userId)
        {
            try
            {
                return await FirebaseAuth.DefaultInstance.GetUserAsync(userId);
            }
            catch (FirebaseAuthException)
            {
                return null;
            }
        }
        
        private readonly uint _cacheSize;
        private readonly LinkedDictionary<string, UserRecord> _cache = new LinkedDictionary<string, UserRecord>();

        public UserService(uint cacheSize)
        {
            _cacheSize = cacheSize;
        }

        private async Task<UserRecord> FromRequest(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var record = userId != null ? await ById(userId) : null;
            if (record == null) return null;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var displayName = user.FindFirst(ClaimTypes.Name)?.Value;
            if ((email != null && !record.Email.Equals(email)) ||
                (displayName != null && !record.DisplayName.Equals(displayName)))
            {
                MarkActive(record = await QueryRecord(userId));
            }

            return record;
        }

        public async Task<UserRecord> ById(string userId)
        {
            if (_cache.ContainsKey(userId)) return MarkActive(_cache[userId]);
            var userRecord = await QueryRecord(userId);
            if (userRecord == null) return null;
            AddRecord(userRecord);
            return userRecord;
        }

        private void AddRecord(UserRecord record)
        {
            _cache.Add(record.Uid, record);
            while (_cache.Count > _cacheSize)
            {
                _cache.RemoveLast();
            }
        }

        private UserRecord MarkActive(UserRecord record)
        {
            _cache[record.Uid] = record;
            return record;
        }
    }
}