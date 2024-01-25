using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.RolesMemoryCache
{
    public class RolesMemoryCache : IRolesMemoryCache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TheFullStackTeamDbContext _context;


        public RolesMemoryCache(IMemoryCache cache, TheFullStackTeamDbContext context)
        {
            _memoryCache = cache;
            _context = context;
        }

       public async  Task<bool> ClearCache(string accountId)
        {
            var cacheKey = $"UserRoles-{accountId}";
            _memoryCache.Remove(cacheKey);
            return true;
        }

       public async Task<List<string>> GetUserRoles(string accountId)
        {
            var cacheKey = $"UserRole-{accountId}";

            if (_memoryCache.TryGetValue(cacheKey, out List<string> userRoles))
            {
                return userRoles;
            }
            else
            {
                userRoles = await _context.UserRole.Where(u => u.User.AccountId.Equals(accountId)).Select(u => u.RoleName).ToListAsync();
                if (userRoles == null)
                {
                    throw new Exception("Error");
                }
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _memoryCache.Set(cacheKey, userRoles, cacheOptions);
            }
            return userRoles;
        }
    }
}
