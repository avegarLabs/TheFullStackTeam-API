using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Cities entity
/// </summary>
public class Cities : BaseEntity
{
    public const int NameMaxLenght = 500;
    

    public string Name { get; set; } = null!;

    public Guid? CountryId { get; set; }
    public virtual Country? Country { get; set; }

}