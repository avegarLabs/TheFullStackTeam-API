using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional skill entity
/// </summary>
public class ProfessionalJobType : BaseEntity
{
    public string Name { get; set; } = null!;
    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;

}