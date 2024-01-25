using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFullStackTeam.Application.Users.Results
{
    public class DeleteCommandResult: AppResult<bool>
    {
        public DeleteCommandResult(bool success):base(success) { }
    }
}
