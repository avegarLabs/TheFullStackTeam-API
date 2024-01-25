using MediatR;
using TheFullStackTeam.Application.Model.POST;
using TheFullStackTeam.Application.Organizations.Results;

namespace TheFullStackTeam.Application.Organizations.Command
{
    public class UploadOrganizationLogoCommand : IRequest<ReadOrganizationDetailsResults>
    {
        public Guid OrganizationId { get; set; }
        public FilePost? Avatar { get; set; } = null!;

        public UploadOrganizationLogoCommand(Guid id, FilePost? image)
        {
            OrganizationId = id;
            Avatar = image;
        }
    }
}
