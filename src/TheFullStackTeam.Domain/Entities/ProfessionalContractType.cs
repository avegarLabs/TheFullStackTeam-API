using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional Contract Type entity
/// </summary>
public class ProfessionalContractType : BaseEntity
{
    public string Name { get; set; } = null!;
    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;

}