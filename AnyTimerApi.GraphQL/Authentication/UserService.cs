using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AnyTimerApi.Database.Models;
using AnyTimerApi.Repository;
using FirebaseAdmin.Auth;

namespace AnyTimerApi.GraphQL.Authentication
{
    public class UserService
    {
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

        private static User RecordToUser(IUserInfo record)
        {
            return new User
            {
                Uid = record.Uid,
                Email = record.Email,
                DisplayName = record.DisplayName,
                PhotoUrl = record.PhotoUrl
            };
        }

        private readonly IUserRepository _userRepository;
        private readonly Dictionary<string, ClaimsPrincipal> _contextBoundUser =
            new Dictionary<string, ClaimsPrincipal>();

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void BindContextUser(ClaimsPrincipal user)
        {
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return;
            _contextBoundUser[userId] = user;
        }

        public void UnBindContextUser(ClaimsPrincipal user)
        {
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return;
            _contextBoundUser.Remove(userId);
        }

        public async Task<User> FromRequest(ClaimsPrincipal principal)
        {
            if (principal == null || !principal.Identity.IsAuthenticated) return null;

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            var displayName = principal.FindFirst(ClaimTypes.Name)?.Value;

            // TODO Get photo url?
            if (userId == null) return null;

            var user = await _userRepository.ById(userId);
            if (user == null)
            {
                var record = await QueryRecord(userId);
                user = new User
                {
                    Uid = record.Uid,
                    Email = record.Email,
                    DisplayName = record.DisplayName,
                    PhotoUrl = record.PhotoUrl
                };
                return user;
            }

            if (string.Equals(email, user.Email) && string.Equals(displayName, user.DisplayName)) return user;

            await _userRepository.SaveUser(user);
            return user;
        }

        public async Task<User> ById(string userId)
        {
            if (userId == null) return null;
            if (_contextBoundUser.ContainsKey(userId)) return await FromRequest(_contextBoundUser[userId]);

            var user = await _userRepository.ById(userId);

            if (user != null) return user;
            var record = await QueryRecord(userId);
            if (record == null) return null;

            user = RecordToUser(record);

            await _userRepository.SaveUser(user);
            return user;
        }
    }
}