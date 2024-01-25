using MediatR;
using TheFullStackTeam.Application.Jobs.JobSkill.Results;

namespace TheFullStackTeam.Application.Jobs.JobSkill.Command
{
    public class DeleteJobSkillCommand : IRequest<DeleteJobSkillCommandResult>
    {
        public Guid SkillId { get; set; }
        public DeleteJobSkillCommand(Guid skillId)
        {
            SkillId = skillId;
        }
    }
}
