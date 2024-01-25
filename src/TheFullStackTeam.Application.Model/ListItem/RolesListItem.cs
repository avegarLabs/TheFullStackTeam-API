using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class RolesListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Moniker { get; set; } = null!;

        public static implicit operator RolesListItem(Roles domainEntity) => new()
        {
            Id = domainEntity.Id,
            Moniker = domainEntity.Moniker,
            Name = domainEntity.Name,
        };

        public static Expression<Func<Roles, RolesListItem>> Projection =>
            x => new RolesListItem
            {
                Id = x.Id,
                Name = x.Name,
                Moniker = x.Moniker,
            };
    }

}
