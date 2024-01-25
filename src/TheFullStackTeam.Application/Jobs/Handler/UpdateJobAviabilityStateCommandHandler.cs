using MediatR;
using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Application.Exceptions;
using TheFullStackTeam.Application.Jobs.Command;
using TheFullStackTeam.Application.Jobs.Results;
using TheFullStackTeam.Persistence.App;

namespace TheFullStackTeam.Application.Jobs.Handler
{
    public class UpdateJobAviabilityStateCommandHandler : AppRequestHandler, IRequestHandler<UpdateJobAviabilityStateCommand, CreateJobCommandResult>
    {
        public UpdateJobAviabilityStateCommandHandler(TheFullStackTeamDbContext context) : base(context)
        {
        }

        public async Task<CreateJobCommandResult> Handle(UpdateJobAviabilityStateCommand request, CancellationToken cancellationToken)
        {
            var job = await _context.Jobs.FindAsync(request.JobId);

            if (job == null)
            {
                throw new NotFoundException(nameof(Jobs), request.JobId);
            }

            job.Active = request.State;
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateJobCommandResult(job);
        }
    }
}
