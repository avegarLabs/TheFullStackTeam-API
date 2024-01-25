using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional languege entity
/// </summary>
public class ProfessionalLanguage : BaseEntity
{
    public string LanguegeName { get; set; } = null!;
    public string Level { get; set; }

    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;

    public Guid? LanguegeId { get; set; }
    public virtual Language? Language { get; set; }
}