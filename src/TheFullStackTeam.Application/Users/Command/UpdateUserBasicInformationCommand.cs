using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Command
{
    public class UpdateUserBasicInformationCommand : IRequest<ReadUserProfileInformationResult>
    {
        public string AccountId { get; set; }
        public UserModel Model { get; set; }

        public UpdateUserBasicInformationCommand(string accountId, UserModel model)
        {
            AccountId = accountId;
            Model = model;
        }
    }
}
