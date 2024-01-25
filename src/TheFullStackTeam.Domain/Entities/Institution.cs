using TheFullStackTeam.Domain.Entities.Base;
using TheFullStackTeam.Domain.ValueObjects;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Institution entity
/// </summary>
public class Institution : NicknamedEntity
{
    public const int NameMaxLenght = 250;
    public const int DescriptionMaxLenght = 1024;

    public string Name { get; set; } = null!;
    public ImageUrl Logo { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? City { get; set; } = null!;

    public Guid? CountryId { get; set; }
    public virtual Country? Country { get; set; }

    public virtual ICollection<Position> Positions { get; set; } = null!;

    public virtual ICollection<Title> Titles { get; set; } = null!;

}