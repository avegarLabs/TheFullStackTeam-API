using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Roles.Results;

namespace TheFullStackTeam.Application.Roles.Commands
{
    public class DeleteRolesCommands: IRequest<RolesDeleteResults>
    {
        public Guid RoleId { get; set; }    

        public DeleteRolesCommands(Guid id)
        {
            RoleId = id;
        }
    }
}
