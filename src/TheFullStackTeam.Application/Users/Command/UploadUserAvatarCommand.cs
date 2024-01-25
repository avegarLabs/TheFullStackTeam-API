using MediatR;
using TheFullStackTeam.Application.Model.POST;
using TheFullStackTeam.Application.Users.Results;

namespace TheFullStackTeam.Application.Users.Command
{
    public class UploadUserAvatarCommand : IRequest<ReadUserProfileInformationResult>
    {
        public string AccountId { get; set; }
        public FilePost? Avatar { get; set; } = null!;

        public UploadUserAvatarCommand(string accountId, FilePost? image)
        {
            AccountId = accountId;
            Avatar = image;
        }
    }
}
