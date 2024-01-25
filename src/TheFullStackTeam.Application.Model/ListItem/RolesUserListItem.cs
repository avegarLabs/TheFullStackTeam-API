using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class RolesUserListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Moniker { get; set; }


        public static implicit operator RolesUserListItem(UserRoles domainEntity) => new()
        {
            Id = domainEntity.Id,
            Name = domainEntity.RoleName,
            Moniker = domainEntity.Roles.Moniker

        };

        public static Expression<Func<UserRoles, RolesUserListItem>> Projection =>
            x => new RolesUserListItem
            {
                Id = x.Id,
                Name = x.RoleName,
                Moniker = x.Roles.Moniker
            };
    }
}
