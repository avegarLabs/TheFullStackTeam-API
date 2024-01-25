namespace TheFullStackTeam.RolesMemoryCache
{
    public interface IRolesMemoryCache
    {
        Task<List<string>> GetUserRoles(string accountId);

        Task<bool> ClearCache(string accountId);
    }
}
