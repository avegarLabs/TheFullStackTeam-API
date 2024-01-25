using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFullStackTeam.Application.Roles.Results
{
    public class RolesDeleteResults: AppResult<bool>
    {
        public RolesDeleteResults(bool success): base(success) { }
    }
}
