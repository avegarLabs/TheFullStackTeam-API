namespace TheFullStackTeam.Aplication.Model.Settings;

/// <summary>
/// Ms Graph Api settigns
/// </summary>
public class GraphApi
{
    /// <summary>
    /// Gets or sets the tenant.
    /// </summary>
    public string TenantId { get; set; }
    /// <summary>
    /// Gets or sets the client id.
    /// </summary>
    public string ClientId { get; set; }

    public string Scopes{ get; set; }

    public string ClientSecret { get; set; }
}

