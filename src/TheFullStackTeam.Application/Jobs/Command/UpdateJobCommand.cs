using MediatR;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Application.Model.EntityModel;

namespace TheFullStackTeam.Application.Jobs.Command
{
    public class UpdateJobCommand : IRequest<CreateJobCommandResult>
    {
        public Guid JobId { get; }
        public JobModel JobPost { get; set; }

        public UpdateJobCommand(Guid id, JobModel model)
        {
            JobId = id;
            JobPost = model;
        }
    }
}
