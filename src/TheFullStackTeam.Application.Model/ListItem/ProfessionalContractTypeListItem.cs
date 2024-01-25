using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class ProfessionalContractTypeListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public static implicit operator ProfessionalContractTypeListItem(ProfessionalContractType domainEntity) => new()
        {
            Id = domainEntity.Id,
            Name = domainEntity.Name

        };

        public static Expression<Func<ProfessionalContractType, ProfessionalContractTypeListItem>> Projection =>
            x => new ProfessionalContractTypeListItem
            {
                Id = x.Id,
                Name = x.Name
            };
    }
}
