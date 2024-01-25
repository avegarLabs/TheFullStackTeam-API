using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalServices
{
    public class AddSkillToProfessionalServiceCommandHandler : AppRequestHandler, IRequestHandler<AddSkillToProfessionalServicesCommand, UpdateProfessionalServicesCommandResult>
    {
        private readonly IMonikerService _moniker;
        public AddSkillToProfessionalServiceCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker) : base(context)
        {
            _moniker= moniker;
        }

        public async Task<UpdateProfessionalServicesCommandResult> Handle(AddSkillToProfessionalServicesCommand request, CancellationToken cancellationToken)
        {
            var profService = await _context.ProfessionalSevices.Where(ps => ps.ProfessionalId.Equals(request.ProfessionalId) && ps.Id.Equals(request.ServiceId)).SingleOrDefaultAsync(cancellationToken);
            if(profService != null)
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
                if (!profService.ServiceSkills.Contains(skill))
                {
                    profService.ServiceSkills.Add(skill);
                    _context.ProfessionalSevices.Update(profService);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return new UpdateProfessionalServicesCommandResult(profService);
        }
    }
}
