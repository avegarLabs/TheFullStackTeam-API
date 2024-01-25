namespace TheFullStackTeam.Aplication.Model.Settings;

/// <summary>
/// Azure AD B2C settigns
/// </summary>
public class AzureAdB2C
{
    /// <summary>
    /// Gets or sets the tenant.
    /// </summary>
    public string Tenant { get; set; }
    /// <summary>
    /// Gets or sets the instance.
    /// </summary>
    public string Instance { get; set; }
    /// <summary>
    /// Gets or sets the client id.
    /// </summary>
    public string ClientId { get; set; }
    /// <summary>
    /// Gets or sets the domain.
    /// </summary>
    public string Domain { get; set; }
    /// <summary>
    /// Gets or sets the signed out callback path.
    /// </summary>
    public string SignedOutCallbackPath { get; set; }
    /// <summary>
    /// Gets or sets the sign up sign in policy id.
    /// </summary>
    public string SignUpSignInPolicyId { get; set; }

    public string ClientSecret { get; set; }
    /// <summary>
    /// Gets or sets the b2c extension app client id.
    /// </summary>
    public string B2cExtensionAppClientId { get; set; }
}

