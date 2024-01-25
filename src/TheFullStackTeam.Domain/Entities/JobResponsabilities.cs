using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Job Responsabilities entity
/// </summary>
public class JobResponsabilities : BaseEntity
{
    public string ResposabilityDescription { get; set; } = null!;

    public Guid JobId { get; set; }
    public virtual Job Jobs { get; set; } = null!;

}