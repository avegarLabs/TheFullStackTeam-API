using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices
{
    public class AddSkillToProfessionalServicesCommand: IRequest<UpdateProfessionalServicesCommandResult>
    {
        public Guid ProfessionalId { get; set; }
        public Guid ServiceId { get; set; }
        public SkillModel Skill { get; set; }

        public AddSkillToProfessionalServicesCommand(Guid professionalId, Guid serviceId, SkillModel item)
        {
            ProfessionalId = professionalId;
            ServiceId = serviceId;
            Skill = item;
        }
    }
}
