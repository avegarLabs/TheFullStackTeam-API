using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.UserRoles.Results;

namespace TheFullStackTeam.Application.UserRoles.Commands
{
    public class DeleteUserRolesCommands: IRequest<DeleteUserRolesCommandResults>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public DeleteUserRolesCommands(Guid userId, Guid roleId)
        {
            UserId= userId;
            RoleId= roleId;
        }
    }
}
