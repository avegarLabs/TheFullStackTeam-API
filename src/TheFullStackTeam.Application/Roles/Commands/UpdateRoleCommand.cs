using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Roles.Results;

namespace TheFullStackTeam.Application.Roles.Commands
{
    public class UpdateRoleCommand: IRequest<RolesCommandsResult>
    {
        public Guid RoleId { get; set; }
        public RolesListItem Model { get; set; }

        public UpdateRoleCommand(Guid id, RolesListItem model)
        {
            RoleId = id;
            Model = model;
        }
    }
}
