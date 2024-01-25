using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.UserRoles.Results
{
    public class CreateUserRolesCommandsResults: AppResult<RolesUserListItem>
    {
        public CreateUserRolesCommandsResults(RolesUserListItem item): base(item) { }   
    }
}
