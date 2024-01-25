using MediatR;
using TheFullStackTeam.Application.Jobs.JobResponsability.Results;
using TheFullStackTeam.Application.Model.EntityModel;

namespace TheFullStackTeam.Application.Jobs.JobResponsability.Command
{
    public class CreateJobResponsabilitiesCommand : IRequest<JobResponsabilitiesCommandsResults>
    {
        public Guid JobId { get; set; }
        public JobResposabilitiesModel Model {get; set;}

        public CreateJobResponsabilitiesCommand(Guid jobId, JobResposabilitiesModel model)
        {
            JobId = jobId;
            Model = model;
        }
    }
}
