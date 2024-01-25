using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class SkillPortfolioListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Version { get; set; } = null!;

        public static implicit operator SkillPortfolioListItem(SkillPortfolio domainEntity) => new()
        {
            Id = domainEntity.Id,
            Name = domainEntity.Skill.Name,
            Version = domainEntity.SkillVersion
        };

        public static Expression<Func<SkillPortfolio, SkillPortfolioListItem>> Projection =>
            x => new SkillPortfolioListItem
            {
                Id = x.Id,
                Name = x.Skill.Name,
                Version = x.SkillVersion
            };
    }

}
