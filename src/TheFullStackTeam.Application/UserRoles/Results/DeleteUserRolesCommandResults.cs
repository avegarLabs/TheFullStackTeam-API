using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFullStackTeam.Application.UserRoles.Results
{
    public class DeleteUserRolesCommandResults: AppResult<bool>
    {
        public DeleteUserRolesCommandResults(bool success): base(success) { }
    }
}
