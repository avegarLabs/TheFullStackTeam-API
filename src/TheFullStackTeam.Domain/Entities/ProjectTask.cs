using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class ProjectTask : NicknamedEntity
{
    public const int NameMaxLength = 100;
    public const int DescriptionMaxLength = 255;

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public Guid ProjectId { get; set; }
    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<Effort> Efforts { get; set; } = null!;
}