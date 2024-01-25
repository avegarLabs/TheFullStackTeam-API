using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.GET;

public class SkillCategoryGet
{
    public Guid Id { get; set; }

    public static implicit operator SkillCategoryGet(SkillCategory domainEntity) => new()
    {
        Id = domainEntity.Id,
    };

    public static Expression<Func<SkillCategory, SkillCategoryGet>> Projection =>
      x => new SkillCategoryGet
      {
          Id = x.Id,
      };
}
