using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional skill entity
/// </summary>
public class JobsJobType : BaseEntity
{
    public string JobTypeName { get; set; } = null!;

    public Guid JobId { get; set; }
    public virtual Job Jobs { get; set; } = null!;

}