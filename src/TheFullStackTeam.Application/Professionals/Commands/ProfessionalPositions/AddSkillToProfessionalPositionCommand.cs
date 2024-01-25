using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalPositions;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfessionalsPositions
{
    public class AddSkillToProfessionalPositionCommand : IRequest<UpdateProfessionalPositionsCommandResult>
    {
        public Guid ProfessionalId { get; set; }
        public Guid PositionId { get; set; }
        public SkillModel Skill { get; set; }

        public AddSkillToProfessionalPositionCommand(Guid professionalId, Guid positionId, SkillModel skill)
        {
            ProfessionalId = professionalId;
            PositionId = positionId;
            Skill = skill;
        }
    }
}