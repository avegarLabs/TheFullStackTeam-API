using System.Linq.Expressions;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Application.Model.ListItem
{
    public class UserRolesListItem
    {
        public string AccountId { get; set; }
        public string Role { get; set; }

        public static implicit operator UserRolesListItem(User user) => new()
        {
            AccountId = user.AccountId,
            //Role = user.Roles
        };

        public static Expression<Func<User, UserRolesListItem>> Projection =>
            u => new UserRolesListItem
            {
                AccountId= u.AccountId,
              // Role = u.Roles
            };
    }
}
