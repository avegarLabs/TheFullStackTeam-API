namespace TheFullStackTeam.Configuration;

/// <summary>
/// The email settings.
/// </summary>
public class EmailSettings
{
    public string SmtpAddress { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
}
