using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Organizations.Commands.OrganizationServices;
using TheFullStackTeam.Application.Organizations.Results.OrganizationsServicesResults;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Organizations.Handlers.OrganizationServices
{
    public class AddSkillToOrganizationServiceCommandHandler : AppRequestHandler, IRequestHandler<AddSkillToOrganizationServicesCommand, UpdateOrganizationServicesCommandResult>
    {
        private readonly IMonikerService _moniker;
        public AddSkillToOrganizationServiceCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker) : base(context)
        {
            _moniker= moniker;
        }

        public async Task<UpdateOrganizationServicesCommandResult> Handle(AddSkillToOrganizationServicesCommand request, CancellationToken cancellationToken)
        {
            var orgService = await _context.OrganizationSevices.Where(ps => ps.OrganizationId.Equals(request.OrganizationId) && ps.Id.Equals(request.ServiceId)).SingleOrDefaultAsync(cancellationToken);
            if(orgService != null)
            {
                var skill = await _context.Skills.Where(s => s.Moniker.Equals(request.Skill.Name)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
                if(skill == null)
                {
                    skill = new Domain.Entities.Skill()
                    {
                        Name = request.Skill.Name,
                        Moniker = await _moniker.FindValidMoniker<Skill>(request.Skill.Name)

                    };
                    await _context.Skills.AddAsync(skill, cancellationToken);
                }
                if (!orgService.ServiceSkills.Contains(skill))
                {
                    orgService.ServiceSkills.Add(skill);
                    _context.OrganizationSevices.Update(orgService);
                }
                await _context.SaveChangesAsync(cancellationToken);
            }
            return new UpdateOrganizationServicesCommandResult(orgService);
        }
    }
}
