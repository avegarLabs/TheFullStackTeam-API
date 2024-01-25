using MediatR;
using TheFullStackTeam.Application.Model.EntityModel;
using TheFullStackTeam.Application.Organizations.Results.OrganizationsServicesResults;

namespace TheFullStackTeam.Application.Organizations.Commands.OrganizationServices
{
    public class AddSkillToOrganizationServicesCommand: IRequest<UpdateOrganizationServicesCommandResult>
    {
        public Guid OrganizationId { get; set; }
        public Guid ServiceId { get; set; }
        public SkillModel Skill { get; set; }

        public AddSkillToOrganizationServicesCommand(Guid organizationId, Guid serviceId, SkillModel model)
        {
            OrganizationId = organizationId;
            ServiceId = serviceId;
            Skill = model;
        }
    }
}
