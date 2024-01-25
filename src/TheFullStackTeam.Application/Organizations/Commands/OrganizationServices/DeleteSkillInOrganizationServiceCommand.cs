using MediatR;
using TheFullStackTeam.Application.Organizations.Results.OrganizationsServicesResults;

namespace TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices
{
    public class DeleteSkillInOrganizationServiceCommand: IRequest<UpdateOrganizationServicesCommandResult>
    {
        public Guid OrganizationId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid SkillId { get; set; }

        public DeleteSkillInOrganizationServiceCommand(Guid organizationId, Guid serviceId, Guid skillId)
        {
            OrganizationId = organizationId;
            ServiceId = serviceId;
            SkillId = skillId;
        }
    }
}
