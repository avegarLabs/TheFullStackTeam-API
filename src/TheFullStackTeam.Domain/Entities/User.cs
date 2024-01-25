using TheFullStackTeam.Domain.Entities.Base;
using TheFullStackTeam.Domain.ValueObjects;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// User profile entity
/// </summary>
public class User : NicknamedEntity
{
    public const int NameMaxLenght = 150;
    public const int PhoneMaxLenght = 20;
    public const int ContactEmailMaxLenght = 150;

    public string AccountId { get; set; } = null!;
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Phone { get; set; }
    public string? ContactEmail { get; set; }
    public ImageUrl? Picture { get; set; } = null!;
    public Address? Address { get; set; } = null!;

    public Guid? CountryId { get; set; }
    public virtual Country? Country { get; set; } = null!;

    public virtual Professional Professional { get; set; } = null!;

    public virtual ICollection<Organization> Organizations { get; set; } = null!;

    public virtual ICollection<UserRoles> UserRoles { get; set; } = null!;


}