using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Command
{
    public class DeleteUserProfileCommand: IRequest<DeleteCommandResult>
    {
        public Guid UserId { get; set; }    
        public DeleteUserProfileCommand(Guid id) {
          UserId = id;   
        }
    }
}
