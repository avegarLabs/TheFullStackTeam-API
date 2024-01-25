using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class ProfessionalJobTypeListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public static implicit operator ProfessionalJobTypeListItem(ProfessionalJobType domainEntity) => new()
        {
            Id = domainEntity.Id,
            Name = domainEntity.Name,
        };

        public static Expression<Func<ProfessionalJobType, ProfessionalJobTypeListItem>> Projection =>
            x => new ProfessionalJobTypeListItem
            {
                Id = x.Id,
                Name = x.Name,
            };
    }
}
