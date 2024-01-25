namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional certification
/// </summary>
public class Certification : Title
{
    public const int AuthorityMaxLenght = 150;
    public const int LicenceNumberMaxLenght = 50;
    public const int UrlMaxLenght = 250;

    public Certification()
    {
        TitleType = nameof(Certification);
    }

    public string Authority { get; set; } = null!;
    public string LicenseNumber { get; set; } = null!;
    public string? Url { get; set; }
}