using Suited.ConnectionString.Resolver.Services.Contracts;
using System.Security.Claims;
using TheFullStackTeam.Domain.Entities.Enumerations;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.API;

/// <summary>
/// Session service
/// </summary>
public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TheFullStackTeamDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="context"></param>
    public SessionService(IHttpContextAccessor httpContextAccessor, TheFullStackTeamDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    /// <summary>
    /// Retrieve a login account id as Guid
    /// </summary>
    /// <returns></returns>
    public Guid GetCurrentUserIdentifier()
    {
        var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(id ?? Guid.Empty.ToString());
    }

    /// <summary>
    /// Retrieve a login account id as string
    /// </summary>
    /// <returns></returns>
    public string AccountId()
    {
        return GetCurrentUserIdentifier().ToString();
    }

    /// <summary>
    /// Check if user is admin
    /// </summary>
    /// <returns></returns>
   public bool IsAdmin()
    {
        var accountId = AccountId();
        var user = _context.Users.SingleOrDefault(x => x.AccountId == accountId);
        // return user != null && user.Roles.Split(";").Contains(Role.Admin.Name);
        return true;
    }

    /// <summary>
    /// Retrieve a login account user name
    /// </summary>
    /// <returns></returns>
    public string GetCurrentUserName()
    {
        var user = _httpContextAccessor.HttpContext?.User.FindFirst("emails")?.Value;
        return user ?? string.Empty;


    }

}