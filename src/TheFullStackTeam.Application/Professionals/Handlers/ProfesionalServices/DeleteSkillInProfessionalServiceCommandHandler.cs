using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalServices
{
    public class DeleteSkillInProfessionalServiceCommandHandler : AppRequestHandler, IRequestHandler<DeleteSkillInProfessionalServiceCommand, UpdateProfessionalServicesCommandResult>
    {
        public DeleteSkillInProfessionalServiceCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<UpdateProfessionalServicesCommandResult> Handle(DeleteSkillInProfessionalServiceCommand request, CancellationToken cancellationToken)
        {
            var profService = await _context.ProfessionalSevices.Where(ps => ps.ProfessionalId.Equals(request.ProfessionalId) && ps.Id.Equals(request.ServiceId)).SingleOrDefaultAsync(cancellationToken);
            if (profService != null)
            {
               var skill = profService.ServiceSkills.SingleOrDefault(skill => skill.Id.Equals(request.SkillId));
               profService.ServiceSkills.Remove(skill);

                _context.ProfessionalSevices.Update(profService);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return new UpdateProfessionalServicesCommandResult(profService);
        }
    }
}
