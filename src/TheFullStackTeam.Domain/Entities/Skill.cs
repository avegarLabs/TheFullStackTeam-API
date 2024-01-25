using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

/// <summary>
/// Skill entity
/// </summary>
public class Skill : NicknamedEntity
{
    public const int NameMaxLenght = 50;

    public string Name { get; set; } = null!;

    public virtual ICollection<ProfessionalSkill> ProfessionalSkills { get; set; } = null!;
    public virtual ICollection<SkillCategory> SkillCategories { get; set; } = null!;

    public virtual ICollection<JobSkill> JobSkills { get; set; } = null!;
    public virtual ICollection<SkillPortfolio> SkillPortsfolio { get; set; } = null!;
}