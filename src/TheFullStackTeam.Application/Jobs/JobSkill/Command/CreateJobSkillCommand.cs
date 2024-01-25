using MediatR;
using TheFullStackTeam.Application.Jobs.JobSkill.Results;
using TheFullStackTeam.Application.Model.EntityModel;

namespace TheFullStackTeam.Application.Jobs.JobSkill.Command
{
    public class CreateJobSkillCommand : IRequest<JobSkillsCommandsResults>
    {
        public Guid JobId { get; set; }
        public JobSkillModel Model {get; set;}

        public CreateJobSkillCommand(Guid jobId, JobSkillModel model)
        {
            JobId = jobId;
            Model = model;
        }
    }
}
