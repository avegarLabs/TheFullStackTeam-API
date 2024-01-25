using MediatR;
using TheFullStackTeam.Application.Jobs.JobSkill.Results;
using TheFullStackTeam.Application.Model.EntityModel;

namespace TheFullStackTeam.Application.Jobs.JobSkill.Command
{
    public class UpdateJobSkillCommand: IRequest<JobSkillsCommandsResults>
    {
        public Guid JobId { get; set; } 
        public Guid JobSkillId { get; set; }
        public JobSkillModel Model { get; set; }

        public UpdateJobSkillCommand(Guid jobId, Guid jobSkillId, JobSkillModel model)
        {
            JobId = jobId;
            JobSkillId = jobSkillId;
            Model = model;
        }
    }
}
