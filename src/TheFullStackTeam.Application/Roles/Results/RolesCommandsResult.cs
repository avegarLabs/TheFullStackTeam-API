using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Roles.Results
{
    public class RolesCommandsResult: AppResult<RolesListItem>
    {
        public RolesCommandsResult(RolesListItem model): base(model) { }
    }
}
