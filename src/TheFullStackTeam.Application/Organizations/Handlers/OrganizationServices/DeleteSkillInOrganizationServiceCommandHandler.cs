using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Organizations.Results.OrganizationsServicesResults;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Handlers.OrganizationServices
{
    public class DeleteSkillInOrganizationServiceCommandHandler : AppRequestHandler, IRequestHandler<DeleteSkillInOrganizationServiceCommand, UpdateOrganizationServicesCommandResult>
    {
        public DeleteSkillInOrganizationServiceCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<UpdateOrganizationServicesCommandResult> Handle(DeleteSkillInOrganizationServiceCommand request, CancellationToken cancellationToken)
        {
            var orgService = await _context.OrganizationSevices.Where(ps => ps.OrganizationId.Equals(request.OrganizationId) && ps.Id.Equals(request.ServiceId)).SingleOrDefaultAsync(cancellationToken);
            if (orgService != null)
            {
               var skill = orgService.ServiceSkills.SingleOrDefault(skill => skill.Id.Equals(request.SkillId));
               orgService.ServiceSkills.Remove(skill);

                _context.OrganizationSevices.Update(orgService);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return new UpdateOrganizationServicesCommandResult(orgService);
        }
    }
}
