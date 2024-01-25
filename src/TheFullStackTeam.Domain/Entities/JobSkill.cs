using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Professional skill entity
/// </summary>
public class JobSkill : BaseEntity
{
    public const int SkillNameMaxLenght = 150;

    public string SkillName { get; set; } = null!;
   
    public Guid JobId { get; set; }
    public virtual Job Jobs { get; set; } = null!;

    public Guid? SkillId { get; set; }
    public virtual Skill? Skill { get; set; }
}