using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class OrganizationServiceListItem
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = null!;
        public double Price { get; set; }

        public string Currency { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;

        public List<SkillListItem> skillList { get; set; } = new();

        public static implicit operator OrganizationServiceListItem(OrganizationSevices domainEntity) => new()
        {
            Id = domainEntity.Id,
            ServiceName = domainEntity.ServiceName,
            Price = domainEntity.SevicePrice,
            ServiceDescription = domainEntity.ServiceDescription,
        };

        public static Expression<Func<OrganizationSevices, OrganizationServiceListItem>> Projection =>
            x => new OrganizationServiceListItem
            {
                Id = x.Id,
                ServiceName = x.ServiceName,
                Price = x.SevicePrice,
                Currency = x.Currency,
                ServiceDescription = x.ServiceDescription,
                skillList = x.ServiceSkills.AsQueryable().Select(SkillListItem.Projection).ToList()
            };

    }
}
