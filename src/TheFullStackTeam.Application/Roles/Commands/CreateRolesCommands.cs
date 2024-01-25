using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Roles.Results;

namespace TheFullStackTeam.Application.Roles.Commands
{
    public class CreateRolesCommands: IRequest<RolesCommandsResult>
    {
        public RolesModel Model { get; set; }  

        public CreateRolesCommands(RolesModel model) { Model = model; }
    }
}
