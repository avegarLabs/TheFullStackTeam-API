using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Model.ListItem;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Command
{
    public class UpdateUserProfileCommand:IRequest<ReadUserProfileInformationResult>
    {
        public UserListItem Model { get; set; } = null!;

        public UpdateUserProfileCommand(UserListItem model)
        {
           Model = model;
        }
    }
}
