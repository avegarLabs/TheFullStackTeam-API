using MediatR;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices
{
    public class DeleteSkillInProfessionalServiceCommand: IRequest<UpdateProfessionalServicesCommandResult>
    {
        public Guid ProfessionalId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid SkillId { get; set; }

        public DeleteSkillInProfessionalServiceCommand(Guid professionalId, Guid serviceId, Guid skillId)
        {
            ProfessionalId = professionalId;
            ServiceId = serviceId;
            SkillId = skillId;
        }
    }
}
