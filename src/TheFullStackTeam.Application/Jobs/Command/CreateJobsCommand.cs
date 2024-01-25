using MediatR;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Application.Model.EntityModel;

namespace TheFullStackTeam.Application.Jobs.Command
{
    public class CreateJobsCommand : IRequest<CreateJobCommandResult>
    {
        public JobModel Model { get; set; }
        public CreateJobsCommand(JobModel model)
        {
           Model = model;
        }
    }
}
