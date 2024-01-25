using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Jobs.JobResponsability.Command;
using TheFullStackTeam.Application.Jobs.JobResponsability.Results;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.JobResponsability.Handler
{
    public class UpdateJobResponsabilitiesCommandHandler : AppRequestHandler, IRequestHandler<UpdateJobResponsabilitiesCommand, JobResponsabilitiesCommandsResults>
    {
        public UpdateJobResponsabilitiesCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<JobResponsabilitiesCommandsResults> Handle(UpdateJobResponsabilitiesCommand request, CancellationToken cancellationToken)
        {

            var jobResponsability = await _context.JobResponsabilities.Where(j => j.Id.Equals(request.JobResp) && j.JobId.Equals(request.JobId)).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
            if (jobResponsability == null)
            {
                throw new NotFoundException(nameof(JobResponsabilities), request.JobResp);
            }

            jobResponsability.ResposabilityDescription = request.Model.ResponsibilityDescription;
            _context.JobResponsabilities.Update(jobResponsability);
            await _context.SaveChangesAsync(cancellationToken);
            return new JobResponsabilitiesCommandsResults(jobResponsability);
        }
    }
}
