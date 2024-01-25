using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.UserRoles.Results;

namespace TheFullStackTeam.Application.UserRoles.Commands
{
    public class CreateUserRolesCommands:IRequest<CreateUserRolesCommandsResults>
    {
        public Guid UserId { get; set; }
        public RolesListItem Model { get; set; }
        public CreateUserRolesCommands( Guid id, RolesListItem model) { 
        
            UserId = id;    
            Model = model;
        }
    }
}
