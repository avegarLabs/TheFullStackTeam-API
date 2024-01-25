using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional skill entity
/// </summary>
public class ProfessionalSkill : BaseEntity
{
    public const int SkillNameMaxLenght = 150;

    public string SkillName { get; set; } = null!;
    public int SkillLevel { get; set; }

    public Guid ProfessionalId { get; set; }
    public virtual Professional Professional { get; set; } = null!;

    public Guid? SkillId { get; set; }
    public virtual Skill? Skill { get; set; }
}