using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Roles entity
/// </summary>
public class Roles : NicknamedEntity
{
    public const int NameMaxLenght = 50;

    public string Name { get; set; } = null!;

    public virtual ICollection<UserRoles> UserRoles { get; set; } = null!;
   

}