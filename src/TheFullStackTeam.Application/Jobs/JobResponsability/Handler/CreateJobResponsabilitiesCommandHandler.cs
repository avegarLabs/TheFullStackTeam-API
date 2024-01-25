using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Jobs.JobResponsability.Command;
using TheFullStackTeam.Application.Jobs.JobResponsability.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.JobResponsability.Handler
{
    public class CreateJobResponsabilitiesCommandHandler : AppRequestHandler, IRequestHandler<CreateJobResponsabilitiesCommand, JobResponsabilitiesCommandsResults>
    {
        public CreateJobResponsabilitiesCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<JobResponsabilitiesCommandsResults> Handle(CreateJobResponsabilitiesCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs.Where(j => j.Id.Equals(request.JobId)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if (job == null)
            {
                throw new NotFoundException(nameof(Jobs), request.JobId);
            }
            var jResponsability = new Domain.Entities.JobResponsabilities()
            {
                ResposabilityDescription = request.Model.ResponsibilityDescription,
                JobId = job.Id
            };
            await _context.JobResponsabilities.AddAsync(jResponsability, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new JobResponsabilitiesCommandsResults(jResponsability);
        }
    }
}
