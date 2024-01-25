using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Country entity
/// </summary>
public class Country : BaseEntity
{
    public const int CommonNameMaxLenght = 100;
    public const int OfficialNameMaxLenght = 100;
    public const int NativeNameMaxLenght = 100;
    public const int TldMaxLenght = 10;
    public const int Cca2MaxLenght = 2;
    public const int Cca3MaxLenght = 3;
    public const int Ccn3MaxLenght = 3;

    public string CommonName { get; set; } = null!;
    public string OfficialName { get; set; } = null!;
    public string NativeName { get; set; } = null!;
    public string Tld { get; set; } = null!;
    public string Cca2 { get; set; } = null!;
    public string Cca3 { get; set; } = null!;
    public string Ccn3 { get; set; } = null!;

    public virtual ICollection<User> UserProfiles { get; set; } = null!;
    public virtual ICollection<Professional> Professionals { get; set; } = null!;

    public virtual ICollection<Organization> Organizations { get; set; } = null!;

    public virtual ICollection<Institution> Institutions { get; set; } = null!;

    public virtual ICollection<Cities> Cities { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set; } = null!;
}