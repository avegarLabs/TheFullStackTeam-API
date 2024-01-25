using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Professionals.Commands.ProfessionalsPositions;
using TheFullStackTeam.Application.Professionals.Results.ProfesionalServicesResults;
using TheFullStackTeam.Application.Professionals.Results.ProfessionalPositions;
using TheFullStackTeam.Application.Services.Abstract;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Professionals.Handlers.ProfesionalPositions
{
    public class AddSkillToProfessionalPositionsCommandHandler : AppRequestHandler, IRequestHandler<AddSkillToProfessionalPositionCommand, UpdateProfessionalPositionsCommandResult>
    {
        private readonly IMonikerService _moniker;
        public AddSkillToProfessionalPositionsCommandHandler(TheFullStackTeamDbContext context, IMonikerService moniker) : base(context)
        {
            _moniker= moniker;
        }

        public async Task<UpdateProfessionalPositionsCommandResult> Handle(AddSkillToProfessionalPositionCommand request, CancellationToken cancellationToken)
        {
            var profPosition = await _context.Positions.Where(ps => ps.ProfessionalId.Equals(request.ProfessionalId) && ps.Id.Equals(request.PositionId)).SingleOrDefaultAsync(cancellationToken);
            if(profPosition != null)
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
                if (!profPosition.SkillPositions.Contains(skill))
                {
                    profPosition.SkillPositions.Add(skill);
                    _context.Positions.Update(profPosition);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return new UpdateProfessionalPositionsCommandResult(profPosition);
        }
    }
}
