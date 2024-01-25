using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Domain.Entities;

public class SkillCategory : BaseEntity
{
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;

    public Guid SkillId { get; set; }
    public virtual Skill Skill { get; set; } = null!;
}
