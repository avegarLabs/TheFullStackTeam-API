using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem;

public class JobSkillListItem
{
    public Guid Id { get; set; }
    public string SkillName { get; set; } = null!;
    public string? SkillMoniker { get; set; }

    public static implicit operator JobSkillListItem(JobSkill domainEntity) => new()
    {
        Id = domainEntity.Id,
        SkillName = domainEntity.SkillName

    };

    public static Expression<Func<JobSkill, JobSkillListItem>> Projection =>
        x => new JobSkillListItem
        {
            Id = x.Id,
            SkillName = x.SkillName,
            SkillMoniker = x.Skill != null ? x.Skill.Moniker : null
        };
}