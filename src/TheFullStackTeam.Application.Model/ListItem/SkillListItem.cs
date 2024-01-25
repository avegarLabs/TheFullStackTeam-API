using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class SkillListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Moniker { get; set; } = null!;

        public List<Guid> Categories { get; set; } = new List<Guid>();

        public static implicit operator SkillListItem(Skill domainEntity) => new()
        {
            Id = domainEntity.Id,
            Moniker = domainEntity.Moniker,
            Name = domainEntity.Name,
            Categories = domainEntity.SkillCategories.AsQueryable().Select(a => a.CategoryId).ToList(),

        };

        public static Expression<Func<Skill, SkillListItem>> Projection =>
            x => new SkillListItem
            {
                Id = x.Id,
                Name = x.Name,
                Moniker = x.Moniker,
                Categories = x.SkillCategories.AsQueryable().Select(a => a.CategoryId).ToList(),
            };
    }

}
