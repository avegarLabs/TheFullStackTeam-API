using MediatR;
using TheFullStackTeam.Application.Jobs.JobResposability.Results;

namespace TheFullStackTeam.Application.Jobs.JobResponsability.Command
{
    public class DeleteJobResponsabilitiesCommand : IRequest<DeleteJobResponsabilityCommandResult>
    {
        public Guid ResponsabilityId { get; set; }
        public DeleteJobResponsabilitiesCommand(Guid rId)
        {
            ResponsabilityId = rId;
        }
    }
}
