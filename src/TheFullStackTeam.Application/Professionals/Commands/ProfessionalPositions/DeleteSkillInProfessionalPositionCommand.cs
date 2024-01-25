using MediatR;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalPositions;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalPositions
{
    public class DeleteSkillInProfessionalPositionCommand: IRequest<UpdateProfessionalPositionsCommandResult>
    {
        public Guid ProfessionalId { get; set; }
        public Guid PositionId { get; set; }
        public Guid SkillId { get; set; }

        public DeleteSkillInProfessionalPositionCommand(Guid professionalId, Guid positionId, Guid skillId)
        {
            ProfessionalId = professionalId;
            PositionId = positionId;
            SkillId = skillId;
        }
    }
}
