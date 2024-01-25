using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Institution.Command;
using TheFullStackTeam.Application.Institution.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Institution.Handler
{
    public class DeleteInstitutionCommandHandler : AppRequestHandler, IRequestHandler<DeleteInstitutionCommand, InstitutionDeleteCommandResults>
    {
        public DeleteInstitutionCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<InstitutionDeleteCommandResults> Handle(DeleteInstitutionCommand request, CancellationToken cancellationToken)
        {
            var institution = await _context.institutions.Where(i => i.Id.Equals(request.InstitutionId)).SingleOrDefaultAsync(cancellationToken);
            
            if(institution == null)
            {
                throw new Exception($"Institution Id: {request.InstitutionId} not fount");
            }
            await CleanInstitutionsRelationships(institution, cancellationToken);

            _context.institutions.Remove(institution);
            await _context.SaveChangesAsync(cancellationToken);
            return new InstitutionDeleteCommandResults(true);
        }

        private async Task<Task> CleanInstitutionsRelationships(Domain.Entities.Institution institution, CancellationToken cancellationToken)
        {
            var titles = await _context.Titles.Where(t => t.InstitutionId == institution.Id).ToListAsync(cancellationToken);
            _context.Titles.RemoveRange(titles);

            var positions = await _context.Positions.Where(t => t.InstitutionId == institution.Id).ToListAsync(cancellationToken);
            _context.Positions.RemoveRange(positions);

            await _context.SaveChangesAsync(cancellationToken);
            return Task.CompletedTask;

        }
    }
}
