using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class ProfessionalServiceListItem
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; } = null!;
        public double Price { get; set; }

        public string Currency { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;

        public List<SkillListItem> skillList { get; set; } = new();

        public static implicit operator ProfessionalServiceListItem(ProfessionalSevices domainEntity) => new()
        {
            Id = domainEntity.Id,
            ServiceName = domainEntity.ServiceName,
            Price = domainEntity.SevicePrice,
            ServiceDescription = domainEntity.ServiceDescription,
            Currency = domainEntity.Currency
        };

        public static Expression<Func<ProfessionalSevices, ProfessionalServiceListItem>> Projection =>
            x => new ProfessionalServiceListItem
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
