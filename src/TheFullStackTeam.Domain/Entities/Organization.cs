using TheFullStackTeam.Domain.Entities.Base;
using TheFullStackTeam.Domain.ValueObjects;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Organization entity
/// </summary>
public class Organization : NicknamedEntity
{
    public const int NameMaxLenght = 250;
    public const int DescriptionMaxLenght = 1024;
    public const int ContactEmailMaxLenght = 150;

    public string Name { get; set; } = null!;
    public ImageUrl Logo { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string OrganizationWeb { get; set; } = null!;

    public string LinkedInProfile { get; set; } = null!;

    public string YoutubeProfile { get; set; } = null!;

    public string Phone { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;

    public string Zise { get; set; } = null!;

    public string Sector { get; set; } = null!;

    public Address? Address { get; set; } = null!;

    public Guid? CountryId { get; set; }
    public virtual Country? Country { get; set; }

    public virtual ICollection<Title> Titles { get; set; } = null!;
    public virtual ICollection<Honor> Honors { get; set; } = null!;

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Position> Positions { get; set; } = null!;
    public virtual ICollection<OrganizationSevices> OrganizationSevices { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = null!;

}