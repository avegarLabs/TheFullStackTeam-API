using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Command
{
    public class RegisterUserCommand : IRequest<ReadUserProfileInformationResult>
    {
        public UserModel UserModel { get; set; }

        public RegisterUserCommand(UserModel userModel)
        {
            UserModel = userModel;
        }
    }
}
