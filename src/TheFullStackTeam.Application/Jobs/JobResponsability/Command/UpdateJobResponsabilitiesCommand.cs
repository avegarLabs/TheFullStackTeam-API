using MediatR;
using TheFullStackTeam.Application.Jobs.JobResponsability.Results;
using TheFullStackTeam.Application.Model.EntityModel;

namespace TheFullStackTeam.Application.Jobs.JobResponsability.Command
{
    public class UpdateJobResponsabilitiesCommand : IRequest<JobResponsabilitiesCommandsResults>
    {
        public Guid JobId { get; set; }
        public Guid JobResp { get; set; }
        public JobResposabilitiesModel Model {get; set;}

        public UpdateJobResponsabilitiesCommand(Guid jobId, Guid jobResp, JobResposabilitiesModel model)
        {
            JobId = jobId;
            JobResp = jobResp;
            Model = model;
        }
    }
}
