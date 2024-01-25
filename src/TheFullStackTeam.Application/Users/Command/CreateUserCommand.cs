using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Command
{
    public class CreateUserCommand : IRequest<ReadUserProfileInformationResult>
    {
        public UserModel UserModel { get; set; }

        public CreateUserCommand(UserModel userModel)
        {
            UserModel = userModel;
        }
    }
}
