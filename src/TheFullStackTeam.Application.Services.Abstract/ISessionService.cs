namespace Suited.ConnectionString.Resolver.Services.Contracts;

/// <summary>
/// 
/// </summary>
public interface ISessionService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    string GetCurrentUserName();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Guid GetCurrentUserIdentifier();

    /// <summary>
    /// Retrieve a account id
    /// </summary>
    /// <returns></returns>
    string AccountId();

    /// <summary>
    /// Retrieve if the user authenticated is admin
    /// </summary>
    /// <returns></returns>
    bool IsAdmin();
}