using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class ProfessionalSkillListItem
    {
        public Guid Id { get; set; }
        public string SkillName { get; set; } = null!;
        public int SkillLevel { get; set; }
        public string? SkillMoniker { get; set; }

        public static implicit operator ProfessionalSkillListItem(ProfessionalSkill domainEntity) => new()
        {
            Id = domainEntity.Id,
            SkillName = domainEntity.SkillName,
            SkillLevel = domainEntity.SkillLevel,
        };

        public static Expression<Func<ProfessionalSkill, ProfessionalSkillListItem>> Projection =>
            x => new ProfessionalSkillListItem
            {
                Id = x.Id,
                SkillName = x.SkillName,
                SkillLevel = x.SkillLevel,
                SkillMoniker = x.Skill != null ? x.Skill.Moniker : null
            };
    }
}
