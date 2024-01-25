using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Professionals.Commands.ProfesionalServices;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalPositions;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalPositions;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalServices
{
    public class DeleteSkillInProfessionalPositionsCommandHandler : AppRequestHandler, IRequestHandler<DeleteSkillInProfessionalPositionCommand, UpdateProfessionalPositionsCommandResult>
    {
        public DeleteSkillInProfessionalPositionsCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<UpdateProfessionalPositionsCommandResult> Handle(DeleteSkillInProfessionalPositionCommand request, CancellationToken cancellationToken)
        {
            var profPositions = await _context.Positions.Where(ps => ps.ProfessionalId.Equals(request.ProfessionalId) && ps.Id.Equals(request.PositionId)).SingleOrDefaultAsync(cancellationToken);
            if (profPositions != null)
            {
               var skill = profPositions.SkillPositions.SingleOrDefault(skill => skill.Id.Equals(request.SkillId));
               profPositions.SkillPositions.Remove(skill);

                _context.Positions.Update(profPositions);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return new UpdateProfessionalPositionsCommandResult(profPositions);
        }
    }
}
