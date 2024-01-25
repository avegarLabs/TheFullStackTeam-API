using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Command
{
    public class AddUserContactInformationCommand : IRequest<UserContactInformationResult>
    {
        public string AccountId { get; set; }
        public ContactInformationModel ContactInformation { get; set; }

        public AddUserContactInformationCommand(string accountId, ContactInformationModel contactInformation)
        {
            AccountId = accountId;
            ContactInformation = contactInformation;
        }
    }
}
